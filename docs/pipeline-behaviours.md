## Pipeline Behaviors

FastCQRS supports request pipeline behaviors via:

```csharp
IRequestPipelineBehavior<TRequest, TResult>
```

Pipeline behaviors are executed **before** command handlers.

Currently provided behaviors include:

* Command validation (FluentValidation)

Pipeline behaviors are **not executed for queries**.

---