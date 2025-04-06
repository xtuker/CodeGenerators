namespace CodeGenerators.Utils;

/// <summary>
/// Базовый класс типизированного идентификатора
/// </summary>
public abstract class BaseId : ValueObject<long>, IEqualityComparer<BaseId>, IEquatable<BaseId>
{
    /// <summary>
    /// Создать типизированный идентификатор
    /// </summary>
    /// <param name="value">Значение идентификатора</param>
    /// <typeparam name="T">Тип идентификатора</typeparam>
    public static T Create<T>(long value)
        where T : BaseId
    {
        return CreateInstance<T>([value]);
    }

    protected BaseId(long value)
        : base(value)
    {
    }

    public static implicit operator long(BaseId param)
    {
        return param.Value;
    }

    public static implicit operator long?(BaseId? param)
    {
        return param?.Value;
    }

    public override bool Equals(object? obj)
    {
        return InternalEquals(this, obj as BaseId);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(BaseId? left, BaseId? right)
    {
        return InternalEquals(left, right);
    }

    public static bool operator !=(BaseId? left, BaseId? right)
    {
        return !InternalEquals(left, right);
    }

    private static bool InternalEquals(BaseId? left, BaseId? right)
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
    public bool Equals(BaseId? other)
    {
        return InternalEquals(this, other);
    }

    /// <inheritdoc />
    public bool Equals(BaseId? x, BaseId? y)
    {
        return InternalEquals(x, y);
    }

    /// <inheritdoc />
    public int GetHashCode(BaseId obj)
    {
        return obj.Value.GetHashCode();
    }
}