using System;
using System.Runtime.CompilerServices;

namespace RageLib.Numerics
{
    /// <summary>
    /// Represents a Jenkins's one_at_a_time hash
    /// </summary>
    public readonly struct JoaatHash : IComparable<JoaatHash>, IEquatable<JoaatHash>
    {
        private readonly uint value;

        public JoaatHash(uint hash)
        {
            value = hash;
        }

        public int CompareTo(JoaatHash other) => value.CompareTo(other.value);

        public bool Equals(JoaatHash other) => this == other;

        public override bool Equals(object obj) => (obj is JoaatHash other) && Equals(other);

        public override int GetHashCode() => (int)value;

        public override string ToString() => value.ToString("X8");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(JoaatHash left, JoaatHash right) => (left.value == right.value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(JoaatHash left, JoaatHash right) => (left.value != right.value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator uint(JoaatHash hash) => hash.value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator JoaatHash(uint hash) => new JoaatHash(hash);
    }
}
