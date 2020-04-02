# ArtNetDotNet
A performance focused .NET ArtNet library 

This was mostly written as a test to play with structs in .NET but turned out to be kind of useful where you need to quickly and easily generate Art-Net packets.  There are methods to convert to byte array via Marshal and BinaryWriter.  See benchmarks below.

``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17134.829 (1803/April2018Update/Redstone4)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.1.104
  [Host]     : .NET Core 2.0.6 (CoreCLR 4.6.26212.01, CoreFX 4.6.26212.01), 64bit RyuJIT
  Job-PLJXNI : .NET Core 2.0.6 (CoreCLR 4.6.26212.01, CoreFX 4.6.26212.01), 64bit RyuJIT

InvocationCount=100000  UnrollFactor=1  

```
|                    Method |     Mean |     Error |   StdDev |
|-------------------------- |---------:|----------:|---------:|
|      GetBytesUsingMarshal | 608.9 ns | 13.050 ns | 37.44 ns |
| GetBytesUsingBinaryWriter | 325.4 ns |  8.082 ns | 23.19 ns |

A "real world" test using a UDP socket and [The ArtNetominator](https://www.lightjams.com/artnetominator/) generated about 600Mbps of traffic and ~90k ArtNet packets/s on my system.
