#nullable enable
#pragma warning disable CS0660, CS0661
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using CodeGenerators.Utils;
namespace TEMPLATE_NamespaceName
{
    partial class TEMPLATE_ClassName : IEnumValueObjectParser<TEMPLATE_ClassName>,
        IHavePossibleValues,
        IEqualityComparer<TEMPLATE_ClassName>,
        IEquatable<TEMPLATE_ClassName>
    {
        public enum TEMPLATE_InternalEnumName : TEMPLATE_NumericType
        {
TEMPLATE_EnumBody
        }

        public TEMPLATE_ClassName.TEMPLATE_InternalEnumName ToEnum()
        {
            return (TEMPLATE_ClassName.TEMPLATE_InternalEnumName)this.Value;
        }

        /// <inheritdoc />
        public static string PossibleValues { get; } = @"TEMPLATE_PossibleValues";

        /// <inheritdoc />
        public static Expression<Func<TEMPLATE_NumericType?, TEMPLATE_ClassName?>> TryParseExpression { get; } = x => TEMPLATE_ClassName.TryParse(x);

        /// <summary>
        /// Получить экземпляр <see cref="TEMPLATE_ClassName"/> из числового значения
        /// </summary>
        /// <param name="numValue">Числовое значение</param>
        /// <exception cref="ArgumentNullException">Значение равно null</exception>
        /// <exception cref="ArgumentOutOfRangeException">Значение не определено</exception>
        [return: NotNullIfNotNull(nameof(numValue))]
        public static TEMPLATE_ClassName Parse([NotNull]TEMPLATE_NumericType? numValue)
        {
            if (numValue == null)
            {
                throw new ArgumentNullException(nameof(numValue), "Значение равно null");
            }

TEMPLATE_NumberParseBody
            throw new ArgumentOutOfRangeException(nameof(numValue), "Значение не определено");
        }

        /// <summary>
        /// Получить экземпляр <see cref="TEMPLATE_ClassName"/> из строкового значения
        /// </summary>
        /// <param name="strValue">Строковое значение</param>
        /// <exception cref="ArgumentNullException">Значение равно null</exception>
        /// <exception cref="ArgumentOutOfRangeException">Значение не определено</exception>
        [return: NotNullIfNotNull(nameof(strValue))]
        public static TEMPLATE_ClassName Parse([NotNull]string? strValue)
        {
            if (strValue == null)
            {
                throw new ArgumentNullException(nameof(strValue), "Значение равно null");
            }

TEMPLATE_StringParseBody
            throw new ArgumentOutOfRangeException(nameof(strValue), "Значение не определено");
        }

        /// <summary>
        /// Получить экземпляр <see cref="TEMPLATE_ClassName"/> из <see cref="TEMPLATE_ClassName.TEMPLATE_InternalEnumName"/>
        /// </summary>
        /// <param name="enumValue">Значение перечисления</param>
        /// <exception cref="ArgumentNullException">Значение равно null</exception>
        /// <exception cref="ArgumentOutOfRangeException">Значение не определено</exception>
        [return: NotNullIfNotNull(nameof(enumValue))]
        public static TEMPLATE_ClassName Parse([NotNull]TEMPLATE_ClassName.TEMPLATE_InternalEnumName? enumValue)
        {
            if (enumValue == null)
            {
                throw new ArgumentNullException(nameof(enumValue), "Значение равно null");
            }
            switch(enumValue)
            {
TEMPLATE_EnumParseBody
                default:
                    throw new ArgumentOutOfRangeException(nameof(enumValue), "Значение не определено");
            }
        }

        /// <summary>
        /// Получить экземпляр <see cref="TEMPLATE_ClassName"/> из числового значения
        /// </summary>
        /// <param name="numValue">Числовое значение</param>
        public static TEMPLATE_ClassName? TryParse(TEMPLATE_NumericType? numValue)
        {
            if (numValue == null)
            {
                return null;
            }
            try
            {
                return Parse(numValue);
            }
            catch(ArgumentOutOfRangeException)
            {
                // ignore
            }
            return null;
        }

        /// <summary>
        /// Получить экземпляр <see cref="TEMPLATE_ClassName"/> из строкового значения
        /// </summary>
        /// <param name="strValue">Строковое значение</param>
        public static TEMPLATE_ClassName? TryParse(string? strValue)
        {
            if (strValue == null)
            {
                return null;
            }
            try
            {
                return Parse(strValue);
            }
            catch(ArgumentOutOfRangeException)
            {
                // ignore
            }
            return null;
        }

        /// <inheritdoc />
        public override bool IsFinal()
        {
            switch (this.Value)
            {
TEMPLATE_IsFinalBody
                default:
                    return false;
            }
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return TEMPLATE_NumericTypeernalEquals(this, obj as TEMPLATE_ClassName);
        }

        /// <inheritdoc />
        public override TEMPLATE_NumericType GetHashCode()
        {
            return Value.GetHashCode();
        }

        private static bool TEMPLATE_NumericTypeernalEquals(TEMPLATE_ClassName? left, TEMPLATE_ClassName? right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;

            return left.Value == right.Value;
        }

        public static bool operator ==(TEMPLATE_ClassName? left, TEMPLATE_ClassName? right)
        {
            // ReSharper disable once ArrangeStaticMemberQualifier
            return TEMPLATE_ClassName.TEMPLATE_NumericTypeernalEquals(left, right);
        }

        public static bool operator !=(TEMPLATE_ClassName? left, TEMPLATE_ClassName? right)
        {
            // ReSharper disable once ArrangeStaticMemberQualifier
            return !TEMPLATE_ClassName.TEMPLATE_NumericTypeernalEquals(left, right);
        }

        /// <inheritdoc />
        public bool Equals(TEMPLATE_ClassName? other)
        {
            return TEMPLATE_NumericTypeernalEquals(this, other);
        }

        /// <inheritdoc />
        public bool Equals(TEMPLATE_ClassName? x, TEMPLATE_ClassName? y)
        {
            return TEMPLATE_NumericTypeernalEquals(x, y);
        }

        /// <inheritdoc />
        public TEMPLATE_NumericType GetHashCode(TEMPLATE_ClassName obj)
        {
            return obj.Value.GetHashCode();
        }
    }
}
