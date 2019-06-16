using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ArtNetLib
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ArtNetDmxPacket
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Id;
        public short OpCode;
        public byte ProtVerHi;
        public byte ProtVerLo;
        public byte Sequence;
        public byte Physical;
        public byte SubUni;
        public byte Net;
        public byte LengthHi;
        public byte LengthLo;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public byte[] Data;

        public ArtNetDmxPacket(byte sequence, short length, byte subnet, byte universe, byte[] data)
        {
            Id = new byte[] { 0x41, 0x72, 0x74, 0x2d, 0x4e, 0x65, 0x74, 0 };
            OpCode = 20480;
            ProtVerHi = 0;
            ProtVerLo = 14;
            Sequence = sequence;  //0x00 to 0xFF
            Physical = 0;
            SubUni = universe;
            Net = subnet;
            LengthHi = (byte)(length >> 8);
            LengthLo = (byte)(length & 0xFF);
            Data = data;
        }

        public byte[] GetBytes()
        {
            int size = Marshal.SizeOf(this);
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(this, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }

        public byte[] GetBytesUsingBinaryWriter()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms, Encoding.UTF8))
                {
                    bw.Write(this.Id);
                    bw.Write(this.OpCode);
                    bw.Write(this.ProtVerHi);
                    bw.Write(this.ProtVerLo);
                    bw.Write(this.Sequence);
                    bw.Write(this.Physical);
                    bw.Write(this.SubUni);
                    bw.Write(this.Net);
                    bw.Write(this.LengthHi);
                    bw.Write(this.LengthLo);
                    bw.Write(this.Data);

                    return ms.ToArray();
                }
            }
        }
    }
}
