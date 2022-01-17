using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using PerformanceTest.FillingObjectProperties.DataProcessors;
using PerformanceTest.FillingObjectProperties.Entities;

namespace PerformanceTest.FillingObjectProperties;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class FillingObjectPropertiesBenchmark
{
    [Benchmark]
    public void TestMethod0_1()
    {
        var benchmarkData = new TestData();

        var processDataMethod = new ProcessDataMethod0<TestEntity>();
        var result = processDataMethod.ProcessData(benchmarkData);
        result.EnsureDataFilled();
    }

    [Benchmark]
    public void TestMethod0_1K()
    {
        var benchmarkData = new TestData();

        for (var i = 0; i < 1000; i++)
        {
            var processDataMethod = new ProcessDataMethod0<TestEntity>();
            var result = processDataMethod.ProcessData(benchmarkData);
            result.EnsureDataFilled();
        }
    }

    [Benchmark]
    public void TestMethod0_10K()
    {
        var benchmarkData = new TestData();

        for (var i = 0; i < 10000; i++)
        {
            var processDataMethod = new ProcessDataMethod0<TestEntity>();
            var result = processDataMethod.ProcessData(benchmarkData);
            result.EnsureDataFilled();
        }
    }

    [Benchmark]
    public void TestMethod1_1()
    {
        var benchmarkData = new TestData();

        var processDataMethod = new ProcessDataMethod1<TestEntity>();
        var result = processDataMethod.ProcessData(benchmarkData);
        result.EnsureDataFilled();
    }

    [Benchmark]
    public void TestMethod1_1K()
    {
        var benchmarkData = new TestData();

        for (var i = 0; i < 1000; i++)
        {
            var processDataMethod = new ProcessDataMethod1<TestEntity>();
            var result = processDataMethod.ProcessData(benchmarkData);
            result.EnsureDataFilled();
        }
    }

    [Benchmark]
    public void TestMethod1_10K()
    {
        var benchmarkData = new TestData();

        for (var i = 0; i < 10000; i++)
        {
            var processDataMethod = new ProcessDataMethod1<TestEntity>();
            var result = processDataMethod.ProcessData(benchmarkData);
            result.EnsureDataFilled();
        }
    }

    [Benchmark]
    public void TestMethod2_1()
    {
        var benchmarkData = new TestData();

        var processDataMethod = new ProcessDataMethod2<TestEntity>();
        var result = processDataMethod.ProcessData(benchmarkData);
        result.EnsureDataFilled();
    }

    [Benchmark]
    public void TestMethod2_1K()
    {
        var benchmarkData = new TestData();

        for (var i = 0; i < 1000; i++)
        {
            var processDataMethod = new ProcessDataMethod2<TestEntity>();
            var result = processDataMethod.ProcessData(benchmarkData);
            result.EnsureDataFilled();
        }
    }

    [Benchmark]
    public void TestMethod2_10K()
    {
        var benchmarkData = new TestData();

        for (var i = 0; i < 10000; i++)
        {
            var processDataMethod = new ProcessDataMethod2<TestEntity>();
            var result = processDataMethod.ProcessData(benchmarkData);
            result.EnsureDataFilled();
        }
    }

    [Benchmark]
    public void TestMethod3_1()
    {
        var benchmarkData = new TestData();

        var processDataMethod = new ProcessDataMethod3<TestEntity>();
        var result = processDataMethod.ProcessData(benchmarkData);
        result.EnsureDataFilled();
    }

    [Benchmark]
    public void TestMethod3_1K()
    {
        var benchmarkData = new TestData();

        for (var i = 0; i < 1000; i++)
        {
            var processDataMethod = new ProcessDataMethod3<TestEntity>();
            var result = processDataMethod.ProcessData(benchmarkData);
            result.EnsureDataFilled();
        }
    }

    [Benchmark]
    public void TestMethod3_10K()
    {
        var benchmarkData = new TestData();

        for (var i = 0; i < 10000; i++)
        {
            var processDataMethod = new ProcessDataMethod3<TestEntity>();
            var result = processDataMethod.ProcessData(benchmarkData);
            result.EnsureDataFilled();
        }
    }
}