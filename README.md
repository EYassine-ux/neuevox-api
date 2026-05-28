# NeueVox API
A RESTful API backend for **NeueVox**, a college portal inspired by Omnivox, built as a class project at Collège Bois-de-Boulogne. Manages students, professors, courses, classes, evaluations, grades, documents, and more.

> 🚧 **Work in progress** — Core API is stable. Authorization enforcement and frontend integration are actively in development.

---

## 🚀 Getting Started

```bash
# Start the API (builds image + runs in background)
docker compose up -d --build

# Stop and remove volumes
docker compose down -v
```

Once running:
- **Interactive Docs** → `http://localhost:8080/scalar`
- **Base URL** → `http://localhost:8080/api`

Database migrations run automatically on startup.

---

## Tech Stack
- **Runtime** — ASP.NET Core 10, C#
- **Database** — Entity Framework Core + PostgreSQL
- **Auth** — JWT (access token + refresh token rotation)
- **Docs** — Scalar
- **Deployment** — Docker

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

> All endpoints are currently public — no authentication required.

| Resource | GET | POST | PUT | DELETE |
|----------|-----|------|-----|--------|
| Auth | — | 🟢 `/login`, `/register`, `/refresh-token` | — | — |
| User | 🟢 | 🟢 | 🟢 | 🟢 |
| Student | 🟢 | 🟢 | 🟢 | 🟢 |
| Professor | 🟢 | 🟢 | 🟢 | 🟢 |
| Course | 🟢 | 🟢 | 🟢 | 🟢 |
| Class | 🟢 | 🟢 | 🟢 | 🟢 |
| Evaluation | 🟢 | 🟢 | 🟢 | 🟢 |
| Grade | 🟢 | 🟢 | 🟢 | 🟢 |
| Document | 🟢 | 🟢 | 🟢 | 🟢 |
| Schedule | 🟢 | 🟢 | 🟢 | 🟢 |
| Program | 🟢 | 🟢 | 🟢 | 🟢 |
| StudentClass | 🟢 | 🟢 | 🟢 | 🟢 |
| StudentSubmission | 🟢 | 🟢 | 🟢 | 🟢 |
| Announcement | 🟢 | 🟢 | 🟢 | 🟢 |
| Quote | 🟢 | 🟢 | 🟢 | 🟢 |

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

## 🔑 Test Accounts

Three seeded accounts are available to test authenticated flows:

| Role | Email | Password |
|------|-------|----------|
| Admin | `admin@neuevox.net` | `admin123` |
| Student | `student@neuevox.net` | `student123` |
| Professor | `professor@neuevox.net` | `professor123` |

---

## 🗄️ Database — pgAdmin

A pgAdmin interface is included in the Docker setup to browse the seeded data visually.

1. Visit `http://localhost:5050` in your browser.
2. Log in with:
   - **Email** → `admin@neuevox.net`
   - **Password** → `admin`
3. In the left panel, open **Servers** and click the application server.
4. Enter the database password: `TeamProject123`
5. Navigate to: `Databases > NeueVoxDb > Schemas > public > Tables`
6. Right-click any table (e.g. `Users`) and select **View/Edit Data → All Rows**, or use the **Query Tool** to run custom SQL.
---
### Notable nested routes
- `GET /api/student/{id}/evaluations` — all evaluations for a student
- `GET /api/student/{id}/classes` — student's enrolled classes
- `GET /api/student/{id}/schedules` — student's weekly schedule
- `GET /api/student/{id}/performance` — academic progress & weekly hours
- `GET /api/student/{id}/classes/{classId}/grades` — grades per class
- `GET /api/student/{id}/classes/{classId}/documents` — documents per class
- `GET /api/professor/{id}/schedules` — professor's schedule
- `GET /api/professor/depart/search?depart=` — search by department
- `GET /api/course/{id}/classes` — classes under a course
- `GET /api/class/{id}/documents` — documents for a class
- `GET /api/quote/daily` — daily quote
---
## ⚠️ MongoDB Note

Some features rely on MongoDB (Announcements, Quotes). The hosted instance is no longer running, so those endpoints will not work out of the box.

To enable them, update the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "MongoDb": "your-mongodb-connection-string-here"
}
```

You can use a free cluster from [MongoDB Atlas](https://www.mongodb.com/atlas) or point it to a local instance.

## Team

Designed and developed by **Yassine El Kssir**.

