/*
    Copyright(c) 2017 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using RageLib.Data;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace RageLib.Resources.GTA5.PC.Bounds
{
    public interface IBoundPrimitive
    {
        public ref BoundPrimitive AsRawData();
    }

    public enum BoundPrimitiveType : byte
    {
        Triangle = 0,
        Sphere = 1,
        Capsule = 2,
        Box = 3,
        Cylinder = 4,
    }

    // phPrimitiveBase
    // phPrimitive
    public struct BoundPrimitive : IResourceStruct<BoundPrimitive>
    {
        public uint Data0;
        public uint Data1;
        public uint Data2;
        public uint Data3;

        public BoundPrimitiveType PrimitiveType 
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (BoundPrimitiveType)(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref this, 1))[0] & 0x7); 
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref BoundPrimitiveTriangle AsTriangle() => ref MemoryMarshal.AsRef<BoundPrimitiveTriangle>(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref this, 1)));
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref BoundPrimitiveSphere AsSphere() => ref MemoryMarshal.AsRef<BoundPrimitiveSphere>(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref this, 1)));
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref BoundPrimitiveCapsule AsCapsule() => ref MemoryMarshal.AsRef<BoundPrimitiveCapsule>(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref this, 1)));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref BoundPrimitiveBox AsBox() => ref MemoryMarshal.AsRef<BoundPrimitiveBox>(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref this, 1)));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref BoundPrimitiveCylinder AsCylinder() => ref MemoryMarshal.AsRef<BoundPrimitiveCylinder>(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref this, 1)));

        public BoundPrimitive ReverseEndianness()
        {
            return PrimitiveType switch
            {
                BoundPrimitiveType.Triangle => AsTriangle().ReverseEndianness().AsRawData(),
                BoundPrimitiveType.Sphere => AsSphere().ReverseEndianness().AsRawData(),
                BoundPrimitiveType.Capsule => AsSphere().ReverseEndianness().AsRawData(),
                BoundPrimitiveType.Box => AsBox().ReverseEndianness().AsRawData(),
                BoundPrimitiveType.Cylinder => AsCylinder().ReverseEndianness().AsRawData(),
                _ => this,
            };
        }
    }

    // PrimitiveTriangle
    public struct BoundPrimitiveTriangle : IBoundPrimitive, IResourceStruct<BoundPrimitiveTriangle>
    {
        // structure data
        public int TypeAndArea;
        public ushort VertexIndexAndFlag1;
        public ushort VertexIndexAndFlag2;
        public ushort VertexIndexAndFlag3;
        public short NeighborIndex1;
        public short NeighborIndex2;
        public short NeighborIndex3;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref BoundPrimitive AsRawData() => ref MemoryMarshal.AsRef<BoundPrimitive>(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref this, 1)));

        public float Area
        {
            get => BitConverter.Int32BitsToSingle((int)(TypeAndArea & 0xFFFFFFF8));
            set => TypeAndArea = (int)(BitConverter.SingleToInt32Bits(value) & 0xFFFFFFF8);
        }

        public ushort VertexIndex1
        {
            get => (ushort)(VertexIndexAndFlag1 & 0x7FFF);
            set => VertexIndexAndFlag1 = (ushort)((VertexIndexAndFlag1 & 0x8000) | (value & 0x7FFF));
        }

        public ushort VertexIndex2
        {
            get => (ushort)(VertexIndexAndFlag2 & 0x7FFF);
            set => VertexIndexAndFlag2 = (ushort)((VertexIndexAndFlag2 & 0x8000) | (value & 0x7FFF));
        }

        public ushort VertexIndex3
        {
            get => (ushort)(VertexIndexAndFlag3 & 0x7FFF);
            set => VertexIndexAndFlag3 = (ushort)((VertexIndexAndFlag3 & 0x8000) | (value & 0x7FFF));
        }

        public bool VertexFlag1
        {
            get => (VertexIndexAndFlag1 & 0x8000) == 0x8000;
            set
            {
                if (value)
                    VertexIndexAndFlag1 |= 0x8000;
                else
                    VertexIndexAndFlag1 &= 0x7FFF;
            }
        }

        public bool VertexFlag2
        {
            get => (VertexIndexAndFlag2 & 0x8000) == 0x8000;
            set
            {
                if (value)
                    VertexIndexAndFlag2 |= 0x8000;
                else
                    VertexIndexAndFlag2 &= 0x7FFF;
            }
        }

        public bool VertexFlag3
        {
            get => (VertexIndexAndFlag3 & 0x8000) == 0x8000;
            set
            {
                if (value)
                    VertexIndexAndFlag3 |= 0x8000;
                else
                    VertexIndexAndFlag3 &= 0x7FFF;
            }
        }

        public BoundPrimitiveTriangle ReverseEndianness()
        {
            return new BoundPrimitiveTriangle()
            {
                TypeAndArea = EndiannessExtensions.ReverseEndianness(TypeAndArea),
                VertexIndexAndFlag1 = EndiannessExtensions.ReverseEndianness(VertexIndexAndFlag1),
                VertexIndexAndFlag2 = EndiannessExtensions.ReverseEndianness(VertexIndexAndFlag2),
                VertexIndexAndFlag3 = EndiannessExtensions.ReverseEndianness(VertexIndexAndFlag3),
                NeighborIndex1 = EndiannessExtensions.ReverseEndianness(NeighborIndex1),
                NeighborIndex2 = EndiannessExtensions.ReverseEndianness(NeighborIndex2),
                NeighborIndex3 = EndiannessExtensions.ReverseEndianness(NeighborIndex3),
            };
        }
    }

    // PrimitiveSphere
    public struct BoundPrimitiveSphere : IBoundPrimitive, IResourceStruct<BoundPrimitiveSphere>
    {
        // structure data
        public ushort Type;
        public ushort VertexIndex;
        public float Radius;
        public uint Unknown_8h;
        public uint Unknown_Ch;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref BoundPrimitive AsRawData() => ref MemoryMarshal.AsRef<BoundPrimitive>(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref this, 1)));

        public BoundPrimitiveSphere ReverseEndianness()
        {
            return new BoundPrimitiveSphere()
            {
                Type = EndiannessExtensions.ReverseEndianness(Type),
                VertexIndex = EndiannessExtensions.ReverseEndianness(VertexIndex),
                Radius = EndiannessExtensions.ReverseEndianness(Radius),
                Unknown_8h = EndiannessExtensions.ReverseEndianness(Unknown_8h),
                Unknown_Ch = EndiannessExtensions.ReverseEndianness(Unknown_Ch),
            };
        }
    }

    // PrimitiveCapsule
    public struct BoundPrimitiveCapsule : IBoundPrimitive, IResourceStruct<BoundPrimitiveCapsule>
    {
        // structure data
        public ushort Type;
        public ushort VertexIndex1;
        public float Radius;
        public ushort VertexIndex2;
        public ushort Unknown_Ah;
        public uint Unknown_Ch;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref BoundPrimitive AsRawData() => ref MemoryMarshal.AsRef<BoundPrimitive>(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref this, 1)));

        public BoundPrimitiveCapsule ReverseEndianness()
        {
            return new BoundPrimitiveCapsule()
            {
                Type = EndiannessExtensions.ReverseEndianness(Type),
                VertexIndex1 = EndiannessExtensions.ReverseEndianness(VertexIndex1),
                Radius = EndiannessExtensions.ReverseEndianness(Radius),
                VertexIndex2 = EndiannessExtensions.ReverseEndianness(VertexIndex2),
                Unknown_Ah = EndiannessExtensions.ReverseEndianness(Unknown_Ah),
                Unknown_Ch = EndiannessExtensions.ReverseEndianness(Unknown_Ch),
            };
        }
    }

    // PrimitiveBox
    public struct BoundPrimitiveBox : IBoundPrimitive, IResourceStruct<BoundPrimitiveBox>
    {
        // structure data
        public uint Type;
        public short VertexIndex1;
        public short VertexIndex2;
        public short VertexIndex3;
        public short VertexIndex4;
        public uint Unknown_Ch;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref BoundPrimitive AsRawData() => ref MemoryMarshal.AsRef<BoundPrimitive>(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref this, 1)));

        public BoundPrimitiveBox ReverseEndianness()
        {
            return new BoundPrimitiveBox()
            {
                Type = EndiannessExtensions.ReverseEndianness(Type),
                VertexIndex1 = EndiannessExtensions.ReverseEndianness(VertexIndex1),
                VertexIndex2 = EndiannessExtensions.ReverseEndianness(VertexIndex2),
                VertexIndex3 = EndiannessExtensions.ReverseEndianness(VertexIndex3),
                VertexIndex4 = EndiannessExtensions.ReverseEndianness(VertexIndex4),
                Unknown_Ch = EndiannessExtensions.ReverseEndianness(Unknown_Ch),
            };
        }
    }

    // PrimitiveCylinder
    public struct BoundPrimitiveCylinder : IBoundPrimitive, IResourceStruct<BoundPrimitiveCylinder>
    {
        // structure data
        public ushort Type;
        public ushort VertexIndex1;
        public float Radius;
        public ushort VertexIndex2;
        public ushort Unknown_Ah;
        public uint Unknown_Ch;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref BoundPrimitive AsRawData() => ref MemoryMarshal.AsRef<BoundPrimitive>(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref this, 1)));

        public BoundPrimitiveCylinder ReverseEndianness()
        {
            return new BoundPrimitiveCylinder()
            {
                Type = EndiannessExtensions.ReverseEndianness(Type),
                VertexIndex1 = EndiannessExtensions.ReverseEndianness(VertexIndex1),
                Radius = EndiannessExtensions.ReverseEndianness(Radius),
                VertexIndex2 = EndiannessExtensions.ReverseEndianness(VertexIndex2),
                Unknown_Ah = EndiannessExtensions.ReverseEndianness(Unknown_Ah),
                Unknown_Ch = EndiannessExtensions.ReverseEndianness(Unknown_Ch),
            };
        }
    }
}
