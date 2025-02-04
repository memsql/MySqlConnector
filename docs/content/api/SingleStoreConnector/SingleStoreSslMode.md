# SingleStoreSslMode enumeration

SSL connection options.

```csharp
public enum SingleStoreSslMode
```

## Values

| name | value | description |
| --- | --- | --- |
| None | `0` | Do not use SSL. |
| Disabled | `0` | Do not use SSL. This is the same as None. |
| Preferred | `1` | Use SSL if the server supports it. |
| Required | `2` | Always use SSL. Deny connection if server does not support SSL. |
| VerifyCA | `3` | Always use SSL. Validate the Certificate Authority but tolerate name mismatch. |
| VerifyFull | `4` | Always use SSL. Fail if the host name is not correct. |

## See Also

* namespace [SingleStoreConnector](../SingleStoreConnector.md)

<!-- DO NOT EDIT: generated by xmldocmd for SingleStoreConnector.dll -->
