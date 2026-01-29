# Dependencies

This section lists the **direct dependencies added to a project** when referencing each QuickCQRS package.

---

## Joaoaalves.QuickCQRS.Abstractions

**Purpose:**
Defines the core contracts shared across the library (commands, queries, notifications, handlers, unit of work interfaces).

### Dependencies

* `Joaoaalves.DDD`

---

## Joaoaalves.QuickCQRS.Core

**Purpose:**
Provides the CQRS execution pipeline, mediator implementation, handler resolution, and validation integration.

### Dependencies

* `Joaoaalves.DDD`
* `FluentValidation`
* `Microsoft.Extensions.DependencyInjection`

---

## Joaoaalves.QuickCQRS.Persistence

**Purpose:**
Defines persistence-related abstractions and base implementations that are independent of any specific ORM.

### Dependencies

* `Joaoaalves.DDD`
* `Microsoft.Extensions.DependencyInjection`

---

## Joaoaalves.QuickCQRS.Persistence.EntityFramework

**Purpose:**
Provides Entity Framework Core–specific implementations, including Unit of Work coordination and domain event extraction.

### Dependencies

* `Joaoaalves.DDD`
* `Microsoft.EntityFrameworkCore`
