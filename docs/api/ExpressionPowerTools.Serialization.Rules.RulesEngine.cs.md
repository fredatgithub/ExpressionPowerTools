﻿# RulesEngine Class

[Index](../index.md) > [ExpressionPowerTools.Serialization](ExpressionPowerTools.Serialization.a.md) > [ExpressionPowerTools.Serialization.Rules](ExpressionPowerTools.Serialization.Rules.n.md) > **RulesEngine**

Implementation of [IRulesConfiguration](ExpressionPowerTools.Serialization.Signatures.IRulesConfiguration.i.md) .

```csharp
public class RulesEngine : IRulesConfiguration, IRulesEngine
```

Inheritance [Object](https://docs.microsoft.com/dotnet/api/system.object) → **RulesEngine**

Implements  [IRulesConfiguration](ExpressionPowerTools.Serialization.Signatures.IRulesConfiguration.i.md) ,  [IRulesEngine](ExpressionPowerTools.Serialization.Signatures.IRulesEngine.i.md) 

## Remarks

The rules engine is an opt-in engine for execution. By default, properties and fields are allowed.
            Methods and constructors are not allowed. The rules engine operates on a few principles.

Allowing or denying a type will allow or deny all children of that type. For example,
            although methods are denied by defeault, allowing a type will also allow access to methods.
            More specific rules override general. You can allow a type and then deny a method, for example.
            Rules defined on generic types are inherited by closed types by default. You can choose to specify
            a rule for the closed type. For example, you might allow [IQueryable&lt;out T>](https://docs.microsoft.com/dotnet/api/system.linq.iqueryable-1) as a generic
            type but deny `IQueryable<int>`.

Use the chainable [IRulesConfiguration](ExpressionPowerTools.Serialization.Signatures.IRulesConfiguration.i.md) interface to build the rules. By default
            they are "allow." You can use the explicitto reinforce this, or turn it into
            a.

## Constructors

| Ctor | Description |
| :-- | :-- |
| [RulesEngine()](ExpressionPowerTools.Serialization.Rules.RulesEngine.ctor.md#rulesengine) | Initializes a new instance of the [RulesEngine](ExpressionPowerTools.Serialization.Rules.RulesEngine.cs.md) class. |
## Properties

| Property | Type | Description |
| :-- | :-- | :-- |
| [`AllowAnonymousTypes`](ExpressionPowerTools.Serialization.Rules.RulesEngine.AllowAnonymousTypes.prop.md) | [Boolean](https://docs.microsoft.com/dotnet/api/system.boolean) | Gets a value indicating whether anonymous types are allowed. |

## Methods

| Method | Description |
| :-- | :-- |
| [Void AddRule(ISerializationRule rule)](ExpressionPowerTools.Serialization.Rules.RulesEngine.AddRule.m.md) | Adds a rule to the engine. |
| [IRulesConfiguration Allow()](ExpressionPowerTools.Serialization.Rules.RulesEngine.Allow.m.md) | Allow the rule in queue. |
| [IRulesConfiguration Deny()](ExpressionPowerTools.Serialization.Rules.RulesEngine.Deny.m.md) | Deny the rule in queue. |
| [IRulesConfiguration DenyAnonymousTypes()](ExpressionPowerTools.Serialization.Rules.RulesEngine.DenyAnonymousTypes.m.md) | Use this rule to disallow anonymous types. |
| [Boolean MemberIsAllowed(MemberInfo member)](ExpressionPowerTools.Serialization.Rules.RulesEngine.MemberIsAllowed.m.md) | Check if a member is allowed. |
| [Void Reset()](ExpressionPowerTools.Serialization.Rules.RulesEngine.Reset.m.md) | Clears the ruleset. |
| [IRulesConfiguration RuleForConstructor(Action&lt;MemberSelector&lt;ConstructorInfo>> selector)](ExpressionPowerTools.Serialization.Rules.RulesEngine.RuleForConstructor.m.md) | Sets up a constructor rule. |
| [IRulesConfiguration RuleForField(Action&lt;MemberSelector&lt;FieldInfo>> selector)](ExpressionPowerTools.Serialization.Rules.RulesEngine.RuleForField.m.md) | Sets up a field rule. |
| [IRulesConfiguration RuleForMethod(Action&lt;MemberSelector&lt;MethodInfo>> selector)](ExpressionPowerTools.Serialization.Rules.RulesEngine.RuleForMethod.m.md) | Sets up a method rule. |
| [IRulesConfiguration RuleForProperty(Action&lt;MemberSelector&lt;PropertyInfo>> selector)](ExpressionPowerTools.Serialization.Rules.RulesEngine.RuleForProperty.m.md) | Sets up a property rule. |
| [IRulesConfiguration RuleForType(Type type)](ExpressionPowerTools.Serialization.Rules.RulesEngine.RuleForType.m.md) | Adds the rules for a type. |

---

| Generated | Copyright | Version |
| :-- | :-: | --: |
| 09/21/2020 19:07:57 | (c) Copyright 2020 Jeremy Likness. | 0.9.2-alpha |
