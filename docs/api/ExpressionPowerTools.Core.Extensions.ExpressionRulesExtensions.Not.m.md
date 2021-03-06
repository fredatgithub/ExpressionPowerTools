﻿# ExpressionRulesExtensions.Not Method

[Index](../index.md) > [ExpressionPowerTools.Core](ExpressionPowerTools.Core.a.md) > [ExpressionPowerTools.Core.Extensions](ExpressionPowerTools.Core.Extensions.n.md) > [ExpressionRulesExtensions](ExpressionPowerTools.Core.Extensions.ExpressionRulesExtensions.cs.md) > **Not**

Logical NOT of result of rule.

## Overloads

| Overload | Description |
| :-- | :-- |
| [Not&lt;T>(Expression&lt;Func&lt;T, T, Boolean>> rule)](#nottexpressionfunct-t-boolean-rule) | Logical NOT of result of rule. |
## Not&lt;T>(Expression&lt;Func&lt;T, T, Boolean>> rule)

Logical NOT of result of rule.

```csharp
public static Expression<Func<T, T, Boolean>> Not<T>(Expression<Func<T, T, Boolean>> rule)
```

### Return Type

 [Expression&lt;Func&lt;T, T, Boolean>>](https://docs.microsoft.com/dotnet/api/system.linq.expressions.expression-1)  - The opposite of the rule evaluation.

### Parameters

| Parameter | Type | Description |
| :-- | :-- | :-- |
| `rule` | [Expression&lt;Func&lt;T, T, Boolean>>](https://docs.microsoft.com/dotnet/api/system.linq.expressions.expression-1) | The rule to evaluate. |


## Examples

For example:

```csharp
var source = Expression.Constant(true);
             var target = Expression.Constant(true);
             var rule = rules.Not<ConstantExpression>(
                (s, t) => (bool)s.Value);
             var result = rule.Compile())source, target);
             
```

Because of the call to `Not` , the result is `false` .


---

| Generated | Copyright | Version |
| :-- | :-: | --: |
| 09/21/2020 19:07:57 | (c) Copyright 2020 Jeremy Likness. | 0.9.2-alpha |
