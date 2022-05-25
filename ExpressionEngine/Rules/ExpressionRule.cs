using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Rules
{
    public class ExpressionRule : IRule
    {
        private readonly IFunction _function;
        private readonly IEnumerable<IRule> _args;
        public string FunctionName { get; }


        public ExpressionRule(List<FunctionMetadata> functions, IServiceProvider serviceProvider, string functionName,
            IEnumerable<IRule> args)
        {
            var functionMetadata = functions.Find(x => x.Alias == functionName);
            if (functionMetadata == null)
            {
                throw new FunctionNotKnown(
                    $"Function metadata for function with name: {functionName}, does not exist.\nAdd the function metadata to the expression engine to test this expression.");
            }
            
            _function = serviceProvider.GetService(functionMetadata.Function) as IFunction;
            if (_function == null)
            {
                throw new FunctionNotKnown(
                    $"Function with name: {functionName}, does not exist.\nAdd the function to the expression engine to test this expression.");
            }

            _args = args;
            FunctionName = functionName;
        }

        public async ValueTask<ValueContainer> Evaluate()
        {
            // var strings = _args?.Select(async rule => await rule.Evaluate()).Select(x => x.Result).ToArray();
            var strings = await Task.WhenAll(_args?.Select(async rule => await rule.Evaluate()) ??
                                             Array.Empty<Task<ValueContainer>>());


            return await _function.ExecuteFunction(strings);
        }

        public string PrettyPrint()
        {
            var seed = _args?.First().PrettyPrint();
            var argumentList = _args?.Skip(1).Aggregate(seed, (s, rule) => s + ", " + rule.PrettyPrint());
            return $"{FunctionName}({argumentList})";
        }
    }
}