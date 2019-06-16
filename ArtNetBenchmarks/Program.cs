using System;
using BenchmarkDotNet.Running;

namespace ArtNetBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run(typeof(DmxPacketBenchmarks));
        }
    }
}
