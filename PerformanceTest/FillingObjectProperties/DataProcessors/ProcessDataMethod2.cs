using PerformanceTest.FillingObjectProperties.Extencions;
using System.Reflection;

namespace PerformanceTest.FillingObjectProperties.DataProcessors;

public class ProcessDataMethod2<TModel> where TModel : class
{
    private static readonly Type ModelType = typeof(TModel);
    private static readonly Type CollectionType = typeof(List<>);
    private static readonly IEnumerable<PropertyTypeInfo> PropertyObjects = PrepareModelProperties();

    public TModel ProcessData(TestData inputData)
    {
        TModel model = (TModel)Activator.CreateInstance(ModelType);

        foreach (var publicModelProperty in PropertyObjects)
        {
            if (!inputData.InputModels.TryGetValue(publicModelProperty.Alias, out var dataValue))
                continue;

            if (publicModelProperty.IsCollections)
            {
                publicModelProperty.PublicPropertyInfo.SetValue(model,
                    Activator.CreateInstance(publicModelProperty.PropertyType));

                continue;
            }

            if (publicModelProperty.IsSystemType)
            {
                publicModelProperty.PropertyType.SetValue(publicModelProperty.PublicPropertyInfo, model, dataValue);
            }
        }

        return model;
    }
    private static IEnumerable<PropertyTypeInfo> PrepareModelProperties()
    {
        var publicModelProperties = ModelType
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(x => x.CanRead && x.CanWrite)
            .ToArray();

        var propertyObjectsList = new List<PropertyTypeInfo>();

        foreach (var publicModelPropertyInfo in publicModelProperties)
        {
            if (publicModelPropertyInfo.IsIgnoreAttribute())
                continue;

            Type publicModelType = publicModelPropertyInfo.PropertyType;

            string alias = publicModelPropertyInfo.GetPropertyAlias();

            var propertyObject = new PropertyTypeInfo
            {
                PublicPropertyInfo = publicModelPropertyInfo,
                Alias = alias
            };

            if (publicModelType.IsCollection())
            {
                propertyObject.IsCollections = true;

                Type collectionType = publicModelType.GetGenericArguments()[0];

                Type genericCollectionType = CollectionType.MakeGenericType(collectionType);
                propertyObject.PropertyType = genericCollectionType;
            }

            if (publicModelPropertyInfo.PropertyType.IsSystemType() && !publicModelType.IsCollection())
            {
                propertyObject.IsSystemType = true;
                propertyObject.PropertyType = publicModelType;
            }

            propertyObjectsList.Add(propertyObject);
        }

        return propertyObjectsList;
    }

    private class PropertyTypeInfo
    {
        public Type PropertyType { get; set; }

        public bool IsSystemType { get; set; }

        public PropertyInfo? PublicPropertyInfo { get; set; }

        public string Alias { get; set; }

        public bool IsCollections { get; set; }
    }
}