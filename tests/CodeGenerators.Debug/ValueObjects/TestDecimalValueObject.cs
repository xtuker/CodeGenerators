namespace CodeGenerators.Debug.ValueObjects;

using CodeGenerators.Utils;
using CodeGenerators.Utils.Attributes;

[ComparableValueObject]
public sealed partial class TestDecimalValueObject : ValueObject<decimal>
{
    /// <inheritdoc />
    public TestDecimalValueObject(decimal value) : base(value)
    {
    }
}