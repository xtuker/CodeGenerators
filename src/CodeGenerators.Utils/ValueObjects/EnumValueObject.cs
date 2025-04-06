namespace CodeGenerators.Utils;

using CodeGenerators.Utils.Attributes;

/// <inheritdoc cref="IEnumValueObject" />
public abstract class EnumValueObject : ValueObject<int>, IEnumValueObject
{
    public string Name { get; }

    protected EnumValueObject(string name, int value)
        : base(value)
    {
        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }

    /// <summary>
    /// Является финальным статусом <see cref="FinalStatusAttribute"/>
    /// </summary>
    public virtual bool IsFinal()
    {
        return false;
    }
}

/// <summary>
/// ValueObject-перечисление
/// </summary>
public interface IEnumValueObject
{
    /// <summary>
    /// Значение
    /// </summary>
    int Value { get; }

    /// <summary>
    /// Наименование
    /// </summary>
    string Name { get; }
}

/// <summary>
/// Содержит список доступных значений
/// </summary>
public interface IHavePossibleValues
{
    /// <summary>
    /// Список доступных значений
    /// </summary>
    /// <returns>value (name)...</returns>
    static abstract string PossibleValues { get; }
}