﻿# RulesEngine.RuleForProperty Method

[Index](../index.md) > [ExpressionPowerTools.Serialization](ExpressionPowerTools.Serialization.a.md) > [ExpressionPowerTools.Serialization.Rules](ExpressionPowerTools.Serialization.Rules.n.md) > [RulesEngine](ExpressionPowerTools.Serialization.Rules.RulesEngine.cs.md) > **RuleForProperty**

Sets up a property rule.

## Overloads

| Overload | Description |
| :-- | :-- |
| [RuleForProperty(Action&lt;MemberSelector&lt;PropertyInfo>> selector)](#ruleforpropertyactionmemberselectorpropertyinfo-selector) | Sets up a property rule. |
## RuleForProperty(Action&lt;MemberSelector&lt;PropertyInfo>> selector)

Sets up a property rule.

```csharp
public virtual IRulesConfiguration RuleForProperty(Action<MemberSelector<PropertyInfo>> selector)
```

### Return Type

 [IRulesConfiguration](ExpressionPowerTools.Serialization.Signatures.IRulesConfiguration.i.md)  - The [IRulesConfiguration](ExpressionPowerTools.Serialization.Signatures.IRulesConfiguration.i.md) .

### Parameters

| Parameter | Type | Description |
| :-- | :-- | :-- |
| `selector` | [Action&lt;MemberSelector&lt;PropertyInfo>>](https://docs.microsoft.com/dotnet/api/system.action-1) | The selector. |



---

| Generated | Copyright | Version |
| :-- | :-: | --: |
| 09/21/2020 19:07:57 | (c) Copyright 2020 Jeremy Likness. | 0.9.2-alpha |
