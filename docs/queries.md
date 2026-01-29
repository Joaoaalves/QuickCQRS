# Queries

Queries represent **read-only requests** and must not mutate application state.

---

### Query Definition

```csharp
using Joaoaalves.QuickCQRS.Abstractions.Queries;
using Example.Domain.Notes;

namespace Example.Application.Notes.Queries.ListNotes
{
    public sealed class ListNotes : IQuery<IEnumerable<Note>>
    {
    }
}
```

Key points:

* Implements `IQuery<TResult>`
* Contains only the data required to execute the read
* Has no behavior

---

### Query Handler

```csharp
using Joaoaalves.QuickCQRS.Abstractions.Queries;
using Example.Domain.Notes;

namespace Example.Application.Notes.Queries.ListNotes
{
    public sealed class ListNotesQueryHandler(
        INoteRepository noteRepository
    ) : IQueryHandler<ListNotes, IEnumerable<Note>>
    {
        private readonly INoteRepository _noteRepo = noteRepository;

        public async Task<IEnumerable<Note>> Handle(
            ListNotes request,
            CancellationToken cancellationToken)
        {
            var notes = await _noteRepo.List();
            return notes;
        }
    }
}
```

Characteristics:

* Exactly one handler per query
* No transaction or Unit of Work involved
* Can return entities, projections, or DTOs
* Should never raise domain events

---