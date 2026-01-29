# Query Dispatching

### Expected Behavior

When a query is sent:

1. The Mediator resolves the query handler
2. The handler is invoked directly
3. The result is returned

Queries:

* Do not create a Unit of Work
* Do not execute domain event dispatching
* Do not run transactional logic

---

### Missing Query Handler

If a query is dispatched and no handler is found, the Mediator throws an exception.

**Typical exception:**

```text
InvalidOperationException: "No handler registered for request type 'ListNotes'"
```

This ensures that read paths are always explicit.

---

### Multiple Query Handlers

Queries must also have **exactly one handler**.

Multiple handlers result in a DI resolution failure and are considered an invalid configuration.

---