# Claude Code Skill

QuickCQRS ships a **Claude Code skill** that teaches Claude about the library — its packages, patterns, and documentation — so you can get accurate, context-aware help without having to explain the library yourself.

---

## What the Skill Does

When activated, the skill tells Claude:

- What QuickCQRS is and how it is structured
- Which documentation page covers each topic (commands, queries, notifications, mediator, unit of work, pipeline behaviours, EF Core, etc.)
- Where to fetch content when it needs to look something up
- The key rules and constraints of the library (e.g. commands must not return entities, queries must not change state)

---

## Installation

The skill is a single markdown file. Add it to your project's `.claude/skills/` directory.

### Option 1 — curl (Linux / macOS / Git Bash)

```bash
mkdir -p .claude/skills
curl -o .claude/skills/quickcqrs.md \
  https://raw.githubusercontent.com/Joaoaalves/QuickCQRS/main/.claude/skills/quickcqrs.md
```

### Option 2 — PowerShell (Windows)

```powershell
New-Item -ItemType Directory -Force -Path .claude/skills | Out-Null
Invoke-WebRequest `
  -Uri https://raw.githubusercontent.com/Joaoaalves/QuickCQRS/main/.claude/skills/quickcqrs.md `
  -OutFile .claude/skills/quickcqrs.md
```

### Option 3 — Manual

1. Download [quickcqrs.md](https://raw.githubusercontent.com/Joaoaalves/QuickCQRS/main/.claude/skills/quickcqrs.md)
2. Place it in `.claude/skills/quickcqrs.md` inside your project root

---

## Usage

Once installed, invoke the skill from the Claude Code CLI with:

```
/quickcqrs
```

Then ask your question naturally, for example:

```
/quickcqrs How do I create a command handler with validation?
/quickcqrs What happens if a notification handler throws an exception?
/quickcqrs Set up Unit of Work with EF Core in my project
```

Claude will fetch the relevant documentation page(s) and answer with full context about the library.

---

## What Gets Installed

The skill file is placed **only in your project directory** under `.claude/skills/`. It does not modify any global Claude settings. You can delete or update it at any time.

---

## Keeping It Updated

To update the skill to the latest version, re-run the install command from above. It overwrites the existing file.
