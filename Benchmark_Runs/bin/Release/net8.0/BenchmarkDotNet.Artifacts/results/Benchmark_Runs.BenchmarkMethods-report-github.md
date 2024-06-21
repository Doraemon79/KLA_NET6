```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3737/23H2/2023Update/SunValley3)
11th Gen Intel Core i7-11370H 3.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.300
  [Host]     : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI [AttachedDebugger]
  DefaultJob : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method                           | Mean     | Error     | StdDev    |
|--------------------------------- |---------:|----------:|----------:|
| RecursionToBenchmark             | 3.333 ms | 0.0655 ms | 0.1277 ms |
| ConverternNoRecursionToBenchmark | 5.190 ms | 0.1181 ms | 0.3390 ms |
| ConverterChatGPTToBenchmark      | 2.289 ms | 0.0472 ms | 0.1376 ms |
