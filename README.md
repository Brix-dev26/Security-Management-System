# Security Management System 🛡️

![GitHub release](https://img.shields.io/github/release/Brix-dev26/Security-Management-System.svg) ![License](https://img.shields.io/badge/license-MIT-blue.svg)

Welcome to the **Security Management System** repository! This project is a web-based application designed to enhance campus security operations. It provides a robust platform for managing various security-related tasks effectively.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)
- [Releases](#releases)

## Introduction

In today’s world, ensuring safety on campuses is crucial. Our Security Management System aims to streamline security operations, making it easier for staff to manage and respond to security needs. The system includes role-based login, visitor and vehicle log tracking, and emergency event reporting, among other features.

## Features

- **Role-Based Login**: Users can access the system based on their roles, ensuring that sensitive information is only available to authorized personnel.
  
- **Visitor and Vehicle Log Tracking**: Keep track of all visitors and vehicles entering the campus. This feature helps maintain a secure environment and provides a historical log for reference.

- **Emergency Event Reporting**: Quickly report emergencies through a dedicated interface, allowing for immediate action and response.

- **CRUD Operations**: Manage campuses, gates, and security staff with create, read, update, and delete operations.

- **User-Friendly Interface**: Built with Blazor WebAssembly, the application provides a smooth and responsive user experience.

- **Secure Authentication**: The system uses JWT for secure authentication, ensuring that user data remains protected.

## Technologies Used

This project leverages several technologies to provide a robust and scalable application:

- **ASP.NET Core**: The backend is built using ASP.NET Core Web API, providing a powerful and flexible server-side framework.

- **Blazor WebAssembly**: The frontend is developed using Blazor, allowing for rich web applications with C#.

- **Entity Framework**: This ORM tool simplifies database interactions and management.

- **JWT Authentication**: JSON Web Tokens are used for secure user authentication.

## Installation

To get started with the Security Management System, follow these steps:

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/Brix-dev26/Security-Management-System.git
   ```

2. **Navigate to the Project Directory**:
   ```bash
   cd Security-Management-System
   ```

3. **Install Dependencies**:
   - For the backend, navigate to the API project folder and run:
     ```bash
     dotnet restore
     ```
   - For the frontend, navigate to the Blazor project folder and run:
     ```bash
     dotnet restore
     ```

4. **Set Up the Database**:
   - Update the connection string in the `appsettings.json` file.
   - Run the migrations to set up the database:
     ```bash
     dotnet ef database update
     ```

5. **Run the Application**:
   - Start the backend server:
     ```bash
     dotnet run
     ```
   - Start the frontend:
     ```bash
     dotnet run
     ```

## Usage

After installation, you can access the application through your web browser. The default URL is `http://localhost:5000`. 

### Login

1. Use your credentials to log in based on your assigned role.
2. Navigate through the dashboard to access various features.

### Tracking Visitors and Vehicles

- Use the visitor log feature to add new entries.
- Access vehicle logs to monitor and manage campus traffic.

### Reporting Emergencies

- Click on the emergency reporting button to fill out the necessary details.
- Submit the report for immediate attention.

## Contributing

We welcome contributions from the community! If you would like to contribute, please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Make your changes and commit them.
4. Push your changes to your forked repository.
5. Create a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact

For questions or support, feel free to reach out:

- **Email**: support@example.com
- **GitHub**: [Brix-dev26](https://github.com/Brix-dev26)

## Releases

You can find the latest releases of the Security Management System [here](https://github.com/Brix-dev26/Security-Management-System/releases). Please download and execute the necessary files to get started with the latest features and updates.

For more information about the releases, check the **Releases** section in the GitHub repository.

---

Thank you for checking out the Security Management System! Your feedback and contributions are highly appreciated.