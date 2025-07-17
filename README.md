# Configurable Workflow Engine (State-Machine API)

## Overview

This project implements a configurable workflow engine backend as a REST API.  
Users can:

- Define workflows as state machines (states and actions)
- Start workflow instances
- Move instances through states by executing actions
- View state and history of instances

---

## Technical Details

- **Language / Stack**:  
  - C# with .NET 8  
  - ASP.NET Core minimal API style with Controllers for clarity  
- **Dependencies**: Minimal, only built-in .NET libraries and Swashbuckle (Swagger) for API docs  
- **Storage**: In-memory collections (no persistence) for simplicity  
- **Time-Box**: Developed within ~2 hours as a minimal viable product  
- **Testing**: Manual testing done via Swagger UI and curl; no unit tests included (left as TODO)  

---

## API Endpoints

| Method | Path                         | Description                          |
|--------|------------------------------|------------------------------------|
| POST   | /workflows                   | Create workflow definition          |
| GET    | /workflows                   | List all workflow definitions       |
| GET    | /workflows/{id}              | Get workflow definition by id       |
| POST   | /workflows/{id}/instances    | Start new workflow instance         |
| GET    | /instances                   | List all workflow instances         |
| GET    | /instances/{instanceId}      | Get workflow instance state/history |
| POST   | /instances/{instanceId}/actions | Execute an action on a workflow instance |

---

## Assumptions and Notes

- Workflow **definitions** must have exactly one enabled initial state.
- Only enabled states and actions can be used or executed.
- No data persistence; all data cleared on application restart.
- No concurrency/thread-safety considerations (single-threaded for demo purposes).
- Minimal error messages and validation to keep the implementation concise.
- Input validation is basic; no complex schema validation.
- Authentication and authorization are **not implemented**.
- Unit tests are not included but recommended for production use.
- Additional polish such as logging, detailed error handling, and UI can be added later.

---

## How to Run

1. Install [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Clone the repository
3. Run:


