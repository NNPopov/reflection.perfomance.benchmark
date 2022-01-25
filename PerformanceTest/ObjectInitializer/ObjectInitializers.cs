using PerformanceTest.ObjectInitializer.Entities;
using System.Linq.Expressions;
using System.Reflection;

namespace PerformanceTest.ObjectInitializer;

public static class ObjectInitializers
{
    private static readonly ConstructorInfo testClassConstructorCache = typeof(TestClass)
        .GetConstructor(new[] { typeof(string), typeof(int) });

    private static readonly Func<string, int, object> createTestClassFunc = СreateTestClassInitializerFunction();

    private static Func<string, int, object> СreateTestClassInitializerFunction()
    {
        ParameterExpression nameParameter = Expression.Parameter(typeof(string), "name");
        ParameterExpression idParameter = Expression.Parameter(typeof(int), "id");

        NewExpression constructorExpression = Expression.New(
            testClassConstructorCache,
            nameParameter,
            idParameter);

        Expression<Func<string, int, object>> lambdaExpression =
            Expression.Lambda<Func<string, int, object>>(
                constructorExpression,
                nameParameter,
                idParameter);

        return lambdaExpression.Compile();
    }

    public static void InitializeMethod0()
    {
        var testСlass = new TestClass("First Name", 35);
    }
    public static void InitializeMethod1()
    {
        var testClass = (TestClass)Activator.CreateInstance(
            typeof(TestClass), new object[] { "First Name", 35 });
    }

    public static void InitializeMethod2()
    {
        ConstructorInfo testClassConstructor = typeof(TestClass).GetConstructor(
            new[] { typeof(string), typeof(int) });
        var testСlass = (TestClass)testClassConstructor.Invoke(new object[] { "First Name", 35 });
    }

    public static void InitializeMethod3()
    {
        var testСlass = (TestClass)testClassConstructorCache.Invoke(new object[] { "First Name", 35 });
    }

    public static void InitializeMethod4()
    {
        var testСlass = (TestClass)createTestClassFunc("First Name", 35);
    }
}
