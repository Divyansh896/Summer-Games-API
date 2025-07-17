# 🏅 Summer Games RESTful WebAPI

A complete backend system for managing athletes, sports, and contingents in a Summer Games event. This RESTful API supports full CRUD operations and is consumed by a frontend built with XAML and C#. The backend is designed using ASP.NET Core WebAPI, Entity Framework Core, and a lightweight SQLite database.

> 👤 **Individual Project**  
> 🗓️ **Duration**: January 2025 – February 2025  
> 🌐 **Frontend Repository**: [Summer-Games-WebAPI-Client](https://github.com/Divyansh896/Summer-games-WebAPI-Client.git)

---

## 📌 Table of Contents

- [Tech Stack](#tech-stack)
- [Features](#features)
- [Project Entities](#project-entities)
- [API Endpoints](#api-endpoints)
- [Running the API Locally](#running-the-api-locally)
- [Swagger UI](#swagger-ui)
- [Deployment](#deployment)
- [Known Issues](#known-issues)
- [Author](#author)

---

## 🔧 Tech Stack

- **ASP.NET Core WebAPI** (C#)
- **Entity Framework Core (EF Core)**
- **SQLite** database
- **Swagger / Swashbuckle** for API documentation
- **Newtonsoft.Json** for JSON serialization
- **CORS** for frontend integration
- **Deployed on Azure App Services**

---

## ✅ Features

- Full CRUD operations for:
  - **Athletes**
  - **Sports**
  - **Contingents**
- Assign athletes to a specific sport and contingent
- Relationship management across entities
- Built-in Swagger UI for testing
- Error handling using `ApiException`
- Integrated with a XAML + C# frontend client
- Import/export support (planned or implemented based on frontend)
- Hosted API (may not be active, fallback to local)

---

## 🧍 Project Entities

### 🏃 Athlete

| Property     | Type    | Description                     |
|--------------|---------|---------------------------------|
| Id           | int     | Unique athlete ID               |
| Name         | string  | Athlete name                    |
| Age          | int     | Age of athlete                  |
| SportId      | int     | Foreign key to Sport            |
| ContingentId | int     | Foreign key to Contingent       |

### 🏆 Sport

| Property | Type   | Description          |
|----------|--------|----------------------|
| Id       | int    | Unique sport ID      |
| Name     | string | Name of the sport    |

### 🌍 Contingent

| Property | Type   | Description             |
|----------|--------|-------------------------|
| Id       | int    | Unique contingent ID    |
| Name     | string | Name of the contingent  |

---

## 📡 API Endpoints

### 🏃 Athlete Endpoints

| Method | Endpoint               | Description               |
|--------|------------------------|---------------------------|
| GET    | `/api/athletes`        | Get all athletes          |
| GET    | `/api/athletes/{id}`   | Get athlete by ID         |
| POST   | `/api/athletes`        | Create a new athlete      |
| PUT    | `/api/athletes/{id}`   | Update an athlete         |
| DELETE | `/api/athletes/{id}`   | Delete an athlete         |

---

### 🏆 Sport Endpoints

| Method | Endpoint            | Description            |
|--------|---------------------|------------------------|
| GET    | `/api/sports`       | Get all sports         |
| GET    | `/api/sports/{id}`  | Get sport by ID        |
| POST   | `/api/sports`       | Add new sport          |
| PUT    | `/api/sports/{id}`  | Update a sport         |
| DELETE | `/api/sports/{id}`  | Delete a sport         |

---

### 🌍 Contingent Endpoints

| Method | Endpoint                | Description                |
|--------|-------------------------|----------------------------|
| GET    | `/api/contingents`      | Get all contingents        |
| GET    | `/api/contingents/{id}` | Get contingent by ID       |
| POST   | `/api/contingents`      | Add new contingent         |
| PUT    | `/api/contingents/{id}` | Update a contingent        |
| DELETE | `/api/contingents/{id}` | Delete a contingent        |

---

## ⚙️ Running the API Locally

### 🖥️ Prerequisites

- [.NET 7 SDK or newer](https://dotnet.microsoft.com/download)
- Visual Studio or Visual Studio Code
- SQLite installed (optional for inspecting DB)

### 📦 Steps

1. Clone the repository:
   ```bash
   git clone https://github.com/Divyansh896/Summer-Games-API.git
   cd Summer-Games-API
