namespace CodeGenerators.Shared;

using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>
/// Хэлпер для кодогенерации
/// </summary>
public static class GenerateHelper
{
    /// <summary>
    /// Безопасная кодогенерация с уведомлением об ошибке
    /// </summary>
    public static void SafeGenerate(this SourceProductionContext context, string fileName, Action action)
    {
        SafeGenerate(context, Location.None, fileName, action);
    }

    /// <summary>
    /// Безопасная кодогенерация с уведомлением об ошибке
    /// </summary>
    public static void SafeGenerate(this SourceProductionContext context, Location location, string fileName, Action action)
    {
        try
        {
            action();
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch(Exception e)
        {
            Debugger.Break();
            context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.GeneratorErrorRule,
                location,
                fileName,
                e.Message));
        }
    }

    /// <summary>
    /// Узел является <see langword="partial class"/>
    /// </summary>
    public static bool IsPartialClass(this SyntaxNode node, CancellationToken cancellationToken)
    {
        return node is ClassDeclarationSyntax classDeclarationSyntax
            && classDeclarationSyntax.Modifiers.Any(z => z.IsKind(SyntaxKind.PartialKeyword));
    }

    /// <summary>
    /// Узел является <see langword="partial class"/>
    /// </summary>
    public static ClassDescriptor ToClassDescriptor(this GeneratorSyntaxContext syntaxContext, CancellationToken cancellationToken)
    {
        return new ClassDescriptor((syntaxContext.Node as ClassDeclarationSyntax)!,
            syntaxContext.SemanticModel.GetDeclaredSymbol((syntaxContext.Node as ClassDeclarationSyntax)!)!);
    }
}