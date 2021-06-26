using System;
using System.Globalization;
using System.Numerics;
using System.Xml;

namespace RageLib.Helpers.Xml
{
    public static class XmlWriterExtensions
    {
        public static void WriteAttributeValue(this XmlWriter writer, bool value)
        {
            writer.WriteAttributeString("value", value ? "true" : "false");
        }

        public static void WriteAttributeValue(this XmlWriter writer, byte value)
        {
            writer.WriteAttributeString("value", value.ToString(CultureInfo.InvariantCulture));
        }

        public static void WriteAttributeValue(this XmlWriter writer, sbyte value)
        {
            writer.WriteAttributeString("value", value.ToString(CultureInfo.InvariantCulture));
        }

        public static void WriteAttributeValue(this XmlWriter writer, short value)
        {
            writer.WriteAttributeString("value", value.ToString(CultureInfo.InvariantCulture));
        }

        public static void WriteAttributeValue(this XmlWriter writer, ushort value)
        {
            writer.WriteAttributeString("value", value.ToString(CultureInfo.InvariantCulture));
        }

        public static void WriteAttributeValue(this XmlWriter writer, int value)
        {
            writer.WriteAttributeString("value", value.ToString(CultureInfo.InvariantCulture));
        }

        public static void WriteAttributeValue(this XmlWriter writer, uint value)
        {
            writer.WriteAttributeString("value", value.ToString(CultureInfo.InvariantCulture));
        }

        public static void WriteAttributeValue(this XmlWriter writer, Half value)
        {
            var s1 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", value);
            writer.WriteAttributeString("value", s1);
        }

        public static void WriteAttributeValue(this XmlWriter writer, float value)
        {
            var s1 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", value);
            writer.WriteAttributeString("value", s1);
        }

        public static void WriteAttributeValue(this XmlWriter writer, double value)
        {
            var s1 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", value);
            writer.WriteAttributeString("value", s1);
        }

        public static void WriteAttributesXY(this XmlWriter writer, Vector2 value)
        {
            var s1 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", value.X);
            var s2 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", value.Y);
            writer.WriteAttributeString("x", s1);
            writer.WriteAttributeString("y", s2);
        }

        public static void WriteAttributesXYZ(this XmlWriter writer, Vector3 value)
        {
            var s1 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", value.X);
            var s2 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", value.Y);
            var s3 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", value.Z);
            writer.WriteAttributeString("x", s1);
            writer.WriteAttributeString("y", s2);
            writer.WriteAttributeString("z", s3);
        }

        public static void WriteAttributesXYZW(this XmlWriter writer, Vector4 value)
        {
            var s1 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", value.X);
            var s2 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", value.Y);
            var s3 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", value.Z);
            var s4 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", value.W);
            writer.WriteAttributeString("x", s1);
            writer.WriteAttributeString("y", s2);
            writer.WriteAttributeString("z", s3);
            writer.WriteAttributeString("w", s4);
        }

        public static void WriteAttributesXYZW(this XmlWriter writer, Quaternion value)
        {
            var s1 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", value.X);
            var s2 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", value.Y);
            var s3 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", value.Z);
            var s4 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", value.W);
            writer.WriteAttributeString("x", s1);
            writer.WriteAttributeString("y", s2);
            writer.WriteAttributeString("z", s3);
            writer.WriteAttributeString("w", s4);
        }

        public static void WriteAttributeValueAsHex(this XmlWriter writer, uint value)
        {
            writer.WriteAttributeString("value", $"0x{value:X8}");
        }
    }
}
