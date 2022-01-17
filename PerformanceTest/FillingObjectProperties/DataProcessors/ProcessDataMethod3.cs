using PerformanceTest.FillingObjectProperties.Extencions;
using System.Linq.Expressions;
using System.Reflection;

namespace PerformanceTest.FillingObjectProperties.DataProcessors;

public class ProcessDataMethod3<TModel> where TModel : class
{
    private static readonly Type ModelType = typeof(TModel);
    private static readonly Type CollectionType = typeof(List<>);
    private static readonly IEnumerable<PropertyTypeInfo2> PropertyObjects = PrepareModelProperties();
    private static Func<TModel> newTModel;

    public TModel ProcessData(TestData inputData)
    {
        TModel model = newTModel.Invoke();

        foreach (var publicModelProperty in PropertyObjects)
        {
            if (!inputData.InputModels.TryGetValue(publicModelProperty.Alias, out var dataValue))
                continue;

            if (publicModelProperty.IsCollections)
            {
                publicModelProperty.ValueSetter.Invoke(model,
                    Activator.CreateInstance(publicModelProperty.PropertyType));

                continue;
            }

            if (publicModelProperty.IsSystemType)
            {
                publicModelProperty.ValueSetter.Invoke(model, dataValue);
            }
        }

        return model;
    }

    private static Action<TModel, object> CreateSetValueDelagate(PropertyInfo property)
    {
        ParameterExpression parameterInstance = Expression.Parameter(ModelType);
        ParameterExpression parameterValue = Expression.Parameter(typeof(object));

        UnaryExpression bodyInstance = Expression.Convert(parameterInstance, property.DeclaringType);
        UnaryExpression bodyValue = Expression.Convert(parameterValue, property.PropertyType);
        MethodCallExpression bodyCall = Expression.Call(bodyInstance, property.GetSetMethod(), bodyValue);

        return Expression.Lambda<Action<TModel, object>>(bodyCall, parameterInstance, parameterValue).Compile();
    }

    private static IEnumerable<PropertyTypeInfo2> PrepareModelProperties()
    {
        var publicModelProperties = ModelType
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(x => x.CanRead && x.CanWrite);

        var propertyObjectsList = new List<PropertyTypeInfo2>();

        NewExpression newTExpression = Expression.New(ModelType.GetConstructor(Type.EmptyTypes));
        newTModel = Expression.Lambda<Func<TModel>>(newTExpression).Compile();

        foreach (var publicModelPropertyInfo in publicModelProperties)
        {
            if (publicModelPropertyInfo.IsIgnoreAttribute())
                continue;

            Type publicModelType = publicModelPropertyInfo.PropertyType;

            string alias = publicModelPropertyInfo.GetPropertyAlias();

            var propertyObject = new PropertyTypeInfo2
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

            propertyObject.ValueSetter = CreateSetValueDelagate(publicModelPropertyInfo);

            propertyObjectsList.Add(propertyObject);
        }

        return propertyObjectsList;
    }

    private class PropertyTypeInfo2
    {
        public Type PropertyType { get; set; }

        public bool IsSystemType { get; set; }

        public PropertyInfo? PublicPropertyInfo { get; set; }

        public string Alias { get; set; }

        public bool IsCollections { get; set; }

        public Action<TModel, object> ValueSetter { get; set; }
    }
}