namespace CodeGenerators.Utils;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

public abstract class ValueObject
{
    [return: NotNullIfNotNull(nameof(value))]
    public static TValueObj? Create<TValueObj>(object? value)
        where TValueObj : ValueObject
    {
        if (value == null)
        {
            return null;
        }

        return CreateInstance<TValueObj>([value])!;
    }

    protected static TValueObj CreateInstance<TValueObj>(object[] values)
        where TValueObj : ValueObject
    {
        var type = typeof(TValueObj);
        var argTypes = values.Select(x => x.GetType()).ToArray();
        var ctor = type.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, argTypes);
        if (ctor == null)
        {
            throw new ArgumentException($"Constructor {type.Name}({string.Join(',', argTypes.Select(x => x.Name))}) does not found",
                nameof(values));
        }

        return (TValueObj)ctor.Invoke(values);
    }
}

[DebuggerDisplay("{Value}")]
public abstract class ValueObject<T> : ValueObject
{
    [NotNull]
    public T Value { get; }

    protected ValueObject([NotNull]T value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value), "Отсутствует значение для ValueObject");
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Value.ToString()!;
    }
}