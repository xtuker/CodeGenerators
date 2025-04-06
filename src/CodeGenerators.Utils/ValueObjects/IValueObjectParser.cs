namespace CodeGenerators.Utils;

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

public interface IValueObjectParser<TValueObject, TValue>
{
    [return: NotNullIfNotNull(nameof(value))]
    static abstract TValueObject Parse([NotNull]TValue? value);

    static abstract TValueObject? TryParse(TValue? value);

    static abstract Expression<Func<TValue?, TValueObject?>> TryParseExpression { get; }
}

public interface IEnumValueObjectParser<TValueObject> : IValueObjectParser<TValueObject, int?>
    where TValueObject : EnumValueObject
{
    [return: NotNullIfNotNull(nameof(value))]
    static abstract TValueObject Parse([NotNull]string? value);

    static abstract TValueObject? TryParse(string? value);
}

public interface IStringValueObjectParser<TValueObject> : IValueObjectParser<TValueObject, string?>
{
}

public interface IIdValueObjectParser<TValueObject> : IValueObjectParser<TValueObject, long?>
{
}