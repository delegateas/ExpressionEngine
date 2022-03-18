using System;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;
using ExpressionEngine.Functions.Implementations.CollectionFunctions;
using ExpressionEngine.Functions.Implementations.ConversionFunctions;
using ExpressionEngine.Functions.Implementations.LogicalComparisonFunctions;
using ExpressionEngine.Functions.Implementations.StringFunctions;
using ExpressionEngine.Functions.Math;
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

            AddStringFunctions(services);
            AddCollectionFunction(services);
            AddConversionFunction(services);
            AddLogicalComparisonFunctions(services);
            AddMathFunctions(services);

            services.RegisterTransientFunctionAlias<LengthFunction>("length");
            services.RegisterTransientFunctionAlias<GreaterFunction>("greater");
        }
        
        /// <summary>
        /// Register function to be used in expression, function implementation must implement <see cref="IFunction"/>.
        /// </summary>
        /// <param name="services">Services which to add function metadata</param>
        /// <param name="functionName">name of function used to invoke it</param>
        /// <typeparam name="T">Function implementation</typeparam>
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
        public static void AddFunctionDefinition(this IServiceCollection services, string fromFunctionName, string toExpression)
        {
            if (fromFunctionName.EndsWith("()"))
            {
                throw new ArgumentError($"{nameof(fromFunctionName)} cannot end in ()");
            }

            services.AddSingleton<IFunctionDefinition>(new FunctionDefinition{From = fromFunctionName + "()", To = toExpression});
        }

        private static void AddStringFunctions(IServiceCollection services)
        {
            services.RegisterTransientFunctionAlias<ConcatFunction>("concat");
            services.RegisterTransientFunctionAlias<EndsWithFunction>("endsWith");
            services.RegisterTransientFunctionAlias<FormatNumberFunction>("formatNumber");
            services.RegisterTransientFunctionAlias<GuidFunction>("guid");
            services.RegisterTransientFunctionAlias<IndexOfFunction>("indexOf");
            services.RegisterTransientFunctionAlias<LastIndexOfFunction>("lastIndexOf");
            services.RegisterTransientFunctionAlias<LengthFunction>("length");
            services.RegisterTransientFunctionAlias<ReplaceFunction>("replace");
            services.RegisterTransientFunctionAlias<SplitFunction>("split");
            services.RegisterTransientFunctionAlias<StartsWithFunction>("startsWith");
            services.RegisterTransientFunctionAlias<SubstringFunction>("substring");
            services.RegisterTransientFunctionAlias<ToLowerFunction>("toLower");
            services.RegisterTransientFunctionAlias<ToUpperFunction>("toUpper");
            services.RegisterTransientFunctionAlias<TrimFunction>("trim");
        }

        private static void AddCollectionFunction(IServiceCollection services)
        {
            services.RegisterTransientFunctionAlias<ContainsFunction>("contains");
            services.RegisterTransientFunctionAlias<EmptyFunction>("empty");
            services.RegisterTransientFunctionAlias<FirstFunction>("first");
            services.RegisterTransientFunctionAlias<InterSectionFunction>("intersection");
            services.RegisterTransientFunctionAlias<JoinFunction>("join");
            services.RegisterTransientFunctionAlias<LastFunction>("last");
            services.RegisterTransientFunctionAlias<LengthFunction>("length");
            services.RegisterTransientFunctionAlias<SkipFunction>("skip");
            services.RegisterTransientFunctionAlias<TakeFunction>("take");
            services.RegisterTransientFunctionAlias<UnionFunction>("union");
        }

        private static void AddConversionFunction(IServiceCollection services)
        {
            services.RegisterTransientFunctionAlias<ArrayFunction>("array");
            services.RegisterTransientFunctionAlias<Base64Function>("base64");
            services.RegisterTransientFunctionAlias<Base64ToBinaryFunction>("base64ToBinary");
            services.RegisterTransientFunctionAlias<Base64ToStringFunction>("base64ToString");
            services.RegisterTransientFunctionAlias<BinaryFunction>("binary");
            services.RegisterTransientFunctionAlias<BoolFunction>("bool");
            services.RegisterTransientFunctionAlias<CreateArrayFunction>("createArray");
            services.RegisterTransientFunctionAlias<DataUriFunction>("dataUri");
            services.RegisterTransientFunctionAlias<DataUriToBinaryFunction>("dataUriToBinary");
            services.RegisterTransientFunctionAlias<FloatFunction>("float");
            services.RegisterTransientFunctionAlias<IntFunction>("int");
        }

        private static void AddLogicalComparisonFunctions(IServiceCollection services)
        {
            services.RegisterTransientFunctionAlias<AndFunction>("and");
            services.RegisterTransientFunctionAlias<EqualFunction>("equal");
            services.RegisterTransientFunctionAlias<GreaterFunction>("greater");
            services.RegisterTransientFunctionAlias<GreaterOrEqualsFunction>("greaterOrEquals");
            services.RegisterTransientFunctionAlias<IfFunction>("if");
            services.RegisterTransientFunctionAlias<LessFunction>("less");
            services.RegisterTransientFunctionAlias<LessOrEqualsFunction>("lessOrEquals");
            services.RegisterTransientFunctionAlias<NotFunction>("not");
            services.RegisterTransientFunctionAlias<OrFunction>("or");
        }

        private static void AddMathFunctions(IServiceCollection services)
        {
            services.RegisterTransientFunctionAlias<AddFunction>("add");
            services.RegisterTransientFunctionAlias<DivFunction>("div");
            services.RegisterTransientFunctionAlias<MaxFunction>("max");
            services.RegisterTransientFunctionAlias<MinFunction>("min");
            services.RegisterTransientFunctionAlias<ModFunction>("mod");
            services.RegisterTransientFunctionAlias<MulFunction>("mul");
            services.RegisterTransientFunctionAlias<RandFunction>("rand");
            services.RegisterTransientFunctionAlias<RangeFunction>("range");
            services.RegisterTransientFunctionAlias<SubFunction>("sub");
        }
    }
}