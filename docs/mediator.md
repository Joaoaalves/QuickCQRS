# Mediator Behavior

The Mediator in QuickCQRS is **not a generic message bus**.
It is a deterministic coordinator responsible for executing CQRS requests according to well-defined rules.

---

## Responsibilities

The Mediator is responsible for:

* Dispatching **Commands**, **Queries**, and **Notifications**
* Resolving handlers via dependency injection
* Executing request pipeline behaviors
* Coordinating command execution with the Unit of Work
* Triggering domain event dispatch after successful commits

It does **not**:

* Perform retries
* Perform logging automatically
* Catch or swallow exceptions
* Infer behavior via conventions

All behavior is explicit and predictable.

---

## Supported Operations

The Mediator supports three categories of messages:

| Message Type | Interface           | Expected Handlers |
| ------------ | ------------------- | ----------------- |
| Command      | `ICommand<TResult>` | Exactly one       |
| Query        | `IQuery<TResult>`   | Exactly one       |
| Notification | `INotification`     | Zero or more      |

---

## Failure Transparency

QuickCQRS intentionally avoids wrapping exceptions.

This means:

* Domain exceptions propagate as-is
* Infrastructure exceptions are not swallowed
* Stack traces remain intact

This behavior is critical for debugging and observability in distributed systems.

---

## Summary of Guarantees

| Scenario                      | Behavior               |
| ----------------------------- | ---------------------- |
| Command without handler       | Exception              |
| Query without handler         | Exception              |
| Multiple handlers             | Configuration error    |
| Notification without handlers | No-op                  |
| Validation failure            | Command aborted        |
| Notification failure          | Exception after commit |

These guarantees make the Mediator **predictable, explicit, and safe** to reason about.

---
