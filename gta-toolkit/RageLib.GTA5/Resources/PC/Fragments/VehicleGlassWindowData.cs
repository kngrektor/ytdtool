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

using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Fragments
{
    // fwVehicleGlassWindowData
    public class VehicleGlassWindowData : ResourceSystemBlock
    {
        public override long BlockLength => 0x10 + Chunk.Length;

        // structure data
        public uint Id; // VGWH: 0x56475748
        public ushort Unknown_4h;
        public ushort Count;
        public uint ChunkSize;
        public uint Unknown_Ch;
        public byte[] Chunk;

        public VehicleGlassWindow[] Windows { get; private set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Id = reader.ReadUInt32();
            this.Unknown_4h = reader.ReadUInt16();
            this.Count = reader.ReadUInt16();
            this.ChunkSize = reader.ReadUInt32();
            this.Unknown_Ch = reader.ReadUInt32();
            this.Chunk = reader.ReadBytes((int)ChunkSize - 16);

            // TODO:    rework this to avoid reading data twice
            //          Design a packed data class maybe?
            UnpackChunk(reader);
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.Id);
            writer.Write(this.Unknown_4h);
            writer.Write(this.Count);
            writer.Write(this.ChunkSize);
            writer.Write(this.Unknown_Ch);
            writer.Write(this.Chunk);
        }

        private void UnpackChunk(ResourceDataReader reader)
        {
            var position = reader.Position;
            reader.Position -= ChunkSize;
            reader.Position += 16;

            ValueTuple<uint, uint>[] offsets = new ValueTuple<uint, uint>[Count];
            for (int i = 0; i < Count; i++)
            {
                var offset = reader.ReadUInt32();
                var unknown = reader.ReadUInt32();
                offsets[i] = (offset, unknown);
            }

            Windows = new VehicleGlassWindow[Count];
            for (int i = 0; i < Count; i++)
            {
                Windows[i] = reader.ReadBlock<VehicleGlassWindow>();
            }

            reader.Position = position;
        }
    }

    // fwVehicleGlassWindow
    public class VehicleGlassWindow : ResourceSystemBlock
    {
        public override long BlockLength => 0x70 + Chunk.Length;

        public Matrix4x4 ProjectionMatrix;
        public uint Id; // VGWC: 0x56475743
        public ushort GroupIndex;
        public ushort Unknown_46h;
        public ushort Unknown_48h;
        public ushort Unknown_4Ah;
        public ushort ChunkSize;
        public ushort Unknown_4Eh;
        public uint Unknown_50h;
        public uint Unknown_54h;
        public float Unknown_58h;
        public float Unknown_5Ch;
        public uint Unknown_60h;
        public float Unknown_64h;
        public uint Unknown_68h;
        public uint Unknown_6Ch;
        public byte[] Chunk;

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            ProjectionMatrix = reader.ReadMatrix4x4();
            Id = reader.ReadUInt32();
            GroupIndex = reader.ReadUInt16();
            Unknown_46h = reader.ReadUInt16();
            Unknown_48h = reader.ReadUInt16();
            Unknown_4Ah = reader.ReadUInt16();
            ChunkSize = reader.ReadUInt16();
            Unknown_4Eh = reader.ReadUInt16();
            Unknown_50h = reader.ReadUInt32();
            Unknown_54h = reader.ReadUInt32();
            Unknown_58h = reader.ReadSingle();
            Unknown_5Ch = reader.ReadSingle();
            Unknown_60h = reader.ReadUInt32();
            Unknown_64h = reader.ReadSingle();
            Unknown_68h = reader.ReadUInt32();
            Unknown_6Ch = reader.ReadUInt32();
            Chunk = reader.ReadBytes(ChunkSize);

            // Skip padding
            reader.ReadBytes((16 - (ChunkSize % 16)) % 16);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            writer.Write(ProjectionMatrix);
            writer.Write(Id);
            writer.Write(GroupIndex);
            writer.Write(Unknown_46h);
            writer.Write(Unknown_48h);
            writer.Write(Unknown_4Ah);
            writer.Write(ChunkSize);
            writer.Write(Unknown_4Eh);
            writer.Write(Unknown_50h);
            writer.Write(Unknown_54h);
            writer.Write(Unknown_58h);
            writer.Write(Unknown_5Ch);
            writer.Write(Unknown_60h);
            writer.Write(Unknown_64h);
            writer.Write(Unknown_68h);
            writer.Write(Unknown_6Ch);
            writer.Write(Chunk);
            
            // write padding
            var padding = (16 - (ChunkSize % 16)) % 16;
            for (int i = 0; i < padding; i++)
                writer.Write((byte)0);
        }
    }
}
