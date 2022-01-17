using PerformanceTest.FillingObjectProperties.Attributes;

namespace PerformanceTest.FillingObjectProperties.Entities;

public class TestEntity
{
    public int Id { get; set; }

    public int UniqId { get; set; }

    [Alias("name")]
    public string Name { get; set; }

    public string Phone { get; set; }

    [Alias("IsActive")]
    public bool Active { get; set; }

    [Ignore]
    public string Description { get; set; }

    public string Description2 { get; set; }


    [Alias("description_3")]
    public string Description3 { get; set; }

    public ICollection<SubEntity> TestSubModels { get; set; }

    [Alias("SubModels1")]
    public ICollection<SubEntity> TestSubModels1 { get; set; }

    public void EnsureDataFilled()
    {
        if (Id == 0 ||
            UniqId == 0 ||
            string.IsNullOrEmpty(Name) ||
            string.IsNullOrEmpty(Phone) ||
            !string.IsNullOrEmpty(Description) ||
            string.IsNullOrEmpty(Description2) ||
            string.IsNullOrEmpty(Description3) ||
            TestSubModels == null ||
            TestSubModels1 == null)
            throw new Exception();
    }
}