namespace CodeGenerators.Shared;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>
/// Описатель класса
/// </summary>
public struct ClassDescriptor
{
    /// <summary>
    /// .ctor
    /// </summary>
    public ClassDescriptor(ClassDeclarationSyntax node, INamedTypeSymbol symbol)
    {
        Node = node;
        Symbol = symbol;
    }

    /// <summary>
    /// Декларация класса
    /// </summary>
    public ClassDeclarationSyntax Node { get; }

    /// <summary>
    /// Символ класса
    /// </summary>
    public INamedTypeSymbol Symbol { get; }
}