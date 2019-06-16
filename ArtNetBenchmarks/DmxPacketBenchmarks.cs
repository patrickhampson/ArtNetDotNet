using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using ArtNetLib;
using BenchmarkDotNet.Attributes;

namespace ArtNetBenchmarks
{
    [InvocationCount(100000)]
    public class DmxPacketBenchmarks
    {
        private ArtNetDmxPacket packet;

        public DmxPacketBenchmarks()
        {
            var random = new Random();
            var dmxData = new byte[512];
            random.NextBytes(dmxData);

            packet = new ArtNetDmxPacket(0x10, 512, 0, 0, dmxData);
        }

        [Benchmark]
        public byte[] GetBytesUsingMarshal()
        {
            int size = Marshal.SizeOf(packet);
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(packet, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }

        [Benchmark]
        public byte[] GetBytesUsingBinaryWriter()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms, Encoding.UTF8))
                {
                    bw.Write(packet.Id);
                    bw.Write(packet.OpCode);
                    bw.Write(packet.ProtVerHi);
                    bw.Write(packet.ProtVerLo);
                    bw.Write(packet.Sequence);
                    bw.Write(packet.Physical);
                    bw.Write(packet.SubUni);
                    bw.Write(packet.Net);
                    bw.Write(packet.LengthHi);
                    bw.Write(packet.LengthLo);
                    bw.Write(packet.Data);

                    return ms.ToArray();
                }
            }
        }

    }
}
