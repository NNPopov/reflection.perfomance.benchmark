using PerformanceTest.FillingObjectProperties.Extencions;
using System.Reflection;

namespace PerformanceTest.FillingObjectProperties.DataProcessors;

public class ProcessDataMethod1<TModel> where TModel : class
{
    private static readonly Type ModelType = typeof(TModel);
    private static readonly Type CollectionType = typeof(List<>);

    private static readonly IEnumerable<PropertyInfo> PublicModelProperties =
        typeof(TModel).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(x => x.CanRead && x.CanWrite && !x.PropertyType.IsEnum && !x.IsIgnoreAttribute());

    public TModel ProcessData(TestData inputData)
    {
        TModel data = (TModel)Activator.CreateInstance(ModelType);
        foreach (var publicModelPropertyInfo in PublicModelProperties)
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