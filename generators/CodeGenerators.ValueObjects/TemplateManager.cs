namespace CodeGenerators.ValueObjects;

using System.Collections.Immutable;

public class TemplateManager
{
    public const string NamespaceMarker = "TEMPLATE_NamespaceName";
    public const string ClassNameMarker = "TEMPLATE_ClassName";

    private readonly Lazy<ImmutableDictionary<string, string>> _lazyCache = new Lazy<ImmutableDictionary<string, string>>(InitCache);

    public string Comparable => _lazyCache.Value.GetValueOrDefault(nameof(Comparable))!;
    public string EnumValueObject => _lazyCache.Value.GetValueOrDefault(nameof(EnumValueObject))!;

    private static ImmutableDictionary<string, string> InitCache()
    {
        var assembly = typeof(TemplateManager).Assembly;

        var cache = new Dictionary<string, string>();
        foreach (var resourceName in assembly.GetManifestResourceNames())
        {
            using var resourceStream = assembly.GetManifestResourceStream(resourceName);
            if (resourceStream == null)
            {
                continue;
            }

            using var sr = new StreamReader(resourceStream);
            var parts = resourceName.Split('.');
            var fileName = parts[parts.Length - 2];

            cache[fileName] = sr.ReadToEnd();
        }

        return cache.ToImmutableDictionary();
    }
}