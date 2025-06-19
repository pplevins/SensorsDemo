# Investigation Game v2

> A turn-based deduction game where you attach sensors to uncover covert Iranian agents. Built with **Clean Architecture**, **Dependency Injection**, and **EF Core-ready infrastructure**. This version includes a console-based interface and is prepared for MySQL persistence.

---

## 🧠 Project Architecture

This project follows the **Clean Architecture** paradigm, separating concerns into distinct layers:

### 🧱 Layer Overview

| Layer          | Namespace/Project                  | Description                                                                  |
| -------------- | ---------------------------------- | ---------------------------------------------------------------------------- |
| Domain         | `InvestigationGame.Domain`         | Core entities (Sensors, Agents), business logic, and rules. No dependencies. |
| Application    | `InvestigationGame.Application`    | The game engine (`InvestigationEngine`), factories, use-case logic.          |
| Infrastructure | `InvestigationGame.Infrastructure` | Repositories (EF Core / InMemory), database models.                          |
| Presentation   | `InvestigationGame.Presentation`   | CLI game logic, stats reporting, and entry point.                            |

---

## 🧩 Design Patterns & Principles

* **Dependency Injection**: Used to switch between MySQL (via EF Core) and in-memory storage.
* **Factory Pattern**: `SensorFactory` and `AgentFactory` generate domain objects.
* **Repository Pattern**: `IPlayerRepository` interface abstracts persistence.
* **SOLID Principles**: Each class has a single responsibility and is extendable.

---

## 🎮 Game Flow

1. Player selects a sensor each turn.
2. The sensor is attached and activated.
3. The engine checks how many sensors match the agent’s hidden weaknesses.
4. When all required sensor types are attached, the agent is exposed.
5. Player advances through 4 difficulty levels:

   * Foot Soldier
   * Squad Leader
   * Senior Commander
   * Organization Leader
6. History of games is saved (in memory or MySQL) including:

   * Timestamp
   * Agent rank
   * Turns taken
   * Success or failure

---

## 🔧 Features in This Version

* Console-based UI (interactive sensor selection, live feedback).
* 7 Sensor types with unique effects (limited-use, info reveal, counterattack resistance).
* 4 Iranian agent types with increasing difficulty and counterattacks.
* In-memory or MySQL history persistence via DI.

---

## 📦 NuGet Packages

* `MySql.Data`

---

## 🚀 Run Instructions

```bash
# Clone repository
$ git clone https://github.com/your-username/InvestigationGame.git
$ cd InvestigationGame

# Optional: set up DB connection
$ export INVESTIGATION_DB="server=localhost;uid=root;pwd=pass;database=investigation"

# Play game (CLI)
$ dotnet run --project src/InvestigationGame.Presentation

# View stats
$ dotnet run --project src/InvestigationGame.Presentation -- stats
```

---

## 📌 Roadmap

* Replace manual SQL with EF Migrations (✓)
* Add GUI (WPF / Blazor) front-ends ❌ (in next version)
* Add SignalR multiplayer support ❌

---
