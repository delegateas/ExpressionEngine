using System;
using System.Linq;
using System.Reflection;
using ExpressionEngine.Functions.Base;
using Microsoft.Extensions.DependencyInjection;

namespace ExpressionEngine
{
    /// <summary>
    /// Extension methods for service collection
    /// </summary>
    public static class ExpressionEngineDiExtensions
    {
        /// <summary>
        /// Find function implementations registered with <see cref="FunctionRegistrationAttribute"/>.
        /// 
        /// The function only scans the given type's assembly
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <typeparam name="T">Assembly to scan</typeparam>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns></returns>
        public static IServiceCollection WithFunctionDiscovery<T>(this IServiceCollection serviceCollection)
        {
            var addFunctionMethodInfo = typeof(ExpressionEngineDiExtensions).GetMethod(nameof(AddFunction));

            if (addFunctionMethodInfo == null)
                throw new Exception();

            var functions =
                typeof(T)
                    .Assembly
                    .GetTypes()
                    .Where(t => t.GetCustomAttributes<FunctionRegistrationAttribute>().Any());

            foreach (var function in functions)
            {
                var generic = addFunctionMethodInfo.MakeGenericMethod(function);
                generic.Invoke(null, new object[] {serviceCollection});
            }

            return serviceCollection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <typeparam name="T"></typeparam>
        private static void AddFunction<T>(IServiceCollection serviceCollection) where T : class, IFunction
        {
            serviceCollection.AddFunction<T>();
        }
    }
}