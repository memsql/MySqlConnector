---
lastmod: 2021-09-21
date: 2018-09-29
title: Transaction Usage
customtitle: "Fix: The transaction associated with this command is not the connection’s active transaction"
weight: 20
menu:
  main:
    parent: troubleshooting
---

# The transaction associated with this command is not the connection’s active transaction

## Problem

When using `SingleStoreTransaction` from a C# program, you may receive the following error:

* **System.InvalidOperationException: The transaction associated with this command is not the connection's active transaction; see https://fl.vu/mysql-trans**

By default, SingleStoreConnector requires `SingleStoreCommand.Transaction` to be set to the connection's active transaction in order for the command to be executed successfully. This strictness is intended to catch programming bugs related to using the wrong transaction, a disposed transaction, or forgetting to set the transaction (and using the default value `null`).

However, this strictness can make migrating from Connector/NET more difficult, as it may require significant code changes to pass the current transaction through to all command objects. It can also be challenging when using a library like Dapper that creates the `SingleStoreCommand` objects itself.

## Workaround: Use IgnoreCommandTransaction=true

To easily migrate code from Connector/NET, use the `IgnoreCommandTransaction=true` connection string setting to emulate Connector/NET's behaviour and not validate the value of `SingleStoreCommand.Transaction`. By doing this, you will not need the code fixes prescribed below.

## Code Fix: Set SingleStoreCommand.Transaction

### ADO.NET example

```csharp
using var connection = new SingleStoreConnection(...);
connection.Open();

using var transaction = connection.BeginTransaction();
using var command = connection.CreateCommand();
command.CommandText = "SELECT ...";

// *** ADD THIS LINE ***
command.Transaction = transaction;

// otherwise, this will throw System.InvalidOperationException: The transaction associated with this command is not the connection's active transaction.
command.ExecuteScalar();

// ... remaining code
transaction.Commit();
```

### Dapper Example

```csharp
using var connection = new SingleStoreConnection(...);
connection.Open();
using var transaction = connection.BeginTransaction();

// this will throw System.InvalidOperationException: The transaction associated with this command is not the connection's active transaction.
connection.Query("SELECT ...");

// use this instead:
connection.Query("SELECT ...", transaction: transaction);

// ... remaining code
transaction.Commit();
```

## Further Reading

* [SingleStore bug 88611](https://bugs.mysql.com/bug.php?id=88611) reporting Connector/NET's behaviour as a bug
* [Issue #333](https://github.com/mysql-net/MySqlConnector/issues/333) for the addition of MySqlConnector's strict behaviour
* Issues [#405](https://github.com/mysql-net/MySqlConnector/issues/405), [#452](https://github.com/mysql-net/MySqlConnector/issues/452), [#457](https://github.com/mysql-net/MySqlConnector/issues/457) for users encountering this as a breaking change
