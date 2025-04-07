#nullable enable
#pragma warning disable CS0660, CS0661
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CodeGenerators.Utils;
namespace TEMPLATE_NamespaceName
{
    partial class TEMPLATE_ClassName :
        IEquatable<TEMPLATE_ClassName>,
        IEqualityComparer<TEMPLATE_ClassName>,
        IComparable<TEMPLATE_ClassName>,
        IComparable
    {
        private static bool InternalEquals(TEMPLATE_ClassName? left, TEMPLATE_ClassName? right)
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

        private static int InternalCompare(TEMPLATE_ClassName? left, TEMPLATE_ClassName? right)
        {
            if (ReferenceEquals(left, right))
            {
                return 0;
            }
            if (right is null)
            {
                return 1;
            }
            if (left is null)
            {
                return -1;
            }

            return left.Value.CompareTo(right.Value);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return InternalEquals(this, obj as TEMPLATE_ClassName);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <inheritdoc />
        public int CompareTo(TEMPLATE_ClassName? other)
        {
            if (other is null)
            {
                return 1;
            }

            return this.Value.CompareTo(other.Value);
        }

        /// <inheritdoc />
        public int CompareTo(object? obj)
        {
            return CompareTo(obj as TEMPLATE_ClassName);
        }

        /// <inheritdoc />
        public bool Equals(TEMPLATE_ClassName? other)
        {
            return InternalEquals(this, other);
        }

        /// <inheritdoc />
        public bool Equals(TEMPLATE_ClassName? x, TEMPLATE_ClassName? y)
        {
            return InternalEquals(x, y);
        }

        /// <inheritdoc />
        public int GetHashCode(TEMPLATE_ClassName obj)
        {
            return obj.Value.GetHashCode();
        }

        public static bool operator ==(TEMPLATE_ClassName? left, TEMPLATE_ClassName? right)
        {
            return InternalEquals(left, right);
        }

        public static bool operator !=(TEMPLATE_ClassName? left, TEMPLATE_ClassName? right)
        {
            return !InternalEquals(left, right);
        }

        public static bool operator <(TEMPLATE_ClassName? left, TEMPLATE_ClassName? right)
        {
            return InternalCompare(left, right) < 0;
        }
        public static bool operator <=(TEMPLATE_ClassName? left, TEMPLATE_ClassName? right)
        {
            return InternalCompare(left, right) <= 0;
        }

        public static bool operator >(TEMPLATE_ClassName? left, TEMPLATE_ClassName? right)
        {
            return InternalCompare(left, right) > 0;
        }

        public static bool operator >=(TEMPLATE_ClassName? left, TEMPLATE_ClassName? right)
        {
            return InternalCompare(left, right) >= 0;
        }
    }
}
