using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace PerformanceTest.ObjectInitializer;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class ObjectInitializersBenchmark
{
    [Benchmark]
    public void TestMethod0_1()
    {
        ObjectInitializers.InitializeMethod0();
    }

    [Benchmark]
    public void TestMethod0_1K()
    {
        for (var i = 0; i < 1000; i++)
        {
            ObjectInitializers.InitializeMethod0();
        }
    }

    [Benchmark]
    public void TestMethod0_10K()
    {
        for (var i = 0; i < 10000; i++)
        {
            ObjectInitializers.InitializeMethod0();
        }
    }

    [Benchmark]
    public void TestMethod1_1()
    {
        ObjectInitializers.InitializeMethod1();
    }

    [Benchmark]
    public void TestMethod1_1K()
    {
        for (var i = 0; i < 1000; i++)
        {
            ObjectInitializers.InitializeMethod1();
        }
    }

    [Benchmark]
    public void TestMethod1_10K()
    {
        for (var i = 0; i < 10000; i++)
        {
            ObjectInitializers.InitializeMethod1();
        }
    }

    [Benchmark]
    public void TestMethod2_1()
    {
        ObjectInitializers.InitializeMethod2();
    }

    [Benchmark]
    public void TestMethod2_1K()
    {
        for (var i = 0; i < 1000; i++)
        {
            ObjectInitializers.InitializeMethod2();
        }
    }

    [Benchmark]
    public void TestMethod2_10K()
    {
        for (var i = 0; i < 10000; i++)
        {
            ObjectInitializers.InitializeMethod2();
        }
    }

    [Benchmark]
    public void TestMethod3_1()
    {
        ObjectInitializers.InitializeMethod3();
    }

    [Benchmark]
    public void TestMethod3_1K()
    {
        for (var i = 0; i < 1000; i++)
        {
            ObjectInitializers.InitializeMethod3();
        }
    }

    [Benchmark]
    public void TestMethod3_10K()
    {
        for (var i = 0; i < 10000; i++)
        {
            ObjectInitializers.InitializeMethod3();
        }
    }

    [Benchmark]
    public void TestMethod4_1()
    {
        ObjectInitializers.InitializeMethod4();
    }

    [Benchmark]
    public void TestMethod4_1K()
    {
        for (var i = 0; i < 1000; i++)
        {
            ObjectInitializers.InitializeMethod4();
        }
    }

    [Benchmark]
    public void TestMethod4_10K()
    {
        for (var i = 0; i < 10000; i++)
        {
            ObjectInitializers.InitializeMethod4();
        }
    }
}