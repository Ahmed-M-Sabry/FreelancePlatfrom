# FreelancePlatfrom

Freelance platforms enable remote service offerings and have grown with the rise of remote work. They enhance flexibility, communication, and work-life balance. Machine learning is increasingly used to manage projects and match freelancers to jobs, boosting success in the evolving digital marketplace.

# Freelance Platform API 

This is a backend Web API for a **Freelance Platform** built using modern and scalable architecture with **ASP.NET Core 8**.  
It supports users (clients and freelancers) to register, post jobs, apply, manage contracts, and more.

---

## 📦 Architecture

The project follows the **Clean Architecture** pattern with clear separation of concerns:

- `FreelancePlatform.Api` - Entry point and routing.
- `FreelancePlatform.Core` - Domain models and interfaces.
- `FreelancePlatform.Service` - Business logic and use cases.
- `FreelancePlatform.Infrastructure` - External integrations.
- `FreelancePlatform.Data` - EF Core, DB context, and repositories.

---

## 🛠 Technologies Used

- **ASP.NET Core 8 Web API**
- **Entity Framework Core** with **Code First**
- **Clean Architecture**
- **CQRS Pattern** with **MediatR**
- **JWT Authentication** + Identity
- **AutoMapper**
- **Swagger** (OpenAPI) for documentation
- **Repository Pattern**

---

## ✨ Features

### 🔐 Authentication & Accounts
- User registration (Client / Freelancer)
- Login with JWT token
- Reset password & token refresh
- Freelancer profile management (bio, skills, languages, portfolio)

### 💼 Jobs & Tasks
- Create, edit, delete job posts
- Freelancers apply to jobs
- Clients view applicants
- Favorite job posts / freelancers

### 📑 Contracts & Reviews
- Create and manage contracts between client & freelancer
- Submit reviews and ratings
- Add/remove/edit portfolio items

### 🧠 Admin-Like Management
- Manage skills, categories, languages, countries
- Handle user reports

---

## 🔧 How to Run

1. Clone the repo:
   ```bash
   git clone https://github.com/yourusername/FreelancePlatformApi.git
   cd FreelancePlatformApi
