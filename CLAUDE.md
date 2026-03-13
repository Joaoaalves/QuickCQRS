# QuickCQRS — Claude Guide

**QuickCQRS** is a modular .NET library that provides CQRS building blocks for Clean Architecture and DDD applications. It is **not a framework** — it gives you explicit, composable primitives without hidden conventions or runtime magic.

Full documentation: **https://joaoaalves.github.io/QuickCQRS**

---

## Packages

| Package | Purpose | Layer |
|---|---|---|
| `Joaoaalves.QuickCQRS.Abstractions` | Interfaces for commands, queries, notifications, mediator, UoW | Domain / Application |
| `Joaoaalves.QuickCQRS.Core` | Mediator, executors, handler discovery, validation pipeline | Application |
| `Joaoaalves.QuickCQRS.Persistence` | Unit of Work base implementation, pipeline behaviour | Infrastructure |
| `Joaoaalves.QuickCQRS.Persistence.EntityFramework` | EF Core adapters for DbContext and domain event extraction | Infrastructure |

Packages are hosted on **GitHub Packages** and require a `NuGet.config` with a GitHub token (`read:packages`).

---

## Core Concepts

### Commands
- Inherit from `BaseCommand<TResult>` (from `Core`)
- Exactly one `ICommandHandler<TCommand, TResult>` per command
- Always execute inside a **Unit of Work** (automatic commit/revert)
- May raise domain events inside aggregates
- Must NOT return entities — return IDs or value objects

### Queries
- Implement `IQuery<TResult>` (from `Abstractions`)
- Exactly one `IQueryHandler<TQuery, TResult>` per query
- No transaction, no Unit of Work, no domain events
- Must NOT change state

### Notifications (Domain Events)
- Implement `INotification` (from `Abstractions`)
- Zero or more `INotificationHandler<TNotification>` handlers allowed
- Dispatched **after** the Unit of Work commits — outside the transactional boundary
- A handler failure does not roll back the already-committed command

### Mediator
- Resolves handlers via DI using reflection
- Executes pipeline behaviours before the handler
- Does NOT log, retry, or swallow exceptions
- Exceptions always propagate with the original stack trace

### Unit of Work
- Implemented as a **pipeline behaviour** — never called manually
- Scoped per command execution
- On success: dispatches domain events, then calls `SaveChangesAsync`
- On failure: calls `RevertAsync` and rethrows

### Pipeline Behaviours
- Middleware-style hooks that wrap the handler call
- Built-in: `CommandValidationBehavior` (FluentValidation)
- Custom behaviours implement `IRequestPipelineBehavior<TRequest, TResponse>`

---

## Minimal Setup

```csharp
// Composition root (API / Host layer)
services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

services.AddQuickCQRS(options =>
{
    options.AddAssembly<ApplicationAssemblyMarker>();
});

services.AddEfUnitOfWork<AppDbContext>();
```

`AddAssembly<T>()` scans the assembly containing `T` for all command, query, and notification handlers.

---

## Execution Flows

**Command:**
dispatch → validation pipeline → handler → CommitAsync (dispatch events → SaveChanges) → return result

**Query:**
dispatch → handler → return result

**Failure (command):**
exception → RevertAsync → exception rethrows (no domain events dispatched)

---

## Project Structure

```
src/
  Joaoaalves.QuickCQRS.Abstractions/     # Interfaces only
  Joaoaalves.QuickCQRS.Core/             # Mediator, executors, DI module
  Joaoaalves.QuickCQRS.Persistence/      # UoW base + pipeline behaviour
  Joaoaalves.QuickCQRS.Persistence.EntityFramework/  # EF Core adapters
tests/
  Joaoaalves.QuickCQRS.Core.Tests/
  Joaoaalves.QuickCQRS.Persistence.Tests/
  Joaoaalves.QuickCQRS.Persistence.EntityFramework.Tests/
docs/                                    # Docsify documentation
```

---

## Design Principles

- **Explicit over implicit** — no hidden conventions or runtime discovery by default
- **Failure transparency** — exceptions are never caught or wrapped
- **Modular** — reference only the packages your layer needs
- **Deterministic** — execution flow is always predictable and auditable
