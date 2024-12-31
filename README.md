# Order Management System Web API

A simple Order Management System built using .NET Core Web API, following Clean Architecture principles. This system supports user roles for **Admin**, **Manager**, and **Employee**, each with specific permissions and responsibilities.

---

## Features

### Admin
- A company can sign up and create an **Admin** account by providing company details.
- **Admin** functionalities:
  - Create, manage, and assign roles for **Employee** and **Manager** accounts.
  - Perform all actions available to **Employees** and **Managers**.

### Manager and Employees
- Managers can:
  - Manage inventory/products:
    - Create, view, update, delete items.
  - Manage orders:
    - Create, view, update, and cancel orders.
  - Perform all actions available to **Employees**.

---

## Technical Stack

- **Framework**: .NET Core Web API
- **Database**: Microsoft SQL Server (MSSQL)  
- **ORM**: Entity Framework Core  
- **Authentication**: JSON Web Tokens (JWT), Identity and Role based Auth.  

---

## Architecture

This project adheres to the **Clean Architecture** design pattern for a modular, testable, and maintainable codebase. The key components are: Presentation, Infrastructure, Application and Domain.

---

## Prerequisites

- .NET SDK installed (version 9.0).
- Microsoft SQL Server installed and running.
- Postman or any API testing tool for testing the endpoints.

---

## Setup and Installation

1. **Clone the Repository**  
   ```bash
   git clone https://github.com/rijoanfardous101/OrderMSBackend.git

2. **Create a appsettings.json file**
```json
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*",
    "AllowedOrigins": [ "" ],
    "ConnectionStrings": {
        "OrderMSConnectionString": "Server=;Database=;Trusted_Connection=True;TrustServerCertificate=True;"
    },
    "Jwt": {
        "Key": "",
        "Issuer": "",
        "Audience": ""
    }
}
```
