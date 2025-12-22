# Domain Events and Notification Handlers

Domain events represent **facts that occurred inside the domain** and are dispatched **only after a successful commit**.

---

### Domain Event Handler

```csharp
using Joaoaalves.FastCQRS.Abstractions.Notifications;
using Joaoaalves.FastCQRS.Core.Events;
using Example.Domain.Notes.Events;

namespace Example.Application.Notes.Events.NoteCreatedEvent
{
    public sealed class NoteCreatedEventHandler
        : INotificationHandler<DomainNotificationBase<NoteCreated>>
    {
        public Task Handle(
            DomainNotificationBase<NoteCreated> notification,
            CancellationToken cancellationToken)
        {
            Console.WriteLine("Notification Handled");
            return Task.CompletedTask;
        }
    }
}
```

Key points:

* Handlers implement `INotificationHandler<TNotification>`
* Notifications wrap domain events using `DomainNotificationBase<T>`
* Multiple handlers can exist for the same domain event
* Executed **after the transaction is committed**

This guarantees that side effects are never triggered if persistence fails.

---

## Execution Flow Summary

### Query Flow
**PLEASE, avoid creating Domain Events on queries!**
1. Query is dispatched
2. Handler is resolved
3. Data is returned
4. No transaction
5. No domain events

---

### Command Flow

1. Command is dispatched
2. Validation pipeline executes
3. Handler runs business logic
4. Unit of Work commits
5. Domain events are dispatched
6. Notification handlers are executed

---

## Important Constraints

* Commands **must not** return entities
* Queries **must not** change state
* Notification handlers **must not** rely on transactional context
* All handlers must live in assemblies registered in `AddFastCQRS`

These constraints are intentional and enforce architectural clarity.

---