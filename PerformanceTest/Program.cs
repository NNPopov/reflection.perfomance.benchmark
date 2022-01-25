using BenchmarkDotNet.Running;
using PerformanceTest.FillingObjectProperties;
using PerformanceTest.ObjectInitializer;

//BenchmarkRunner.Run<FillingObjectPropertiesBenchmark>();

BenchmarkRunner.Run<ObjectInitializersBenchmark>();
Console.ReadKey();