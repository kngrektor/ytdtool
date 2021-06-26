using System;
using System.Globalization;
using System.Numerics;
using System.Xml;

namespace RageLib.Helpers.Xml
{
    public static class XmlReaderExtensions
    {
        public static bool GetAttributeValueAsBool(this XmlReader reader)
        {
            return bool.Parse(reader.GetAttribute("value"));
        }

        public static byte GetAttributeValueAsByte(this XmlReader reader)
        {
            return byte.Parse(reader.GetAttribute("value"), CultureInfo.InvariantCulture);
        }

        public static sbyte GetAttributeValueAsSByte(this XmlReader reader)
        {
            return sbyte.Parse(reader.GetAttribute("value"), CultureInfo.InvariantCulture);
        }

        public static short GetAttributeValueAsInt16(this XmlReader reader)
        {
            return short.Parse(reader.GetAttribute("value"), CultureInfo.InvariantCulture);
        }

        public static ushort GetAttributeValueAsUInt16(this XmlReader reader)
        {
            return ushort.Parse(reader.GetAttribute("value"), CultureInfo.InvariantCulture);
        }

        public static int GetAttributeValueAsInt32(this XmlReader reader)
        {
            return int.Parse(reader.GetAttribute("value"), CultureInfo.InvariantCulture);
        }

        public static uint GetAttributeValueAsUInt32(this XmlReader reader)
        {
            return uint.Parse(reader.GetAttribute("value"), CultureInfo.InvariantCulture);
        }

        public static Half GetAttributeValueAsHalf(this XmlReader reader)
        {
            return Half.Parse(reader.GetAttribute("value"), CultureInfo.InvariantCulture);
        }

        public static float GetAttributeValueAsFloat(this XmlReader reader)
        {
            return float.Parse(reader.GetAttribute("value"), CultureInfo.InvariantCulture);
        }

        public static double GetAttributeValueAsDouble(this XmlReader reader)
        {
            return double.Parse(reader.GetAttribute("value"), CultureInfo.InvariantCulture);
        }

        public static Vector2 GetAttributesXYAsVector2(this XmlReader reader)
        {
            float x = float.Parse(reader.GetAttribute("x"), CultureInfo.InvariantCulture);
            float y = float.Parse(reader.GetAttribute("y"), CultureInfo.InvariantCulture);
            return new Vector2(x, y);
        }

        public static Vector3 GetAttributesXYZAsVector3(this XmlReader reader)
        {
            float x = float.Parse(reader.GetAttribute("x"), CultureInfo.InvariantCulture);
            float y = float.Parse(reader.GetAttribute("y"), CultureInfo.InvariantCulture);
            float z = float.Parse(reader.GetAttribute("z"), CultureInfo.InvariantCulture);
            return new Vector3(x, y, z);
        }

        public static Vector4 GetAttributesXYZWAsVector4(this XmlReader reader)
        {
            float x = float.Parse(reader.GetAttribute("x"), CultureInfo.InvariantCulture);
            float y = float.Parse(reader.GetAttribute("y"), CultureInfo.InvariantCulture);
            float z = float.Parse(reader.GetAttribute("z"), CultureInfo.InvariantCulture);
            float w = float.Parse(reader.GetAttribute("w"), CultureInfo.InvariantCulture);
            return new Vector4(x, y, z, w);
        }

        public static Quaternion GetAttributesXYZWAsQuaternion(this XmlReader reader)
        {
            float x = float.Parse(reader.GetAttribute("x"), CultureInfo.InvariantCulture);
            float y = float.Parse(reader.GetAttribute("y"), CultureInfo.InvariantCulture);
            float z = float.Parse(reader.GetAttribute("z"), CultureInfo.InvariantCulture);
            float w = float.Parse(reader.GetAttribute("w"), CultureInfo.InvariantCulture);
            return new Quaternion(x, y, z, w);
        }

        public static uint GetAttributeValueAsUInt32FromHex(this XmlReader reader)
        {
            return Convert.ToUInt32(reader.GetAttribute("value"), 16);
        }
    }
}
