using RageLib.Data;
using RageLib.Resources;

namespace RageLib.Numerics
{
    public struct Matrix3x4 : IResourceStruct<Matrix3x4>
    {
        public float M11;
        public float M12;
        public float M13;
        public float M14;
        public float M21;
        public float M22;
        public float M23;
        public float M24;
        public float M31;
        public float M32;
        public float M33;
        public float M34;

        public Matrix3x4 ReverseEndianness()
        {
            return new Matrix3x4()
            {
                M11 = EndiannessExtensions.ReverseEndianness(M11),
                M12 = EndiannessExtensions.ReverseEndianness(M12),
                M13 = EndiannessExtensions.ReverseEndianness(M13),
                M14 = EndiannessExtensions.ReverseEndianness(M14),
                M21 = EndiannessExtensions.ReverseEndianness(M21),
                M22 = EndiannessExtensions.ReverseEndianness(M22),
                M23 = EndiannessExtensions.ReverseEndianness(M23),
                M24 = EndiannessExtensions.ReverseEndianness(M24),
                M31 = EndiannessExtensions.ReverseEndianness(M31),
                M32 = EndiannessExtensions.ReverseEndianness(M32),
                M33 = EndiannessExtensions.ReverseEndianness(M33),
                M34 = EndiannessExtensions.ReverseEndianness(M34),
            };
        }
    }
}
