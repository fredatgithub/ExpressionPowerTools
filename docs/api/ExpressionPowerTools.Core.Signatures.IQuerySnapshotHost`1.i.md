﻿# IQuerySnapshotHost&lt;T> Interface

[ExpressionPowerTools.Core](ExpressionPowerTools.Core.a.md) > [ExpressionPowerTools.Core.Signatures](ExpressionPowerTools.Core.Signatures.n.md) > **IQuerySnapshotHost<T>**

Host to snapshot a query. Will raise an event when it is executed
            to allow inspecting the expression.

```csharp
public interface IQuerySnapshotHost<T> : IQueryHost<T, IQuerySnapshotProvider<T>>
```

### Type Parameters

| Parameter Name | Description |
| :-- | :-- |
| `T` | The type of entity. |

Implements  [IEnumerable](https://docs.microsoft.com/dotnet/api/system.collections.ienumerable) ,  [IEnumerable&lt;T>](https://docs.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1) ,  [IOrderedQueryable](https://docs.microsoft.com/dotnet/api/system.linq.iorderedqueryable) ,  [IOrderedQueryable&lt;T>](https://docs.microsoft.com/dotnet/api/system.linq.iorderedqueryable-1) ,  [IQueryable](https://docs.microsoft.com/dotnet/api/system.linq.iqueryable) ,  [IQueryable&lt;T>](https://docs.microsoft.com/dotnet/api/system.linq.iqueryable-1) ,  [IQueryHost<T, TProvider>](ExpressionPowerTools.Core.Signatures.IQueryHost`2.i.md) 

Derived  [QuerySnapshotHost<T>](ExpressionPowerTools.Core.Hosts.QuerySnapshotHost`1.cs.md) 
