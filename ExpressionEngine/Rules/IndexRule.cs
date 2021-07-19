﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpressionEngine.Rules
{
    public class IndexRule : IRule
    {
        private readonly IRule _index;
        private readonly bool _nullConditional;

        public IndexRule(IRule index, bool nullConditional)
        {
            _index = index;
            _nullConditional = nullConditional;
        }

        public async ValueTask<ValueContainer> Evaluate()
        {
            var indexContent = new Dictionary<string, ValueContainer>
            {
                {"nullConditional", new ValueContainer(_nullConditional)},
                {"index", await _index.Evaluate()}
            };

            return new ValueContainer(indexContent);
        }

        public string PrettyPrint()
        {
            return $"[{_index.PrettyPrint()}]";
        }
    }
}