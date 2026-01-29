# Command Dispatching

### Expected Behavior

When a command is sent:

1. The Mediator resolves the command handler
2. Pipeline behaviors are executed (e.g. validation)
3. The command handler is invoked
4. The Unit of Work commits
5. Domain events are dispatched
6. Notification handlers are executed

All steps must succeed for the command to complete.

---

### Missing Command Handler

If a command is dispatched and **no handler is registered**, the Mediator throws an exception immediately.

**Example scenario:**

* Command type exists
* No `ICommandHandler<TCommand, TResult>` is registered in DI
* Assembly containing the handler was not scanned

**Resulting behavior:**

* No pipeline execution
* No Unit of Work created
* No side effects

**Typical exception:**

```text
InvalidOperationException: "No handler registered for request type 'CreateNote'"
```

This behavior is intentional and prevents silent failures.

---

### Multiple Command Handlers

If more than one handler is registered for the same command type, this is treated as a configuration error.

**Resulting behavior:**

* Application startup or first resolution fails
* Dependency Injection throws an exception

QuickCQRS enforces **exactly one handler per command**.

---