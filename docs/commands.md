## Commands

Commands represent **intent to change state** and always execute within a transactional boundary.

---

### Command Definition

```csharp
using Joaoaalves.FastCQRS.Core.Commands;

namespace Example.Application.Notes.Commands.CreateNoteCommand
{
    public class CreateNote(CreateNoteRequest request) : BaseCommand<Guid>
    {
        public string Title { get; } = request.Title;
    }
}
```

Key points:

* Commands inherit from `BaseCommand<TResult>`
* Represent an explicit intention
* Carry only the data required for execution

---

### Command Handler

```csharp
using Joaoaalves.FastCQRS.Abstractions.Commands;
using Example.Domain.Notes;

namespace Example.Application.Notes.Commands.CreateNoteCommand
{
    public class CreateNoteHandler(
        INoteRepository noteRepository
    ) : ICommandHandler<CreateNote, Guid>
    {
        private readonly INoteRepository _noteRepository = noteRepository;

        public async Task<Guid> Handle(
            CreateNote request,
            CancellationToken cancellationToken)
        {
            var note = Note.Create(request.Title);

            await _noteRepository.Add(note);

            return note.Id;
        }
    }
}
```

Characteristics:

* Exactly one handler per command
* Executed inside a Unit of Work
* Changes are committed automatically
* Domain events raised inside aggregates are collected

---
