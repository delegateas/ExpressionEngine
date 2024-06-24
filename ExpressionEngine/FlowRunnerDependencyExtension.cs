using System;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;
using Microsoft.Extensions.DependencyInjection;

namespace ExpressionEngine
{
    /// <summary>
    /// Selection of extension methods
    /// </summary>
    public static class FlowRunnerDependencyExtension
    {

        /// <summary>
        /// Add necessary dependencies inorder to use expression engine. 
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> to add dependencies</param>
        public static void AddExpressionEngine(this IServiceCollection services)
        {
            services.AddScoped<IExpressionEngine, ExpressionEngine>();
            services.AddScoped<ExpressionGrammar>();

            services.WithFunctionDiscovery<Functions.Functions>();
        }

        /// <summary>
        /// Register function to be used in expression, function implementation must implement <see cref="IFunction"/>.
        /// </summary>
        /// <param name="services">Services which to add function metadata</param>
        /// <param name="functionName">name of function used to invoke it</param>
        /// <typeparam name="T">Function implementation</typeparam>
        [Obsolete("Use '.AddFunction().AsTransient().WithAlias(<string>)' instead")]
        public static void RegisterTransientFunctionAlias<T>(this IServiceCollection services, string functionName)
            where T : class, IFunction
        {
            services.AddTransient<T>();
            services.AddSingleton(new FunctionMetadata(typeof(T), functionName));
        }

        /// <summary>
        /// Register function to be used in expression, function implementation must implement <see cref="IFunction"/>.
        /// </summary>
        /// <param name="services">Services which to add function metadata</param>
        /// <param name="functionName">name of function used to invoke it</param>
        /// <typeparam name="T">Function implementation</typeparam>
        [Obsolete("Use '.AddFunction().AsScoped().WithAlias(<string>)' instead")]
        public static void RegisterScopedFunctionAlias<T>(this IServiceCollection services, string functionName)
            where T : class, IFunction
        {
            services.AddScoped<T>();
            services.AddSingleton(new FunctionMetadata(typeof(T), functionName));
        }

        /// <summary>
        /// Register function to be used in expression, function implementation must implement <see cref="IFunction"/>.
        /// </summary>
        /// <param name="services">Services which to add function metadata</param>
        /// <param name="functionName">name of function used to invoke it</param>
        /// <param name="implementationFactory"></param>
        /// <typeparam name="T">Function implementation</typeparam>
        [Obsolete("Use '.AddFunction<T>(<factory>).AsScoped().WithAlias(<string>)' instead")]
        public static void RegisterScopedFunctionAlias<T>(this IServiceCollection services, string functionName,
            Func<IServiceProvider, T> implementationFactory)
            where T : class, IFunction
        {
            services.AddScoped(implementationFactory);
            services.AddSingleton(new FunctionMetadata(typeof(T), functionName));
        }

        /// <summary>
        /// Added FunctionDefinition to service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="fromFunctionName">The name of the function, without function parenthesis</param>
        /// <param name="toExpression">The full expression which is inserted</param>
        public static void AddFunctionDefinition(this IServiceCollection services, string fromFunctionName,
            string toExpression)
        {
            if (fromFunctionName.EndsWith("()"))
            {
                throw new ArgumentError($"{nameof(fromFunctionName)} cannot end in ()");
            }

            services.AddSingleton<IFunctionDefinition>(new FunctionDefinition
                {From = fromFunctionName + "()", To = toExpression});
        }
    }
}