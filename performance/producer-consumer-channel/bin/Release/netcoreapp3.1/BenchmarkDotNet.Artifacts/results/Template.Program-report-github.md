``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i5 CPU M 540 2.53GHz, 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.1.100
  [Host]     : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT
  DefaultJob : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT


```
|        Method |    Mean |    Error |   StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |--------:|---------:|---------:|------:|------:|------:|----------:|
| WriteThenRead | 1.157 s | 0.0260 s | 0.0309 s |     - |     - |     - |         - |
