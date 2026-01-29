# Notification Dispatching

Notifications are used to represent **domain events wrapped as application-level notifications**.

---

### Expected Behavior

When a notification is published:

1. All registered handlers are resolved
2. Handlers are executed sequentially
3. Failures propagate immediately

Notifications:

* Are executed **after** the Unit of Work commits
* Do not participate in transactions
* May have zero, one, or multiple handlers

---

### No Notification Handlers

If a notification has **no registered handlers**, nothing happens.

This is a valid and expected scenario.

---

### Notification Handler Failure

If a notification handler throws an exception:

* The exception is propagated
* The command has already been committed
* No rollback is performed

This behavior is deliberate and avoids coupling side effects to persistence success.

---