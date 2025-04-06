namespace CodeGenerators.ValueObjects;

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using CodeGenerators.Shared;

/// <inheritdoc />
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class EnumValueObjectAnalyzer : DiagnosticAnalyzer
{

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    private static ImmutableArray<DiagnosticDescriptor> Diagnostics =>
        ImmutableArray.Create(DiagnosticDescriptors.GeneratorErrorRule,
            DiagnosticDescriptors.SealedRule,
            DiagnosticDescriptors.NamingRule,
            DiagnosticDescriptors.DupplicateValueRule,
            DiagnosticDescriptors.PrivateCtorRule,
            DiagnosticDescriptors.PositionalArgOnlyRule
        );
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    /// <inheritdoc />
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => Diagnostics;

    /// <inheritdoc />
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.ClassDeclaration);
    }

    private void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        var classSyntax = (ClassDeclarationSyntax)context.Node;
        var isEnumValueObject = classSyntax.BaseList?.Types.Any(x => x.ToString().EndsWith("EnumValueObject")) ?? false;
        if (!isEnumValueObject)
        {
            return;
        }

        var isSealed = classSyntax.Modifiers.Any(x => x.IsKind(SyntaxKind.SealedKeyword));
        var isPartial = classSyntax.Modifiers.Any(x => x.IsKind(SyntaxKind.PartialKeyword));
        if (!isSealed || !isPartial)
        {
            context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.SealedRule, Location.Create(classSyntax.SyntaxTree, classSyntax.Modifiers.FullSpan), classSyntax.Identifier.ToString()));
        }

        foreach (var ctor in classSyntax.Members.OfType<ConstructorDeclarationSyntax>())
        {
            if (!ctor.Modifiers.Any(x => x.IsKind(SyntaxKind.PrivateKeyword)))
            {
                var location = new TextSpan(ctor.Modifiers.FullSpan.Start, ctor.Modifiers.FullSpan.Length + ctor.Identifier.Span.Length);
                context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.PrivateCtorRule, Location.Create(ctor.SyntaxTree, location)));
            }
        }

        var valueSet = new HashSet<string>();
        foreach (var property in classSyntax.Members.OfType<PropertyDeclarationSyntax>())
        {
            AnalyzeProperty(context, property, valueSet);
        }
    }

    private static void AnalyzeProperty(SyntaxNodeAnalysisContext context, PropertyDeclarationSyntax property, ISet<string> valueSet)
    {
        if (property.Initializer == null)
        {
            return;
        }

        var isStatic = property.Modifiers.Any(z => z.IsKind(SyntaxKind.StaticKeyword));
        if (!isStatic)
        {
            return;
        }

        var initializerExpr = property.Initializer.ChildNodes()
            .OfType<BaseObjectCreationExpressionSyntax>()
            .FirstOrDefault();

        if (initializerExpr == null)
        {
            return;
        }

        var argListExpr = initializerExpr.ArgumentList;
        if (argListExpr == null)
        {
            return;
        }

        var argList = argListExpr.Arguments;
        if (argList.Count != 2)
        {
            return;
        }

        var propName = property.Identifier.ToString();
        var name = argList[0];
        var value = argList[1];

        if (name.NameColon != null)
        {
            context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.PositionalArgOnlyRule, name.GetLocation(), propName, name.NameColon.ToString()));
        }
        if (value.NameColon != null)
        {
            context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.PositionalArgOnlyRule, value.GetLocation(), propName, value.NameColon.ToString()));
        }

        var nameString = name.ToString();
        if (nameString != $"nameof({propName})")
        {
            context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.NamingRule, name.GetLocation(), propName, nameString));
        }

        var valueString = value.ToString();
        if (value.Expression.IsKind(SyntaxKind.NumericLiteralExpression))
        {
            if (!valueSet.Add(valueString))
            {
                context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.DupplicateValueRule, value.GetLocation(), propName, valueString));
            }
        }
        else if (value.Expression is IdentifierNameSyntax identifierNameSyntax)
        {
            var constValue = context.SemanticModel.GetConstantValue(identifierNameSyntax, context.CancellationToken);
            if (!valueSet.Add(constValue.ToString()))
            {
                context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.DupplicateValueRule, value.GetLocation(), propName, $"{valueString} = {constValue}"));
            }
        }
    }
}