namespace CodeGenerators.Debug.ValueObjects;

using CodeGenerators.Utils;
using CodeGenerators.Utils.Attributes;

public sealed partial class TestEnumValueObject : EnumValueObject
{
    public static TestEnumValueObject New { get; } = new(nameof(New), 1);

    [FinalStatus]
    public static TestEnumValueObject Final { get; } = new TestEnumValueObject(nameof(Final), 100);

    private TestEnumValueObject(string name, int value) : base(name, value)
    {
    }
}