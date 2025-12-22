# Entity Framework Core Integration

The integration is based on **adapters**, not inheritance or direct coupling.

---

## Design Goals

The EF Core integration is designed to:

* Keep EF Core isolated in the infrastructure layer
* Avoid leaking `DbContext` into application or domain code
* Support domain event collection from tracked aggregates
* Integrate seamlessly with the Unit of Work pipeline
* Preserve Clean Architecture boundaries

FastCQRS treats EF Core as an **implementation detail**, not as a core dependency.

---

## Registration Module

EF Core integration is enabled via an explicit module.

---

### EntityFrameworkPersistenceModule

```csharp
using Joaoaalves.FastCQRS.Persistence.EntityFramework.Modules;

services.AddEfUnitOfWork<AppDbContext>();
```

This single call wires all EF-related infrastructure.

---

### What Gets Registered

Calling `AddEfUnitOfWork<TDbContext>` registers:

| Service                 | Implementation              |
| ----------------------- | --------------------------- |
| `IDatabaseContext`      | `EFDatabaseContextAdapter`  |
| `IUnitOfWork`           | Default FastCQRS UnitOfWork |
| `DbContext`             | `TDbContext`                |
| `IDomainEventsProvider` | `EFDomainEventsProvider`    |

All services are registered with **scoped lifetime**.

---

## IDatabaseContext Adapter

FastCQRS abstracts persistence behind `IDatabaseContext`.

---

### EFDatabaseContextAdapter

```csharp
public class EFDatabaseContextAdapter(DbContext context) : IDatabaseContext
{
    private readonly DbContext _context = context;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public void RevertChanges()
    {
        foreach (var entry in _context.ChangeTracker.Entries<Entity>())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                case EntityState.Deleted:
                    entry.State = EntityState.Unchanged;
                    break;

                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
            }
        }
    }
}
```

---

### Responsibilities

The adapter:

* Delegates persistence to `DbContext`
* Normalizes rollback behavior across providers
* Reverts tracked entity state on failure

The Unit of Work never interacts with EF Core directly.

---

## Domain Events Integration

Domain events are collected directly from EF Core’s change tracker.

---

### EFDomainEventsProvider

```csharp
public class EFDomainEventsProvider(DbContext db) : IDomainEventsProvider
{
    private readonly DbContext _db = db;

    public IReadOnlyCollection<IDomainEvent> CollectDomainEvents()
    {
        return _db.ChangeTracker.Entries<Entity>()
            .Where(e => e.Entity.DomainEvents is { Count: > 0 })
            .SelectMany(e => e.Entity.DomainEvents!)
            .ToList();
    }

    public void ClearCollectedDomainEvents()
    {
        var entities = _db.ChangeTracker.Entries<Entity>()
            .Where(e => e.Entity.DomainEvents is { Count: > 0 });

        foreach (var entry in entities)
            entry.Entity.ClearDomainEvents();
    }
}
```

---

### Domain Requirements

To participate in domain event dispatching:

* Aggregates must inherit from `Joaoaalves.DDD.Common.Entity`
* Domain events must implement `IDomainEvent`
* Events must be added via the aggregate

```csharp
AddDomainEvent(new NoteCreated(note.Id));
```

---

## End-to-End Command Flow with EF Core

When a command is executed:

1. Command handler mutates aggregates
2. EF Core tracks changes
3. Domain events are added to aggregates
4. Unit of Work is triggered
5. Domain events are collected via EF change tracker
6. Domain events are dispatched
7. EF Core persists changes
8. Domain events are cleared

This flow is deterministic and explicit.

---

## Transaction Boundaries

* One `DbContext` instance per command execution
* One Unit of Work per command
* Queries do not use EF transactions
* Notification handlers run outside EF transactions

FastCQRS does not create explicit `DbTransaction` scopes.
Transaction behavior is delegated to EF Core.

---

## Failure Scenarios

### Command Handler Failure

* EF Core tracked changes are reverted
* Domain events are not dispatched
* Exception propagates

---

### Persistence Failure

* Changes are reverted
* Domain events may have been dispatched
* Exception propagates

This behavior is intentional and avoids implicit retries or compensations.

---

## What This Integration Does Not Provide

The EF Core module does **not**:

* Provide repositories
* Configure mappings
* Manage migrations
* Handle distributed transactions
* Implement outbox or inbox patterns

These concerns remain the responsibility of the consuming application.

---

## Architectural Guarantees

| Concern                  | Guarantee  |
| ------------------------ | ---------- |
| EF isolation             | Preserved  |
| Clean Architecture       | Enforced   |
| Explicit wiring          | Required   |
| Domain event consistency | Guaranteed |
| Provider replacement     | Supported  |

---

## Summary

The Entity Framework Core integration in FastCQRS:

* Uses adapters instead of inheritance
* Keeps EF Core out of the application layer
* Integrates cleanly with the Unit of Work
* Provides predictable domain event handling
* Avoids hidden behavior or implicit conventions

It is designed to be simple, explicit, and extensible.

---