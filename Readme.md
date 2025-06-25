# 🧠 Team Task Management API

A secure RESTful API for team-based task collaboration, built with **C#/.NET 8**, **Entity Framework Core**, and **SQL Server**. It allows users to register, join teams, and manage tasks within their teams — all securely authenticated using JWT.

---

## ⚙️ Tech Stack

- **Framework**: .NET 8 (ASP.NET Core Web API)
- **Database**: SQL Server with Entity Framework Core (Code-First)
- **Authentication**: JWT Bearer Tokens
- **Password Hashing**: BCrypt.Net-Next

### 🔌 NuGet Dependencies

- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `Microsoft.EntityFrameworkCore.Tools`
- `BCrypt.Net-Next`

---

## 🗂️ EF Core Migrations (Code-First)

### ⚙️ Running the API

1. **Clone the repository**  
   ```bash
   git clone https://github.com/Simeon1809/TaskManagementAPI.git

Use the following steps to configure and create your database schema using **EF Core 8**.

### 1. Configure Your Connection

Open the `appsettings.json` file and update the connection string to match your SQL Server:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=TeamTaskDb;Trusted_Connection=True;TrustServerCertificate=True;"
}


 go to Package manager console and run the following query 

    1. dotnet ef migrations add InitialCreate
 
    2. dotnet ef database update




 API Assumptions


  Authentication & Authorization
All endpoints require JWT authentication via Authorization: Bearer <token>.

JWT tokens include a NameIdentifier claim used as the user’s unique identifier.

[Authorize] is applied to secure routes, and user authorization is verified in services where necessary.

👤 Users
Users register with a valid, unique email and password.

Passwords are stored securely using salted hashing (e.g., HMACSHA512 or BCrypt).

The system returns a token and basic profile data upon successful login or registration.

Password hashes and other sensitive fields are never returned to clients.

👥 Teams
Users can create and belong to multiple teams.

The user who creates a team becomes its Admin.

Admins can invite other users and assign them roles (Admin, Member).

Team names must be unique per user.

Teams cannot be deleted (in current implementation).

📋 Tasks
Tasks belong to teams and must be assigned to a user who is a member of that team.

Only team members can view tasks for that team.

Only Admins or task creators can update or delete tasks.

Task statuses include: Pending, InProgress, and Completed.

Each task must include a title, due date, assignee, and team.

⚙️ Infrastructure & Behavior
SQL Server is used with EF Core's Code-First approach.

All entities use Guid as their primary keys.


A global exception-handling middleware ensures uniform error responses in JSON format.

Swagger UI is available at /swagger for testing and documentation.

Unit tests are written using xUnit and Moq for service-level testing.

