﻿# TypeCache Class

[Index](../index.md) > [ExpressionPowerTools.Utilities.DocumentGenerator](ExpressionPowerTools.Utilities.DocumentGenerator.a.md) > [ExpressionPowerTools.Utilities.DocumentGenerator.Hierarchy](ExpressionPowerTools.Utilities.DocumentGenerator.Hierarchy.n.md) > **TypeCache**

Saves the "user-friendly" type and link.

```csharp
public class TypeCache
```

Inheritance [Object](https://docs.microsoft.com/dotnet/api/system.object) → **TypeCache**

## Constructors

| Ctor | Description |
| :-- | :-- |
| [TypeCache()](ExpressionPowerTools.Utilities.DocumentGenerator.Hierarchy.TypeCache.ctor.md#typecache) | Initializes a new instance of the [TypeCache](ExpressionPowerTools.Utilities.DocumentGenerator.Hierarchy.TypeCache.cs.md) class. |
| [static TypeCache()](ExpressionPowerTools.Utilities.DocumentGenerator.Hierarchy.TypeCache.ctor.md#static-typecache) | Initializes a new instance of the [TypeCache](ExpressionPowerTools.Utilities.DocumentGenerator.Hierarchy.TypeCache.cs.md) class. |
## Properties

| Property | Type | Description |
| :-- | :-- | :-- |
| [`Cache`](ExpressionPowerTools.Utilities.DocumentGenerator.Hierarchy.TypeCache.Cache.prop.md) | [TypeCache](ExpressionPowerTools.Utilities.DocumentGenerator.Hierarchy.TypeCache.cs.md) | Gets the global cache instance. |
| [`Index [Type]`](ExpressionPowerTools.Utilities.DocumentGenerator.Hierarchy.TypeCache.Item.prop.md) | [TypeRef](ExpressionPowerTools.Utilities.DocumentGenerator.Hierarchy.TypeRef.cs.md) | Indexer to type cache. |
| [`TypeCount`](ExpressionPowerTools.Utilities.DocumentGenerator.Hierarchy.TypeCache.TypeCount.prop.md) | [Int32](https://docs.microsoft.com/dotnet/api/system.int32) | Gets the count of cached types. |

## Methods

| Method | Description |
| :-- | :-- |
| [String GetFriendlyMethodName(MethodInfo method)](TypeCache-GetFriendlyMethodName.m.md) | Gets the friendly (with generic parameters) name for the method. |
| [Type GetTypeFromName(String name)](TypeCache-GetTypeFromName.m.md) | Get a [Type](https://docs.microsoft.com/dotnet/api/system.type) from the name. |
| [Void RegisterAssembly(DocAssembly assembly)](TypeCache-RegisterAssembly.m.md) | Register the [DocAssembly](ExpressionPowerTools.Utilities.DocumentGenerator.Hierarchy.DocAssembly.cs.md) . |

---

| Generated | Copyright | Version |
| :-- | :-: | --: |
| 8/15/2020 12:36:00 AM | (c) Copyright 2020 Jeremy Likness. | **v0.1.0.0** |