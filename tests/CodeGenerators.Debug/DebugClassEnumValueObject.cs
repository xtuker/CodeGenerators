namespace CodeGenerators.Debug;

using CodeGenerators.Utils;

public sealed partial class DebugClassEnumValueObject : EnumValueObject
{
    public static DebugClassEnumValueObject New { get; } = new (nameof(New), 10);
    public static DebugClassEnumValueObject Final { get; } = new (nameof(Final), 12);

    private DebugClassEnumValueObject(string name, int value) : base(name, value)
    {
    }
}