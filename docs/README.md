# Introduction

**FastCQRS** is a .NET library that provides a foundational implementation of **CQRS** concepts, aiming to reduce the repetitive and error-prone work involved in setting up commands, queries, handlers, mediators, unit of work, and domain event dispatching.

It is **not a framework**.
FastCQRS does not attempt to dictate application structure, enforce conventions at runtime, or abstract infrastructure behind opaque mechanisms. Instead, it offers a set of **explicit building blocks** that can be composed according to the needs of each module or service.

The main goal of the library is to **accelerate development** in systems that adopt:

- CQRS
- Clean Architecture
- Domain-Driven Design (DDD)
- Distributed systems and microservices

FastCQRS provides the **baseline CQRS infrastructure** so that application modules can focus on business logic rather than repeatedly implementing mediators, pipelines, validation, unit of work coordination, and domain event propagation.

---

## Design Intent

FastCQRS is intentionally modular.

Each package exists to solve a **specific concern**, and consumers are expected to reference **only the packages required by the module** they are implementing. There is no “all-in-one” dependency.

This makes the library suitable for:

- Modular monoliths
- Independent microservices
- Background workers
- API-only services
- Internal tooling and batch processing

---

## Early-Stage Scope

FastCQRS is at an early stage of development and focuses primarily on:

- Establishing a solid CQRS execution pipeline
- Defining clear abstractions for commands, queries, and notifications
- Coordinating transactional boundaries via a Unit of Work
- Supporting domain events in a predictable and explicit way
- Enabling integration with persistence mechanisms without leaking infrastructure concerns into the domain

Advanced concerns such as messaging transports, retries, and outbox patterns are **explicitly out of scope** of the core and are expected to be implemented via separate modules or external integrations.

---

## Architectural Context

FastCQRS is designed to fit naturally into:

- **Clean Architecture**, where application and domain layers remain isolated from infrastructure
- **Distributed systems**, where each service owns its own persistence and execution model
- **CQRS-first designs**, where write and read concerns are treated independently

The library assumes that architectural decisions are made by the consumer. FastCQRS only provides the execution primitives required to support those decisions consistently.
