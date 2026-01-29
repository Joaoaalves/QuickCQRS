# Unit of Work

QuickCQRS treats the Unit of Work as an **execution concern**, not as an ORM abstraction.
Its responsibility is to **coordinate persistence and domain event dispatching** in a deterministic way.

---

## Purpose

The Unit of Work exists to ensure that:

* All state changes caused by a command are persisted atomically
* Domain events are dispatched at a well-defined moment
* Failures result in consistent rollback behavior
* Application code does not manually control transactions

The application layer is never responsible for committing or reverting changes.

---

## Core Abstraction

```csharp
public interface IUnitOfWork
{
    Task<int> CommitAsync(CancellationToken cancellationToken);
    Task RevertAsync();
}
```

The abstraction is intentionally minimal:

* No transaction APIs
* No ORM-specific concepts
* No lifecycle management exposed to handlers

---

## Default Implementation

QuickCQRS provides a default `UnitOfWork` implementation in the persistence layer.

```csharp
public class UnitOfWork(
    IDatabaseContext context,
    IDomainEventsDispatcher domainEventsDispatcher) : IUnitOfWork
{
    private readonly IDatabaseContext _context = context;
    private readonly IDomainEventsDispatcher _domainEventsDispatcher = domainEventsDispatcher;

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        await _domainEventsDispatcher.DispatchEventsAsync();
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public Task RevertAsync()
    {
        _context.RevertChanges();
        return Task.CompletedTask;
    }
}
```

---

## Responsibilities Breakdown

### CommitAsync

`CommitAsync` performs two operations in sequence:

1. Dispatches all collected domain events
2. Persists changes via the underlying database context

This ordering is **intentional** and defines the consistency model used by QuickCQRS.

---

### RevertAsync

`RevertAsync`:

* Reverts all tracked changes in the persistence context
* Is invoked only when an exception occurs during command execution

It does **not** attempt to compensate side effects or external calls.

---

## Pipeline Integration

The Unit of Work is not invoked manually.
It is executed automatically via a **pipeline behavior**.

---

### UnitOfWorkPipelineBehavior

```csharp
public class UnitOfWorkPipelineBehavior<TRequest, TResponse>(
    IUnitOfWork unitOfWork)
    : IRequestPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    private readonly IUnitOfWork _uow = unitOfWork;

    public async Task<TResponse> Handle(
        TRequest request,
        Func<Task<TResponse>> next,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await next();
            await _uow.CommitAsync(cancellationToken);
            return result;
        }
        catch
        {
            await _uow.RevertAsync();
            throw;
        }
    }
}
```

---

## Execution Flow

For **commands only**, the execution flow is:

1. Command is dispatched
2. Validation pipeline executes
3. Command handler runs
4. `CommitAsync` is invoked
5. Domain events are dispatched
6. Changes are persisted
7. Result is returned

If **any exception** occurs:

1. Execution is interrupted
2. `RevertAsync` is invoked
3. Exception is rethrown

---

## Scope and Lifetime

* One Unit of Work instance is created per command execution
* The lifetime is **scoped**
* Queries do not create or use a Unit of Work
* Notification handlers do not participate in the Unit of Work

This enforces a clear transactional boundary.

---

## Domain Events Interaction

Domain events are:

* Raised inside aggregates
* Collected by the persistence context
* Dispatched during `CommitAsync`

Important characteristics:

* Events are dispatched **only if the command succeeds**
* Events are dispatched **before persistence is finalized**
* Notification handlers run **outside the transactional context**

This prevents side effects from running when persistence fails.

---

## Failure Scenarios

### Exception in Command Handler

* Changes are reverted
* No domain events are dispatched
* Exception propagates

---

### Exception During Commit

* Revert is invoked
* Exception propagates
* No retry is performed

---

### Exception in Notification Handler

* Commit has already completed
* No rollback is performed
* Exception propagates

This behavior is intentional and avoids implicit compensations.

---

## What the Unit of Work Does Not Do

The Unit of Work deliberately does **not**:

* Manage distributed transactions
* Handle retries
* Provide isolation levels
* Perform logging
* Handle outbox or inbox patterns

These concerns are expected to be handled by higher-level infrastructure or external components.

---

## Architectural Guarantees

| Concern                  | Guarantee       |
| ------------------------ | --------------- |
| Transaction scope        | One per command |
| Queries                  | No Unit of Work |
| Automatic commit         | Yes             |
| Automatic rollback       | Yes             |
| Domain event consistency | Guaranteed      |
| Exception transparency   | Guaranteed      |

---

## Summary

The QuickCQRS Unit of Work provides:

* Deterministic transactional boundaries
* Explicit failure behavior
* Clean separation between application and infrastructure
* Predictable domain event dispatching

It is intentionally simple, explicit, and aligned with Clean Architecture and CQRS principles.

---