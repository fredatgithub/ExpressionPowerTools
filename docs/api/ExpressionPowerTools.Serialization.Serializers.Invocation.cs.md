﻿# Invocation Class

[Index](../index.md) > [ExpressionPowerTools.Serialization](ExpressionPowerTools.Serialization.a.md) > [ExpressionPowerTools.Serialization.Serializers](ExpressionPowerTools.Serialization.Serializers.n.md) > **Invocation**

A serializable version of [InvocationExpression](https://docs.microsoft.com/dotnet/api/system.linq.expressions.invocationexpression) .

```csharp
public class Invocation : SerializableExpression
```

Inheritance [Object](https://docs.microsoft.com/dotnet/api/system.object) → [SerializableExpression](ExpressionPowerTools.Serialization.Serializers.SerializableExpression.cs.md) → **Invocation**

## Constructors

| Ctor | Description |
| :-- | :-- |
| [Invocation()](ExpressionPowerTools.Serialization.Serializers.Invocation.ctor.md#invocation) | Initializes a new instance of the [Invocation](ExpressionPowerTools.Serialization.Serializers.Invocation.cs.md) class. |
## Properties

| Property | Type | Description |
| :-- | :-- | :-- |
| [`Arguments`](ExpressionPowerTools.Serialization.Serializers.Invocation.Arguments.prop.md) | [List&lt;Object>](https://docs.microsoft.com/dotnet/api/system.collections.generic.list-1) | Gets or sets the arguments list. |
| [`Expression`](ExpressionPowerTools.Serialization.Serializers.Invocation.Expression.prop.md) | [Object](https://docs.microsoft.com/dotnet/api/system.object) | Gets or sets the target [Expression](ExpressionPowerTools.Serialization.Serializers.Invocation.Expression.prop.md) . |


---

| Generated | Copyright | Version |
| :-- | :-: | --: |
| 09/21/2020 19:07:57 | (c) Copyright 2020 Jeremy Likness. | 0.9.2-alpha |
