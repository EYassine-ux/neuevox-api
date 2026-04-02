# NeueVox API

A RESTful API backend for **NeueVox**, a college portal inspired by Omnivox, built as a class project at Collège Bois-de-Boulogne. Manages students, professors, courses, classes, evaluations, and documents.

> 🚧 **Work in progress** — Core API is stable. Authorization enforcement and frontend integration are actively in development.

---

## 🌐 Live API

| | |
|---|---|
| **Base URL** | `https://api.neuevox.net` |
| **Interactive Docs** | `https://api.neuevox.net/scalar` |

All GET endpoints are publicly accessible — no auth required. Browse and test them directly in Scalar.

---

## Tech Stack

- **Runtime** — ASP.NET Core 9, C#
- **Database** — Entity Framework Core + PostgreSQL
- **Auth** — JWT (access token + refresh token rotation)
- **Docs** — Scalar
- **Deployment** — Docker, hosted on Render (auto-migration on startup)

---

## Features

- JWT authentication with role-based access control (Admin / Student / Professor)
- Refresh token rotation with expiry
- Password hashing via ASP.NET Identity `PasswordHasher`
- Repository + Service pattern (full separation of concerns)
- DTO pattern for all requests and responses
- Enum serialization as strings in JSON
- Auto database migration on Docker startup

---

## API Overview

> 🟢 **Public** — accessible without auth &nbsp;&nbsp; 🔒 **Auth required** &nbsp;&nbsp; 👑 **Admin only** &nbsp;&nbsp;

| Resource | GET | POST | PUT | DELETE |
|----------|-----|------|-----|--------|
| Auth | — | 🟢 `/login` | — | — |
| User | 🟢 | 👑 | 👑 | 👑 |
| Student | 🟢 | 👑 | 👑 | 👑 |
| Professor | 🟢 | 👑 | 👑 | 👑 |
| Course | 🟢 | 👑 | 👑 | 👑 |
| Class | 🟢 | 👑 | 👑 | 👑 |
| Evaluation | 🟢 | 👑 | 👑 |👑 |
| Document | 🟢 | 👑 | 👑 | 👑 |

---

## Project Structure

```
NeueVox/
├── Controllers/       # API endpoints
├── Service/           # Business logic
├── Repository/        # Data access layer (EF Core)
├── Model/
│   ├── NeuevoxModel/  # Entities + DbContext
│   └── DTOs/          # Request & response DTOs
```

---


## Team

Backend developed by **Yassine El Kssir**  
Full project in collaboration with teammates — frontend in active development.

---

## License

MIT
