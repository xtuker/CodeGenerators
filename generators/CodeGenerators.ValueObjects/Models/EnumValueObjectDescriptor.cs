namespace CodeGenerators.ValueObjects.Models;

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

internal readonly struct EnumValueObjectDescriptor : IEquatable<EnumValueObjectDescriptor>, IComparable<EnumValueObjectDescriptor>, IComparable
{
    public EnumValueObjectDescriptor(string ns, string className, ImmutableArray<EnumValueObjectPropertyDescriptor> properties, Location location)
    {
        Namespace = ns;
        ClassName = className;
        Properties = properties;
        Location = location;
    }

    public string Namespace { get; }
    public string ClassName { get; }
    public ImmutableArray<EnumValueObjectPropertyDescriptor> Properties { get; }
    public Location Location { get; }

    /// <inheritdoc />
    public bool Equals(EnumValueObjectDescriptor other)
    {
        return Namespace == other.Namespace && ClassName == other.ClassName && Properties.SequenceEqual(other.Properties);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is EnumValueObjectDescriptor other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Namespace.GetHashCode();
            hashCode = (hashCode * 397) ^ ClassName.GetHashCode();
            hashCode = (hashCode * 397) ^ Properties.GetHashCode();
            return hashCode;
        }
    }

    public static bool operator ==(EnumValueObjectDescriptor left, EnumValueObjectDescriptor right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(EnumValueObjectDescriptor left, EnumValueObjectDescriptor right)
    {
        return !left.Equals(right);
    }

    /// <inheritdoc />
    public int CompareTo(EnumValueObjectDescriptor other)
    {
        var namespaceComparison = string.Compare(Namespace, other.Namespace, StringComparison.Ordinal);
        if (namespaceComparison != 0)
        {
            return namespaceComparison;
        }

        return string.Compare(ClassName, other.ClassName, StringComparison.Ordinal);
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return 1;
        }

        return obj is EnumValueObjectDescriptor other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(EnumValueObjectDescriptor)}");
    }

    public static bool operator <(EnumValueObjectDescriptor left, EnumValueObjectDescriptor right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(EnumValueObjectDescriptor left, EnumValueObjectDescriptor right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator <=(EnumValueObjectDescriptor left, EnumValueObjectDescriptor right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >=(EnumValueObjectDescriptor left, EnumValueObjectDescriptor right)
    {
        return left.CompareTo(right) >= 0;
    }
}