# ExpressionEngine

ExpressionEngine is originally created in [PowerAutomateMockUp]() to parse PowerAutmomate Expressions. Parsing of expression can be used outside of Power Automate, why the ExpressionEngine is now a standalone project.

## BNF

This is the BNF, [Backus-Nuar form](https://da.wikipedia.org/wiki/Backus-Naur_form), of the language used in Power Automate / Logic Apps expressions. 
Their JSON definition can be found [here](https://docs.microsoft.com/en-us/azure/logic-apps/workflow-definition-language-functions-reference) and [here](https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-workflow-definition-language#functions) and the [json](https://schema.management.azure.com/providers/Microsoft.Logic/schemas/2016-06-01/workflowdefinition.json).

### Examples
`@triggerOutputs()`

`@toLower('TEXT')`

`@toLower(toUpper('text'))`

`@variables('var')`

`@variables('array')[0]`

`@variables('object')['user']`

`@variables('object')[toLower('USER')]`

`@{toLower('USER')}@{toUpper('user')}`

`Name: @{variables('object')[toLower('USER')]}`


```xml
<input>                 ::= <expression> | <joinedString> | <atPrefixedString>
<atPrefixedString>      ::= <string> <!-- NO ( ) -->

<joinedString>          ::= *(*<allowedString> *<enclosedExpression>)
<allowedString>         ::= *([A-Za-z] | [0-9] | [\_]) <!-- Add the correct characters -->

<expression>	        ::= "@" <method>
<enclosedExpression>	::= "@{" <method> "}"

<method>                ::= <function> *<operators>

<function>      	    ::= <string> "(" [ <arguments> ] ")"
<arguments>             ::= <argument> *("," <argument>)
<argument>	            ::= <method> | <string>

<operators>	            ::= *( [ <nullconditional> ] "[" <index> "]") | <dot>
<nullconditional>       ::= "?"
<index>                 ::= <int> | <string> | <method>
<dot>                   ::= "." <string>
```

`string` contains a valid string including characters according to Azure Workflow definition language.


## Special expressions - No part of the project anymore
**Section about how to handle specific storage realted functions will come**

Some expressions the user can define itself, since it is idempotent and does not use the engine state.

Some expression should not be user dependent, such as

 * compose
 * outputs
 * triggerOutputs
 * variables

These are all expression which interact with data stored in the state.



Eventuelt skriv om hvordan det var sværet bare at gå igang, men efter du læste en artikel og lavede en BNF var det nemt. Dette var måske også fordi du vidste hvordan Sprache virkede og hvad der skete under moteren og aritklen gav dig en dybere forståelse af hvad man kan! :D

