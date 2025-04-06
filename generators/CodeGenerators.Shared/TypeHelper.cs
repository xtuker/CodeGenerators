namespace CodeGenerators.Shared;

using Microsoft.CodeAnalysis;

/// <summary>
/// Методы расширения <see cref="INamespaceSymbol"/>
/// </summary>
public static class NamespaceSymbolExtensions
{
    /// <summary>
    /// Обход <see langword="namespace"/> в поисках объявленных символов
    /// </summary>
    public static IEnumerable<INamedTypeSymbol> GetAllTypes(this INamespaceSymbol @namespace)
    {
        foreach (var type in @namespace.GetTypeMembers())
        {
            foreach (var nestedType in GetNestedTypes(type))
            {
                yield return nestedType;
            }
        }

        foreach (var nestedNamespace in @namespace.GetNamespaceMembers())
        {
            foreach (var type in GetAllTypes(nestedNamespace))
            {
                yield return type;
            }
        }
    }

    internal static IEnumerable<INamedTypeSymbol> GetNestedTypes(INamedTypeSymbol type)
    {
        yield return type;
        foreach (var nestedType in type.GetTypeMembers().SelectMany(GetNestedTypes))
        {
            yield return nestedType;
        }
    }
}