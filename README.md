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



## Output


<img width="1437" height="912" alt="Screenshot 2025-07-18 001251" src="https://github.com/user-attachments/assets/b52e5b9a-70d0-4c0a-9508-39e02d933f06" />
<img width="986" height="693" alt="Screenshot 2025-07-18 001314" src="https://github.com/user-attachments/assets/1df0fb53-3852-4daf-aa89-699a0044738e" />
<img width="1589" height="751" alt="Screenshot 2025-07-18 001332" src="https://github.com/user-attachments/assets/b7c3d7c6-691a-4af4-833d-086ef1efc6e6" />
<img width="985" height="802" alt="Screenshot 2025-07-18 001345" src="https://github.com/user-attachments/assets/a2162e82-0e5e-4a2b-9e6c-9498645823fb" />
<img width="950" height="841" alt="Screenshot 2025-07-18 001405" src="https://github.com/user-attachments/assets/e1659d60-2746-4f2c-a6e3-014c38a97909" />



