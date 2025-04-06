namespace CodeGenerators.ValueObjects.Models;

using Microsoft.CodeAnalysis;

internal readonly struct ValueObjectDescriptor : IEquatable<ValueObjectDescriptor>, IComparable<ValueObjectDescriptor>, IComparable
{
    public ValueObjectDescriptor(string ns, string className, Location location)
    {
        Namespace = ns;
        ClassName = className;
        Location = location;
    }

    public string Namespace { get; }
    public string ClassName { get; }
    public Location Location { get; }

    /// <inheritdoc />
    public bool Equals(ValueObjectDescriptor other)
    {
        return Namespace == other.Namespace && ClassName == other.ClassName;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is ValueObjectDescriptor other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Namespace.GetHashCode();
            hashCode = (hashCode * 397) ^ ClassName.GetHashCode();
            return hashCode;
        }
    }

    public static bool operator ==(ValueObjectDescriptor left, ValueObjectDescriptor right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ValueObjectDescriptor left, ValueObjectDescriptor right)
    {
        return !left.Equals(right);
    }

    /// <inheritdoc />
    public int CompareTo(ValueObjectDescriptor other)
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

        return obj is ValueObjectDescriptor other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(ValueObjectDescriptor)}");
    }

    public static bool operator <(ValueObjectDescriptor left, ValueObjectDescriptor right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(ValueObjectDescriptor left, ValueObjectDescriptor right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator <=(ValueObjectDescriptor left, ValueObjectDescriptor right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >=(ValueObjectDescriptor left, ValueObjectDescriptor right)
    {
        return left.CompareTo(right) >= 0;
    }
}