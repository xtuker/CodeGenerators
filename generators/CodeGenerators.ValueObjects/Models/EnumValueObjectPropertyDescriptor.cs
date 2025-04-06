namespace CodeGenerators.ValueObjects.Models;

internal readonly struct EnumValueObjectPropertyDescriptor : IEquatable<EnumValueObjectPropertyDescriptor>, IComparable<EnumValueObjectPropertyDescriptor>, IComparable
{
    public EnumValueObjectPropertyDescriptor(string propertyName, string name, string value, bool isFinal)
    {
        PropertyName = propertyName;
        Name = name;
        Value = value;
        IsFinal = isFinal;
    }

    public string PropertyName { get; }
    public string Name { get; }
    public string Value { get; }
    public bool IsFinal { get; }

    /// <inheritdoc />
    public bool Equals(EnumValueObjectPropertyDescriptor other)
    {
        return PropertyName == other.PropertyName && Name == other.Name && Value == other.Value && IsFinal == other.IsFinal;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is EnumValueObjectPropertyDescriptor other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = PropertyName.GetHashCode();
            hashCode = (hashCode * 397) ^ Name.GetHashCode();
            hashCode = (hashCode * 397) ^ Value.GetHashCode();
            hashCode = (hashCode * 397) ^ IsFinal.GetHashCode();
            return hashCode;
        }
    }

    public static bool operator ==(EnumValueObjectPropertyDescriptor left, EnumValueObjectPropertyDescriptor right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(EnumValueObjectPropertyDescriptor left, EnumValueObjectPropertyDescriptor right)
    {
        return !left.Equals(right);
    }

    /// <inheritdoc />
    public int CompareTo(EnumValueObjectPropertyDescriptor other)
    {
        var propertyNameComparison = string.Compare(PropertyName, other.PropertyName, StringComparison.Ordinal);
        if (propertyNameComparison != 0)
        {
            return propertyNameComparison;
        }

        var nameComparison = string.Compare(Name, other.Name, StringComparison.Ordinal);
        if (nameComparison != 0)
        {
            return nameComparison;
        }

        return string.Compare(Value, other.Value, StringComparison.Ordinal);
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return 1;
        }

        return obj is EnumValueObjectPropertyDescriptor other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(EnumValueObjectPropertyDescriptor)}");
    }

    public static bool operator <(EnumValueObjectPropertyDescriptor left, EnumValueObjectPropertyDescriptor right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(EnumValueObjectPropertyDescriptor left, EnumValueObjectPropertyDescriptor right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator <=(EnumValueObjectPropertyDescriptor left, EnumValueObjectPropertyDescriptor right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >=(EnumValueObjectPropertyDescriptor left, EnumValueObjectPropertyDescriptor right)
    {
        return left.CompareTo(right) >= 0;
    }
}