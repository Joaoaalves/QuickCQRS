## Defining Commands, Queries and Notifications

Once the core and persistence modules are configured, the next step is to define **application messages** and their respective handlers.

QuickCQRS distinguishes explicitly between:

* **[Commands](commands.md)** → state-changing operations
* **[Queries](queries.md)** → read-only operations
* **[Notifications (Domain Events)](notifications.md)** → side effects after successful commits

All of them are resolved via assembly scanning configured in `AddQuickCQRS`.