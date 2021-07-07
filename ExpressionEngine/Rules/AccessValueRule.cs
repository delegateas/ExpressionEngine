using System;
using System.Collections.Generic;
using System.Linq;
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

        public ValueContainer Evaluate()
        {
            var currentValue = _func.Evaluate();
            var indexRuleVc = _indexRule.Evaluate();
            var index = indexRuleVc["index"];
            var nullConditional = indexRuleVc["nullConditional"].GetValue<bool>();

            if (currentValue.Type() == ValueContainer.ValueType.Array)
            {
                var asArray = currentValue.GetValue<ValueContainer[]>();
                if (index.Type() != ValueContainer.ValueType.Integer)
                {
                    throw InvalidTemplateException.BuildInvalidTemplateExceptionArray(
                        (_func as ExpressionRule)?.FunctionName,
                        _func.PrettyPrint() + _indexRule.PrettyPrint(),
                        index.GetValue<string>());
                }

                var i = index.GetValue<int>();
                if (i > asArray.Length)
                {
                    if (nullConditional)
                    {
                        return new ValueContainer();
                    }
                    throw new IndexOutOfRangeException();
                }

                currentValue = asArray[i];
            }

            else if (currentValue.Type() == ValueContainer.ValueType.Object)
            {
                var asObject = currentValue.GetValue<Dictionary<string, ValueContainer>>();
                var key = index.GetValue<string>();

                if (asObject.ContainsKey(key))
                {
                    return asObject[key];
                }

                    if (nullConditional)
                    {
                        return new ValueContainer();
                    }

                throw InvalidTemplateException.BuildInvalidTemplateExceptionObject(
                    (_func as ExpressionRule)?.FunctionName,
                    _func.PrettyPrint() + _indexRule.PrettyPrint(),
                    index.GetValue<string>(),
                    asObject.Keys.ToArray());
            }

            return currentValue;
        }

        public string PrettyPrint()
        {
            throw new NotImplementedException();
        }
    }
}