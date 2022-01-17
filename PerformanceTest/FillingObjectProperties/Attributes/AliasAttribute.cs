namespace PerformanceTest.FillingObjectProperties.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class AliasAttribute : Attribute
{
    public AliasAttribute(string alias)
    {
        Alias = alias;
    }

    public string Alias { get; }
}