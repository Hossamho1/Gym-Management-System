# Gymora

A comprehensive Gym Management API built with ASP.NET Core MVC and Entity Framework Core, utilizing a clean N-Tier Architecture.

## Technologies & Patterns Used
* **Framework:** ASP.NET Core MVC / .NET 10
* **Database:** SQL Server & Entity Framework Core
* **Architecture:** N-Tier Architecture (Presentation, BusinessLogic, DataAccess)
* **Design Patterns:** Repository Pattern, Dependency Injection
* **Database Strategies:** Table-Per-Hierarchy (TPH) for User Management, Global Query Filters for Soft Deletion, Interceptors for Auditing.

##  Features
* Full CRUD operations for Gym Plans and Subscriptions.
* Dynamic Seeding for initial database states.
* Centralized Data Configurations using `IEntityTypeConfiguration`.

##  Project Structure
* `GymRout.Presentation`: Controllers and Views.
* `GymRout.BusinessLogic`: Services and Core Logic (Currently implementing).
* `GymRout.DataAccess`: DbContext, Entities, Configurations, and Repositories.
