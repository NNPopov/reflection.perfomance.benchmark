using PerformanceTest.FillingObjectProperties.Extencions;
using System.Reflection;

namespace PerformanceTest.FillingObjectProperties.DataProcessors;

public class ProcessDataMethod0<TModel> where TModel : class
{
    public TModel ProcessData(TestData inputData)
    {
        Type modelType = typeof(TModel);
        Type CollectionType = typeof(List<>);

        IEnumerable<PropertyInfo> publicModelProperties =
            typeof(TModel).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.CanRead && x.CanWrite && !x.PropertyType.IsEnum && !x.IsIgnoreAttribute());

        TModel data = (TModel)Activator.CreateInstance(modelType);

        foreach (var publicModelPropertyInfo in publicModelProperties)
        {
            Type publicModelType = publicModelPropertyInfo.PropertyType;

            string alias = publicModelPropertyInfo.GetPropertyAlias();

            if (!inputData.InputModels.TryGetValue(alias, out var dataValue))
                continue;

            if (publicModelType.IsCollection())
            {
                Type collectionType = publicModelType.GetGenericArguments()[0];

                Type genericCollectionType = CollectionType.MakeGenericType(collectionType);

                publicModelPropertyInfo.SetValue(data, Activator.CreateInstance(genericCollectionType));

                continue;
            }

            if (publicModelPropertyInfo.PropertyType.IsSystemType())
                publicModelType.SetValue(publicModelPropertyInfo, data, dataValue);
        }

        return data;
    }
}