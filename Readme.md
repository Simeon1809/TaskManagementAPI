# Team Task Management API

A secure RESTful API for team-based task management built with C#/.NET Core, SQL Server, and EF Core.

## Tech Stack
- **Framework**: .NET 8
- **Database**: SQL Server with EF Core (code-first migrations)
- **Authentication**: JWT
- **Dependencies**: 
  - Microsoft.EntityFrameworkCore.SqlServer
  - Microsoft.AspNetCore.Authentication.JwtBearer
  - BCrypt.Net-Next (for password hashing)

## EF Core Migrations (Code-First)

To apply or update database schema using EF Core 8:

### 1. To Create the Database and corresponding tables in you MSSQL, The following should be done

1. Go to the Appsettings.json file and change the Server name to your server


2. go to Package manager console and run the following query 

    1. dotnet ef migrations add InitialCreate
 
    2. dotnet ef database update

