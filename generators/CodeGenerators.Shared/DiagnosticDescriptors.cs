namespace CodeGenerators.Shared;

using Microsoft.CodeAnalysis;

/// <summary>
/// Описатели всех диагностических сообщений
/// </summary>
public static class DiagnosticDescriptors
{
    private const string Category = "Usage";

    /// <summary>
    /// Ошибка геренации кода
    /// </summary>
    public const string GeneratorErrorRuleId = "CODEGEN000";
    /// <summary>
    /// Отсутствует модификатор sealed partial
    /// </summary>
    public const string SealedRuleId = "CODEGEN001";
    /// <summary>
    /// Некорректное имя свойства
    /// </summary>
    public const string NamingRuleId = "CODEGEN002";
    /// <summary>
    /// Значение уже определено
    /// </summary>
    public const string DupplicateValueRuleId = "CODEGEN003";
    /// <summary>
    /// Конструктор должен быть закрыт
    /// </summary>
    public const string PrivateCtorRuleId = "CODEGEN004";
    /// <summary>
    /// Именованные аргументы не поддерживаются
    /// </summary>
    public const string PositionalArgOnlyRuleId = "CODEGEN005";

    /// <summary>
    /// <see cref="GeneratorErrorRuleId"/>
    /// </summary>
    public static readonly DiagnosticDescriptor GeneratorErrorRule = new DiagnosticDescriptor(GeneratorErrorRuleId,
        "Ошибка геренации кода",
        "При генерации файла '{0}', произошла ошибка: {1}",
        "Usage",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "При генерации кода возникло исключение.");

    /// <summary>
    /// <see cref="NamingRuleId"/>
    /// </summary>
    public static readonly DiagnosticDescriptor NamingRule = new DiagnosticDescriptor(NamingRuleId,
        "Некорректное имя свойства",
        "Свойство '{0}' имеет некорректное наименование '{1}'",
        Category,
        DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Наименование экземпляра EnumvValueObject должно совпадать с имменем свойства.");

    /// <summary>
    /// <see cref="SealedRuleId"/>
    /// </summary>
    public static readonly DiagnosticDescriptor SealedRule = new DiagnosticDescriptor(SealedRuleId,
        "Отсутствует модификатор sealed partial",
        "Класс '{0}' должен иметь модификатор sealed partial",
        Category,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Экземляры EnumvValueObject не должны иметь наследников и иметь возможность к расширению через генерацию кода.");

    /// <summary>
    /// <see cref="DupplicateValueRuleId"/>
    /// </summary>
    public static readonly DiagnosticDescriptor DupplicateValueRule = new DiagnosticDescriptor(DupplicateValueRuleId,
        "Значение уже определено",
        "Свойство '{0}' имеет дублирующее значение: '{1}'",
        Category,
        DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Наименование экземпляра EnumvValueObject должно быть уникальным.");
    /// <summary>
    /// <see cref="PrivateCtorRuleId"/>
    /// </summary>
    public static readonly DiagnosticDescriptor PrivateCtorRule = new DiagnosticDescriptor(PrivateCtorRuleId,
        "Конструктор должен быть закрыт",
        "Конструктор класса должен иметь модификатор private",
        Category,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Конструктор EnumvValueObject должен быть закрыт.");
    /// <summary>
    /// <see cref="PositionalArgOnlyRuleId"/>
    /// </summary>
    public static readonly DiagnosticDescriptor PositionalArgOnlyRule = new DiagnosticDescriptor(PositionalArgOnlyRuleId,
        "Именованные аргументы не поддерживаются",
        "Инициализатор свойства '{0}' использует именованный аргумент: '{1}'",
        Category,
        DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "При инициализации свойств EnumvValueObject должны использоваться только позиционные параметры.");
}