using PerformanceTest.FillingObjectProperties.Attributes;
using System.Reflection;

namespace PerformanceTest.FillingObjectProperties.Extencions;

public static class ReflectionTypeExtencions
{
    private static readonly Type PropertyAliasAttributeType = typeof(AliasAttribute);
    private static readonly Type IgnoreAttributeType = typeof(IgnoreAttribute);

    public static bool IsCollection(this Type propertyType)
    {
        return propertyType.GetInterfaces().Any(t => t.Namespace == "System.Collections.Generic")
               && (propertyType.Name.Contains("ICollection")
                   || propertyType.Name.Contains("HashSet")
                   || propertyType.Name.Contains("IEnumerable")
                   || propertyType.Name.Contains("List"));
    }

    public static bool IsSystemType(this Type type)
    {
        return type.FullName.StartsWith("System");
    }

    public static string GetPropertyAlias(this PropertyInfo member)
    {
        Attribute propertyAliasAttribute = member.GetCustomAttribute(PropertyAliasAttributeType, true);

        return propertyAliasAttribute
            != null ? ((AliasAttribute)propertyAliasAttribute).Alias : member.Name;
    }

    public static bool IsIgnoreAttribute(this PropertyInfo member) => 
        member.GetCustomAttributes(IgnoreAttributeType, true).Any();

    public static void SetValue<TModel>(this Type publicModelType,
        PropertyInfo publicModelPropertyInfo,
        TModel? data,
        object dataValue)
    {
        switch (publicModelType.Name)
        {
            case nameof(String):
                publicModelPropertyInfo.SetValue(data, (string)dataValue);
                break;
            case nameof(Int32):
                publicModelPropertyInfo.SetValue(data, (int)dataValue);
                break;
            case nameof(Boolean):
                publicModelPropertyInfo.SetValue(data, (bool)dataValue);
                break;
        }
    }
}