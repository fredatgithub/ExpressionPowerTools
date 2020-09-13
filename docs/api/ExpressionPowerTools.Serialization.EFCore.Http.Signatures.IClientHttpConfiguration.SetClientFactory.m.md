﻿# IClientHttpConfiguration.SetClientFactory Method

[Index](../index.md) > [ExpressionPowerTools.Serialization.EFCore.Http](ExpressionPowerTools.Serialization.EFCore.Http.a.md) > [ExpressionPowerTools.Serialization.EFCore.Http.Signatures](ExpressionPowerTools.Serialization.EFCore.Http.Signatures.n.md) > [IClientHttpConfiguration](ExpressionPowerTools.Serialization.EFCore.Http.Signatures.IClientHttpConfiguration.i.md) > **SetClientFactory**

Set the client factory.

## Overloads

| Overload | Description |
| :-- | :-- |
| [SetClientFactory(Func&lt;IRemoteQueryClient> factory)](#setclientfactoryfunciremotequeryclient-factory) | Set the client factory. |
## SetClientFactory(Func&lt;IRemoteQueryClient> factory)

Set the client factory.

```csharp
public virtual IClientHttpConfiguration SetClientFactory(Func<IRemoteQueryClient> factory)
```

### Return Type

 [IClientHttpConfiguration](ExpressionPowerTools.Serialization.EFCore.Http.Signatures.IClientHttpConfiguration.i.md)  - The [IClientHttpConfiguration](ExpressionPowerTools.Serialization.EFCore.Http.Signatures.IClientHttpConfiguration.i.md) .

### Parameters

| Parameter | Type | Description |
| :-- | :-- | :-- |
| `factory` | [Func&lt;IRemoteQueryClient>](https://docs.microsoft.com/dotnet/api/system.func-1) | The factory to use. |



---

| Generated | Copyright | Version |
| :-- | :-: | --: |
| 9/13/2020 7:35:36 AM | (c) Copyright 2020 Jeremy Likness. | 0.8.8-alpha |