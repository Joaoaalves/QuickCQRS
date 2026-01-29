# Quickstart

This section describes the **minimum configuration required** to use QuickCQRS in a real application.
The goal is to make explicit **what must be configured** and **why**, avoiding hidden behavior.

QuickCQRS is split across multiple packages. Each layer should reference **only the abstractions it needs**, while **infrastructure or API layers** are responsible for wiring everything together.

---

## Installation (GitHub Packages)

At the moment, QuickCQRS packages are hosted on **GitHub Packages**.

### 1. Configure NuGet Source

Create or update your `NuGet.config`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="github" value="https://nuget.pkg.github.com/Joaoaalves/index.json" />
  </packageSources>

  <packageSourceCredentials>
    <github>
      <add key="Username" value="YOUR_GITHUB_USERNAME" />
      <add key="ClearTextPassword" value="YOUR_GITHUB_TOKEN" />
    </github>
  </packageSourceCredentials>
</configuration>
```

The token must have at least the `read:packages` permission.

Repository:
[https://github.com/Joaoaalves/QuickCQRS](https://github.com/Joaoaalves/QuickCQRS)

---

## Package Selection by Layer

A typical Clean Architecture setup would look like this:

### Application Layer

* `Joaoaalves.QuickCQRS.Abstractions`
* `Joaoaalves.QuickCQRS.Core`

### Infrastructure Layer (Persistence)

* `Joaoaalves.QuickCQRS.Persistence`
* `Joaoaalves.QuickCQRS.Persistence.EntityFramework`

### API / Composition Root

* References all required modules and performs configuration

---

## Core Configuration (Mandatory)

QuickCQRS requires the application to **explicitly expose the assemblies** that contain:

* Commands
* Queries
* Handlers
* Notifications

This is done via the **Core module**.

---

### Core Module Registration

```csharp
using Joaoaalves.QuickCQRS.Core.Modules;

services.AddQuickCQRS(options =>
{
    options.AddAssembly<ApplicationAssemblyMarker>();
});
```

Using a marker type is the recommended approach to ensure that the correct application assembly is scanned.

---

### What the Core Module Does

The `AddQuickCQRS` extension method configures:

* The internal Mediator
* Command and query executors
* Domain events dispatcher
* Validation pipeline behaviors
* Handler discovery via assembly scanning

Internally, it registers:

* `IMediator`
* `CommandsExecutor`
* `QueriesExecutor`
* `IDomainEventsDispatcher`
* `IRequestPipelineBehavior<,>` (validation)

---

### Assembly Resolution Rules

QuickCQRS resolves assemblies as follows:

1. If assemblies are explicitly provided via `QuickCQRSOptions`, they are:

   * Forced to load
   * Used exclusively for handler scanning
2. If none are provided:

   * All loaded, non-dynamic assemblies are scanned

Explicit assembly registration is **strongly recommended** in modular systems and microservices.

---

## Entity Framework Core Integration

If your application uses Entity Framework Core, you must explicitly register the EF-based persistence module.

---

### EF Unit of Work Registration

```csharp
using Joaoaalves.QuickCQRS.Persistence.EntityFramework.Modules;

services.AddEfUnitOfWork<AppDbContext>();
```

---

### What This Configuration Provides

Registering `AddEfUnitOfWork<TDbContext>` configures:

* `IDatabaseContext` backed by EF Core
* EF-based `IUnitOfWork`
* `IDomainEventsProvider` that extracts domain events from tracked entities
* Scoped `DbContext`

This ensures that:

* Each command execution runs within a single transactional boundary
* Domain events are collected from aggregates
* Events are dispatched only after a successful commit

---

## Minimal End-to-End Setup

A minimal API or composition root setup would look like this:

```csharp
services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

services.AddQuickCQRS(options =>
{
    options.AddAssembly<ApplicationAssemblyMarker>();
});

services.AddEfUnitOfWork<AppDbContext>();
```

This is sufficient to:

* Dispatch commands and queries
* Execute handlers
* Validate commands
* Persist changes
* Dispatch domain events

---

## Layer Responsibilities Summary

| Layer          | Responsibility                         |
| -------------- | -------------------------------------- |
| Domain         | Entities, value objects, domain events |
| Application    | Commands, queries, handlers            |
| Infrastructure | EF Core, persistence implementations   |
| API / Host     | Module registration and composition    |

QuickCQRS intentionally keeps these responsibilities **explicit and separate**.

---

## Important Notes

* Only **infrastructure or API layers** should reference module packages
* Domain and application layers should depend only on abstractions
* No runtime magic or implicit discovery is performed
* Assembly scanning is explicit and deterministic

---