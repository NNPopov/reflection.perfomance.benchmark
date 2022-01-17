namespace PerformanceTest.FillingObjectProperties;

public class TestData
{
    public Dictionary<string, object> InputModels = new()
    {
        { "Id", 10 },
        { "UniqId", 10 },
        { "name", "Same Name" },
        { "Description", "Description" },
        { "Description2", "Description" },
        { "description_3", "Description" },
        { "TestSubModels", "Data" },
        { "SubModels1", "Data" },
        { "SubModels2", "Data" },
        { "Address", "Address" },
        { "Phone", "(050) 199 40 43" },
        { "IsDeleted", true },
        { "IsBonus", true },
        { "IsActive", true }
    };
}