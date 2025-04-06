namespace CodeGenerators.ValueObjects;

using System.Collections.Immutable;
using System.Composition;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using CodeGenerators.Shared;

/// <inheritdoc />
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(EnumValueObjectAnalyzer)), Shared]
public class EnumValueObjectCodeFix : CodeFixProvider
{
    /// <inheritdoc />
    public sealed override ImmutableArray<string> FixableDiagnosticIds { get; } = ImmutableArray.Create(
        DiagnosticDescriptors.NamingRuleId,
        DiagnosticDescriptors.SealedRuleId,
        DiagnosticDescriptors.PrivateCtorRuleId
    );

    /// <inheritdoc />
    public sealed override FixAllProvider GetFixAllProvider()
    {
        // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
        return WellKnownFixAllProviders.BatchFixer;
    }

    /// <inheritdoc />
    public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
        if (root == null)
        {
            return;
        }

        var token = root.FindToken(context.Span.Start);
        if (!token.Span.IntersectsWith(context.Span))
        {
            return;
        }

        var generator = SyntaxGenerator.GetGenerator(context.Document);
        var node = generator.GetDeclaration(token.Parent);
        if (node == null)
        {
            return;
        }

        foreach (var diagnostic in context.Diagnostics)
        {
            switch (diagnostic.Id)
            {
                case DiagnosticDescriptors.SealedRuleId:
                    context.RegisterCodeFix(
                        CodeAction.Create(
                            "Добавить модификатор sealed partial",
                            c => FixSealedRuleAsync(context.Document, node, c),
                            $"Fix {DiagnosticDescriptors.SealedRuleId}"),
                        diagnostic);
                    break;
                case DiagnosticDescriptors.NamingRuleId:
                    context.RegisterCodeFix(
                        CodeAction.Create(
                            "Исправить наименование",
                            c => FixNamingRuleAsync(context.Document, node, c),
                            $"Fix {DiagnosticDescriptors.NamingRuleId}"),
                        diagnostic);
                    break;
                case DiagnosticDescriptors.PrivateCtorRuleId:
                    context.RegisterCodeFix(
                        CodeAction.Create(
                            "Закрыть конструктор",
                            c => FixSPrivateCtorAsync(context.Document, node, c),
                            $"Fix {DiagnosticDescriptors.PrivateCtorRuleId}"),
                        diagnostic);
                    break;
            }
        }
    }

    private async Task<Document> FixNamingRuleAsync(Document document, SyntaxNode node, CancellationToken cancellationToken)
    {
        var propertyDeclarationSyntax = FindParentSyntax<PropertyDeclarationSyntax>(node);
        if (propertyDeclarationSyntax != null)
        {
            var initializerExpr = propertyDeclarationSyntax.Initializer?.ChildNodes()
                .OfType<BaseObjectCreationExpressionSyntax>()
                .FirstOrDefault();

            if (initializerExpr == null)
            {
                return document;
            }

            var argListExpr = initializerExpr.ArgumentList;
            if (argListExpr == null)
            {
                return document;
            }

            var argList = argListExpr.Arguments;
            if (argList.Count != 2)
            {
                return document;
            }

            var value = argList[1];

            var nameof = SyntaxFactory.ParseExpression($"nameof({propertyDeclarationSyntax.Identifier.ToString()})");

            var editor = await DocumentEditor.CreateAsync(document, cancellationToken).ConfigureAwait(false);
            editor.ReplaceNode(
                argListExpr,
                argListExpr.WithArguments(SyntaxFactory.SeparatedList(new[] {SyntaxFactory.Argument(nameof), value })));

            return editor.GetChangedDocument();
        }

        return document;
    }

    private async Task<Document> FixSealedRuleAsync(Document document, SyntaxNode node, CancellationToken cancellationToken)
    {
        var classDeclarationSyntax = FindParentSyntax<ClassDeclarationSyntax>(node);
        if (classDeclarationSyntax != null)
        {
            var editor = await DocumentEditor.CreateAsync(document, cancellationToken).ConfigureAwait(false);
            var oldSyntaxNode = classDeclarationSyntax;
            var mod = oldSyntaxNode.Modifiers.Where(z => !z.IsKind(SyntaxKind.SealedKeyword) && !z.IsKind(SyntaxKind.PartialKeyword)).ToList();
            mod.Add(SyntaxFactory.Token(SyntaxKind.SealedKeyword));
            mod.Add(SyntaxFactory.Token(SyntaxKind.PartialKeyword));
            var newSyntaxNode = oldSyntaxNode.WithModifiers(SyntaxFactory.TokenList(mod));

            editor.ReplaceNode(oldSyntaxNode, newSyntaxNode);

            return editor.GetChangedDocument();
        }

        return document;
    }

    private T? FindParentSyntax<T>(SyntaxNode? node)
        where T : SyntaxNode
    {
        if (node == null)
        {
            return null;
        }

        if (node is T classNode)
        {
            return classNode;
        }

        return FindParentSyntax<T>(node.Parent);
    }

    private async Task<Document> FixSPrivateCtorAsync(Document document, SyntaxNode node, CancellationToken cancellationToken)
    {
        var ctorDeclarationSyntax = FindParentSyntax<ConstructorDeclarationSyntax>(node);
        if (ctorDeclarationSyntax != null)
        {
            var editor = await DocumentEditor.CreateAsync(document, cancellationToken).ConfigureAwait(false);
            var oldSyntaxNode = ctorDeclarationSyntax;
            var mod = oldSyntaxNode.Modifiers.Where(z => !z.IsKind(SyntaxKind.PrivateKeyword)
                    && !z.IsKind(SyntaxKind.ProtectedKeyword)
                    && !z.IsKind(SyntaxKind.PublicKeyword)
                )
                .ToList();
            mod.Add(SyntaxFactory.Token(SyntaxKind.PrivateKeyword));
            var newSyntaxNode = oldSyntaxNode.WithModifiers(SyntaxFactory.TokenList(mod));

            editor.ReplaceNode(oldSyntaxNode, newSyntaxNode);

            return editor.GetChangedDocument();
        }

        return document;
    }
}