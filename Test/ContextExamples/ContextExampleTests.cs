using ExpressionEngine;
using ExpressionEngine.Functions.Base;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ContextExamples
{
    public class FormNameFunction : IFunction
    {
        

        public ValueTask<ValueContainer> ExecuteFunction(ValueContainer[] strings)
        {
            return new ValueTask<ValueContainer>(new ValueContainer("Test"));
        }
    }
    public class AccountNameFunction : IFunction
    {


        public ValueTask<ValueContainer> ExecuteFunction(ValueContainer[] strings)
        {
            return new ValueTask<ValueContainer>(new ValueContainer("Test Account"));
        }
    }

    public class YearFunction : IFunction
    { 
        public ValueTask<ValueContainer> ExecuteFunction(ValueContainer[] strings)
        {
            return new ValueTask<ValueContainer>(new ValueContainer("2022"));
        }
    }



    public class AccountFunction : IFunction
    {


        public ValueTask<ValueContainer> ExecuteFunction(ValueContainer[] strings)
        {
            return new ValueTask<ValueContainer>(new ValueContainer(new Dictionary<string, ValueContainer>
            {
                ["internalAccountCode3"] = new ValueContainer(null as string )
            })); ;
        }
    }




    [TestFixture]
    public class ContextExampleTests
    {
        private IExpressionEngine _expressionGrammar;
       

        [SetUp]
        public void SetUp()
        {
            var t = new ServiceCollection();
            t.RegisterScopedFunctionAlias<FormNameFunction>("formName");
            t.RegisterScopedFunctionAlias<AccountNameFunction>("accountName");
            t.RegisterScopedFunctionAlias<AccountFunction>("account");
            t.RegisterScopedFunctionAlias<YearFunction>("year");

            t.AddExpressionEngine();

            _expressionGrammar = t.BuildServiceProvider().GetService<IExpressionEngine>();
        }

        [Test]
        public async Task TestExpression()
        {
             
            var result = await _expressionGrammar.Parse("@{formName()} - @{year()} - @{accountName()} - @{account()?.internalAccountCode3}");

            Assert.AreEqual("Test - 2022 - Test Account - ", result);
        }



    }
        
}
