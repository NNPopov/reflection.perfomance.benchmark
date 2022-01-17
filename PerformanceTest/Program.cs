using BenchmarkDotNet.Running;
using PerformanceTest.FillingObjectProperties;

BenchmarkRunner.Run<FillingObjectPropertiesBenchmark>();
Console.ReadKey();