using System;

namespace RageLib.Numerics
{
    public static class FloatHelpers
    {
        public static float SignalingNaN = BitConverter.Int32BitsToSingle(0x7F800001);
        public static float QuietNaN = BitConverter.Int32BitsToSingle(0x7FC00000);
    }
}
