using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Rules
{
    public class AccessValueRule : IRule
    {
        private readonly IRule _func;
        private readonly IRule _indexRule;

        public AccessValueRule(IRule func, IRule indexRule)
        {
            _func = func;
            _indexRule = indexRule;
        }

        public async ValueTask<ValueContainer> Evaluate()
        {
            var currentValue = await _func.Evaluate();
            var indexRuleVc = await _indexRule.Evaluate();
            var index = indexRuleVc["index"];
            var nullConditional = indexRuleVc["nullConditional"].GetValue<bool>();

            if (currentValue.Type() == ValueContainer.ValueType.Array)
            {
                if (index.Type() != ValueContainer.ValueType.Integer)
                {
                    throw InvalidTemplateException.BuildInvalidTemplateExceptionArray(
                        (_func as ExpressionRule)?.FunctionName,
                        _func.PrettyPrint() + _indexRule.PrettyPrint(),
                        index.GetValue<string>());
                }

                try
                {
                    return currentValue[index.GetValue<int>()];
                }
                catch (IndexOutOfRangeException)
                {
                    if (nullConditional)
                    {
                        return new ValueContainer();
                    }

                    throw;
                }
            }

            if (currentValue.Type() == ValueContainer.ValueType.Object)
            {
                try
                {
                    return currentValue[index.GetValue<string>()];
                }
                catch (KeyNotFoundException)
                {
                    if (nullConditional)
                    {
                        return new ValueContainer();
                    }

                    throw InvalidTemplateException.BuildInvalidTemplateExceptionObject(
                        (_func as ExpressionRule)?.FunctionName,
                        _func.PrettyPrint() + _indexRule.PrettyPrint(),
                        index.GetValue<string>(),
                        currentValue.GetValue<Dictionary<string, ValueContainer>>().Keys.ToArray());
                }
            }

            return currentValue;
        }

        public string PrettyPrint()
        {
            return $"{_func.PrettyPrint()}{_indexRule.PrettyPrint()}";
        }
    }
}