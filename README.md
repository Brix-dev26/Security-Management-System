# Security Management System

## Overall

The **Security Management System** is a robust web-based platform designed to streamline and secure the management of campus security operations. It offers a centralized interface for administrators and security staff to handle gate monitoring, emergency event tracking, and log management. The system emphasizes secure authentication, role-based access control, and scalable architecture.

## Master Branch (Blazor WebAssembly App)

1. Built using **Blazor WebAssembly** (.NET).
2. Login system for both Security Staff and Admins with JWT-based authentication.
3. Role-based access control (e.g., only "Computer Security Officer" can trigger emergency events).
4. Dynamic dropdowns for selecting campus and gate to filter and manage security logs.
5. Responsive UI for managing campuses, gates, logs, and security personnel.

## Backend API Branch (ASP.NET Core Web API)

1. Developed using **ASP.NET Core Web API**.
2. Structured using layered architecture: Controller → Service → Repository.
3. Entity Framework Core for database access and migrations.
4. Supports all core operations: CRUD for staff, campus, gates, and logs.
5. JWT token generation and validation using Microsoft Identity libraries.

## Technologies Used

### Programming Languages:
1. **C#** (.NET for frontend and backend)
2. **HTML/CSS** (Blazor components)

### Frameworks & Libraries:
1. **Blazor WebAssembly** – For the client-side application.
2. **ASP.NET Core Web API** – Backend RESTful services.
3. **Entity Framework Core** – ORM for database interaction.
4. **Blazored.LocalStorage** – For JWT token persistence on client side.
5. **Microsoft.IdentityModel.Tokens** – For token security.
6. **Bootstrap** – Frontend styling and layout.

### Tools:
1. **Visual Studio 2022** – Main development environment.
2. **Postman** – For testing API endpoints.
3. **SQL Server Management Studio (SSMS)** – For managing the database.
4. **Draw.io / Microsoft Visio** – For creating UML diagrams and ERDs.
5. **Git & GitHub** – Version control and collaboration.


## Features

- ✅ JWT-secured login for Admins and Security Staff  
- ✅ Role-based authorization to protect sensitive features  
- ✅ Emergency event creation limited to authorized roles  
- ✅ Campus and gate assignment for security personnel  
- ✅ Visitor and vehicle logging  
- ✅ Real-time filtering of log entries by date, gate, and campus  
- ✅ Clean separation of concerns using services and repositories

## Installation

### Prerequisites

Before running this project, ensure that you have the following installed:

- **.NET 6.0 SDK** (or later)
- **SQL Server** (or use a local SQL Server instance)

### Setup

1. **Clone the repository**:
   - Run the following command to clone the repository:
     ```bash
     git clone https://github.com/your-username/security-system-management.git
     cd security-system-management
     ```

2. **Restore the NuGet packages**:
   - Run the following command to restore the NuGet packages:
     ```bash
     dotnet restore
     ```

3. **Set up the database**:
   - Modify `appsettings.json` to match your SQL Server credentials.
   - Ensure your connection string is correctly set in the `appsettings.json` file.

4. **Apply migrations to your database**:
   - Run the following command to apply migrations:
     ```bash
     dotnet ef database update
     ```

5. **Build and run the application**:
   - Run the following command to build and start the application:
     ```bash
     dotnet run

## Documentation

📄 **All system diagrams, workflows, and development documentation** are available within the project folder:

- The **ERD** (Entity Relationship Diagram) can be found in the `docs` folder within the project directory.

## Contributing

We appreciate community contributions! Here's how you can help:

1. Fork the repository.
2. Create a new feature branch: `git checkout -b feature/my-new-feature`.
3. Commit your changes: `git commit -am 'Add some feature'`.
4. Push to the branch: `git push origin feature/my-new-feature`.
5. Submit a pull request.

## Contact

📬 **Project Contact**  
Mohamed Amr  
[My LinkedIn](https://www.linkedin.com/in/mohamed-fathy-97a916351/)  
moamrfathytawfik@gmail.com
