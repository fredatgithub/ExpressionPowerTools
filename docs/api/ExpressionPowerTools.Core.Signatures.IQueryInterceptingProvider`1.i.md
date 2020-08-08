﻿# IQueryInterceptingProvider&lt;T> Interface

[ExpressionPowerTools.Core](ExpressionPowerTools.Core.a.md) > [ExpressionPowerTools.Core.Signatures](ExpressionPowerTools.Core.Signatures.n.md) > **IQueryInterceptingProvider<T>**

Interface for provider that intercepts the [System.Linq.Expressions.Expression](https://docs.microsoft.com/dotnet/api/system.linq.expressions.expression) when run.

```csharp
public interface IQueryInterceptingProvider<T> : IQueryInterceptor, ICustomQueryProvider<T>
```

### Type Parameters

| Parameter Name | Description |
| :-- | :-- |
| `T` | The type. |

Implements  [ICustomQueryProvider<T>](ExpressionPowerTools.Core.Signatures.ICustomQueryProvider`1.i.md) ,  [IQueryInterceptor](ExpressionPowerTools.Core.Signatures.IQueryInterceptor.i.md) ,  [IQueryProvider](https://docs.microsoft.com/dotnet/api/system.linq.iqueryprovider) 

Derived  [QueryInterceptingProvider<T>](ExpressionPowerTools.Core.Providers.QueryInterceptingProvider`1.cs.md) 
