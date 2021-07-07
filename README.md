# ExpressionEngine

ExpressionEngine is originally created in [PowerAutomateMockUp]() to parse PowerAutomate Expressions. Parsing of expression can be used outside of Power Automate, why the ExpressionEngine is now a standalone project.

## Usage
EE uses Dependency Injection, which makes it easy to use and extend. Out-of-the-box functionality comes from the `AddExpressionEngine()` extension method:

```c#
using Microsoft.Extensions.DependencyInjection;
using Parser;
...
var services = new ServiceCollection();
services.AddExpressionEngine();
var sp = services.BuildServiceProvider();

ee = sp.GetRequiredService<IExpressionEngine>();
```
EE can evaluate expression to a string or a ValueContainer. A ValueContainer can contain boolean, integer, float, string, object, array or null values.

```c#
var str = ee.Parse("@concat('Hello', ' ', 'World', '!'"); // str = "Hello World!"
var strVc = ee.ParseToValueContainer("@concat('Hello', ' ', 'World', '!'").GetValue<string>(); // str = "Hello World!"
```
> :warning: Expressions can also be written inside strings, such as `someone@@@{toLower('DELEGATE')}.dk` will evaluate to `someone@delegate.dk`, the result using `@{<expression>}` will always evaluate to a [`ValueType.String`](https://github.com/delegateas/ExpressionEngine/blob/18e1717658b82a17a9c50fe7a2c66f988605c80e/ExpressionEngine/ValueContainer.cs#L134), whereas `@<expression>` will evaluate to the actual type.

### Extending with custom functions
Write your function by extending the `Function` class and providing a function name:
````c#
public class FooBar : Function
{
    public FooBar() : base("fooBar") { }
    
    public override ValueContainer ExecuteFunction(params ValueContainer[] parameters)
    {
        if (parameters.Length != 1)
            throw new Exception("Expected 1 argument");
        if (parameters[0].Type() != ValueContainer.ValueType.String)
            throw new Exception("Expected string");
        return new ValueContainer($"Foo - {parameters[0].GetValue<string>()} - Bar");
    }
}
````
Then add the function to the service collection
```c#
services.AddTransient<IFunction, FooBar>();
```
`IFunction` is used to apply the function when the `fooBar` function name is evaluated.

## Contributing
CI/CD is configured using Github Actions and it works by analyzing commit messages to determine the changes and the relevant version number. This is done using [semantic-release](https://github.com/semantic-release/semantic-release) and the commit message style is [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/).
**Please use the correct format in every commit message to ensure the version is determined correctly**

The `main` branch is the release branch which will release a production NuGet package with the determined version number.
The `dev` branch is the development branch and will release a development NuGet package with the determined version number.

Therefore, start by making a pull-request against `dev` which will trigger a pre-release and later on we can make a production release.

