---
name: quickcqrs
description: Read and understand QuickCQRS documentation. Use when working on a project that uses the QuickCQRS library and you need to understand commands, queries, notifications, mediator, unit of work, pipeline behaviours, or EF Core integration.
---

You are helping a developer who is using **QuickCQRS**, a .NET CQRS library. Your job is to read the relevant documentation pages and answer questions or assist with implementation.

## Documentation Site

Base URL: **https://joaoaalves.github.io/QuickCQRS**

The site is built with Docsify. All pages follow the pattern: `https://joaoaalves.github.io/QuickCQRS/#/<page>`

---

## Topic → URL Map

Use this map to decide which page(s) to fetch when answering a question:

| Topic | URL |
|---|---|
| Introduction / Overview | https://joaoaalves.github.io/QuickCQRS/#/ |
| Package dependencies | https://joaoaalves.github.io/QuickCQRS/#/dependencies |
| Installation & minimal setup | https://joaoaalves.github.io/QuickCQRS/#/quickstart |
| CQRS overview | https://joaoaalves.github.io/QuickCQRS/#/cqrs |
| Commands (definition & handlers) | https://joaoaalves.github.io/QuickCQRS/#/commands |
| Queries (definition & handlers) | https://joaoaalves.github.io/QuickCQRS/#/queries |
| Notifications / Domain Events | https://joaoaalves.github.io/QuickCQRS/#/notifications |
| Mediator behavior & guarantees | https://joaoaalves.github.io/QuickCQRS/#/mediator |
| Dispatching commands | https://joaoaalves.github.io/QuickCQRS/#/commands-dispatching |
| Dispatching queries | https://joaoaalves.github.io/QuickCQRS/#/queries-dispatching |
| Dispatching notifications | https://joaoaalves.github.io/QuickCQRS/#/notifications-dispatching |
| Pipeline behaviours | https://joaoaalves.github.io/QuickCQRS/#/pipeline-behaviours |
| Validation pipeline (FluentValidation) | https://joaoaalves.github.io/QuickCQRS/#/validation-pipeline |
| Unit of Work | https://joaoaalves.github.io/QuickCQRS/#/uow |
| Entity Framework Core integration | https://joaoaalves.github.io/QuickCQRS/#/efcore |

> **Note:** Docsify renders markdown files. If a `#/page` URL returns a shell page without content, fetch the raw markdown directly from GitHub:
> `https://raw.githubusercontent.com/Joaoaalves/QuickCQRS/main/docs/<page>.md`

---

## How to Use This Skill

1. Identify which topic(s) the developer is asking about using the map above.
2. Fetch the relevant page(s) with `WebFetch`.
3. Use the fetched content to answer the question or generate the requested code.
4. When in doubt, start with `#/quickstart` for setup questions and `#/cqrs` for conceptual questions.

---

## Library At a Glance

**QuickCQRS** is a modular .NET library (not a framework) that provides CQRS building blocks:

- **Commands** — state-changing operations, one handler each, run inside a Unit of Work
- **Queries** — read-only, one handler each, no transaction
- **Notifications** — domain events, zero or more handlers, run after commit
- **Mediator** — resolves and executes handlers via DI; deterministic, no retries, no logging
- **Unit of Work** — automatic commit/revert pipeline behavior for commands only
- **Pipeline Behaviours** — middleware-style hooks on the request pipeline (e.g. validation)

**Packages:**
| Package | Layer |
|---|---|
| `Joaoaalves.QuickCQRS.Abstractions` | Application / Domain |
| `Joaoaalves.QuickCQRS.Core` | Application |
| `Joaoaalves.QuickCQRS.Persistence` | Infrastructure |
| `Joaoaalves.QuickCQRS.Persistence.EntityFramework` | Infrastructure |

**Minimal DI setup:**
```csharp
services.AddQuickCQRS(options =>
{
    options.AddAssembly<ApplicationAssemblyMarker>();
});

services.AddEfUnitOfWork<AppDbContext>();
```

---

## Key Rules to Always Respect

- Commands must NOT return entities — return IDs or value objects only.
- Queries must NOT change state.
- Notification handlers must NOT rely on transactional context (commit already happened).
- Exactly **one** handler per command/query; zero or more per notification.
- All handlers must be in a registered assembly.
- Exceptions propagate transparently — QuickCQRS never swallows them.
