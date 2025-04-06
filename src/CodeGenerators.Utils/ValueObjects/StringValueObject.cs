namespace CodeGenerators.Utils;

using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

public abstract class StringValueObject : ValueObject<string>, IEqualityComparer<StringValueObject>, IEquatable<StringValueObject>
{
    protected StringValueObject(string value)
        : base(value)
    {
    }

    public override string ToString()
    {
        return Value;
    }

    /// <summary>
    /// Создать <see cref="StringValueObject"/>
    /// </summary>
    /// <param name="value">Строковое значение</param>
    /// <typeparam name="T">Тип значения</typeparam>
    [return: NotNullIfNotNull(nameof(value))]
    public static T? Create<T>(string? value)
        where T : StringValueObject
    {
        return ValueObject.Create<T>(value);
    }

    [return: NotNullIfNotNull(nameof(param))]
    public static implicit operator string?(StringValueObject? param)
    {
        return param?.Value;
    }

    public override bool Equals(object? obj)
    {
        return InternalEquals(this, obj as StringValueObject);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(StringValueObject? left, StringValueObject? right)
    {
        return InternalEquals(left, right);
    }

    public static bool operator !=(StringValueObject? left, StringValueObject? right)
    {
        return !InternalEquals(left, right);
    }

    private static bool InternalEquals(StringValueObject? left, StringValueObject? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }
        if (left is null || right is null || left.GetType() != right.GetType())
        {
            return false;
        }

        return left.Value == right.Value;
    }

    /// <inheritdoc />
    public bool Equals(StringValueObject? other)
    {
        return InternalEquals(this, other);
    }

    /// <inheritdoc />
    public bool Equals(StringValueObject? x, StringValueObject? y)
    {
        return InternalEquals(x, y);
    }

    /// <inheritdoc />
    public int GetHashCode(StringValueObject obj)
    {
        return obj.Value.GetHashCode();
    }

    protected static void ValidateLength<T>(T obj, int? minLength = null)
        where T : StringValueObject, IHaveMaxLength
    {
        var value = obj.Value;
        if (minLength.HasValue && (minLength > value.Length || value.Length > T.MaxLength))
        {
            throw new ArgumentException($"{typeof(T).Name} length must be in the range {minLength}..{T.MaxLength}", nameof(value));
        }
        else if (value.Length > T.MaxLength)
        {
            throw new ArgumentException($"{typeof(T).Name} length must be less than {T.MaxLength}", nameof(value));
        }
    }

    protected static void ValidateMask<T>(T obj)
        where T : StringValueObject, IHaveMask
    {
        var value = obj.Value;
        if (!T.Mask.IsMatch(value))
        {
            throw new ArgumentException($"{typeof(T).Name} must match mask: '{T.Mask}'", nameof(value));
        }
    }
}

public interface IHaveMaxLength
{
    /// <summary>
    /// Максимальная длина строки
    /// </summary>
    static abstract int MaxLength { get; }
}

public interface IHaveMask
{
    /// <summary>
    /// Маска строки
    /// </summary>
    static abstract Regex Mask { get; }
}