namespace PerformanceTest.ObjectInitializer.Entities;

public class TestClass
{
    private readonly int id;

    private readonly string name;

    public TestClass(string name, int id)
    {
        this.name = name;
        this.id = id;
    }
}
