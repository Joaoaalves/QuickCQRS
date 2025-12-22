# Validation Failures

If validation fails during pipeline execution:

* The command handler is not executed
* The Unit of Work is not created
* A validation exception is thrown

This ensures that invalid state never reaches the domain.

---

## Assembly Scanning and Handler Resolution

All handlers must be discoverable via assemblies registered in:

```csharp
services.AddFastCQRS(options =>
{
    options.AddAssembly<ApplicationAssemblyMarker>();
});
```

Common misconfiguration errors include:

* Forgetting to register the application assembly
* Registering the wrong marker type
* Using dynamically loaded assemblies not scanned

These issues typically surface as **missing handler exceptions** at runtime.

---