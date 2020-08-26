﻿# ExpressionEquivalency.ValuesAreEquivalent Method

[Index](../index.md) > [ExpressionPowerTools.Core](ExpressionPowerTools.Core.a.md) > [ExpressionPowerTools.Core.Comparisons](ExpressionPowerTools.Core.Comparisons.n.md) > [ExpressionEquivalency](ExpressionPowerTools.Core.Comparisons.ExpressionEquivalency.cs.md) > **ValuesAreEquivalent**

Attempts to compare values in various ways.

## Overloads

| Overload | Description |
| :-- | :-- |
| [ValuesAreEquivalent(Object source, Object target)](#valuesareequivalentobject-source-object-target) | Attempts to compare values in various ways. |
## ValuesAreEquivalent(Object source, Object target)

Attempts to compare values in various ways.

```csharp
public static Boolean ValuesAreEquivalent(Object source, Object target)
```

### Return Type

 [Boolean](https://docs.microsoft.com/dotnet/api/system.boolean)  - A flag indicating equivalency.

### Parameters

| Parameter | Type | Description |
| :-- | :-- | :-- |
| `source` | [Object](https://docs.microsoft.com/dotnet/api/system.object) | The source value. |
| `target` | [Object](https://docs.microsoft.com/dotnet/api/system.object) | The target value. |


## Remarks

If one side is `null` and other is not `null` , returns `false` . If the objects are the same reference,
            returns `true` . If the type implements [IEquatable&lt;T>](https://docs.microsoft.com/dotnet/api/system.iequatable-1) then the result of [System.IEquatable&lt;T1>.Equals](https://docs.microsoft.com/dotnet/api/System.IEquatable-1.Equals) is returned. If the type implements [IComparable](https://docs.microsoft.com/dotnet/api/system.icomparable) then the result is `true` if [System.IComparable.CompareTo](https://docs.microsoft.com/dotnet/api/System.IComparable.CompareTo) is `0` . Otherwise, the result of [System.Object.Equals](https://docs.microsoft.com/dotnet/api/System.Object.Equals) from
            the source to the target is returned.


---

| Generated | Copyright | Version |
| :-- | :-: | --: |
| 8/25/2020 6:00:34 PM | (c) Copyright 2020 Jeremy Likness. | 0.8.2-alpha |