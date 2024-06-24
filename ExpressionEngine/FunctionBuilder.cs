using System;
using System.Reflection;
using ExpressionEngine.Functions.Base;
using Microsoft.Extensions.DependencyInjection;

namespace ExpressionEngine
{
    /// <summary>
    /// 
    /// </summary>
    public static class FunctionBuilderExt
    {
        /// <summary>
        /// Initiate Function builder for registering a Function, preloaded with <see cref="FunctionRegistrationAttribute"/>
        /// </summary>
        /// <param name="serviceCollection">ServiceCollection to which the function is registered</param>
        /// <typeparam name="TFunction">Type of the Function</typeparam>
        /// <returns></returns>
        public static FunctionBuilder<TFunction> BuildFunction<TFunction>(this IServiceCollection serviceCollection)
            where TFunction : class, IFunction
        {
            var t = new FunctionBuilder<TFunction>(serviceCollection);

            var functionRegistrationAttributes = typeof(TFunction).GetCustomAttributes<FunctionRegistrationAttribute>();

            foreach (var functionRegistrationAttribute in functionRegistrationAttributes)
            {
                t.SetScope(functionRegistrationAttribute.Scope);
                t.WithAlias(functionRegistrationAttribute.FunctionName);
            }

            return t;
        }

        /// <summary>
        /// Add function using <see cref="FunctionRegistrationAttribute"/>
        /// </summary>
        /// <param name="serviceCollection">ServiceCollection to which the function is registered</param>
        /// <typeparam name="TFunction">Type of the Function</typeparam>
        /// <returns></returns>
        public static IServiceCollection AddFunction<TFunction>(this IServiceCollection serviceCollection)
            where TFunction : class, IFunction
        {
            return serviceCollection.BuildFunction<TFunction>().Add();
        }
    }

    /// <summary>
    /// FunctionBuilder is responsible for adding a Function properly
    /// </summary>
    /// <typeparam name="T">Type of the Function</typeparam>
    public class FunctionBuilder<T> where T : class, IFunction
    {
        private readonly IServiceCollection _serviceCollection;
        private Scope _scope = Scope.Transient;

        /// <summary>
        /// Set scope without using As{Scope}
        /// </summary>
        /// <param name="scope"></param>
        internal void SetScope(Scope scope)
        {
            _scope = scope;
        }

        /// <summary>
        /// Create function builder 
        /// </summary>
        /// <param name="serviceCollection">ServiceCollection to which the function is registered</param>
        public FunctionBuilder(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        /// <summary>
        /// Add Function as transient
        /// </summary>
        /// <returns>builder</returns>
        public FunctionBuilder<T> AsTransient()
        {
            _scope = Scope.Transient;
            return this;
        }

        /// <summary>
        /// Add Function as scoped
        /// </summary>
        /// <returns>builder</returns>
        public FunctionBuilder<T> AsScoped()
        {
            _scope = Scope.Scoped;
            return this;
        }

        /// <summary>
        /// Add Function as singleton
        /// </summary>
        /// <returns>builder</returns>
        public FunctionBuilder<T> AsSingleton()
        {
            _scope = Scope.Singleton;
            return this;
        }

        /// <summary>
        /// Associate alias with Function, an alias can be used to invoke a function.
        /// </summary>
        /// <param name="alias">function alias</param>
        /// <returns>builder</returns>
        public FunctionBuilder<T> WithAlias(string alias)
        {
            _serviceCollection.AddSingleton(new FunctionMetadata(typeof(T), alias));
            return this;
        }

        /// <summary>
        /// Add build Function to ServiceCollection
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public IServiceCollection Add()
        {
            switch (_scope)
            {
                case Scope.Scoped:
                    _serviceCollection.AddScoped<T>();
                    break;
                case Scope.Transient:
                    _serviceCollection.AddTransient<T>();
                    break;
                case Scope.Singleton:
                    _serviceCollection.AddSingleton<T>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return _serviceCollection;
        }
    }
}