# Tourist Application - Backend (psw-be)

This repository contains the backend service for a comprehensive tourist application, developed as a project for the Software Design course. The system is built using **.NET** and follows **Clean Architecture** and **Domain-Driven Design (DDD)** principles to create a robust, scalable, and maintainable solution.

## Key Features

The backend provides a full suite of features for tourists, guides, and administrators:

* **User Management & Authentication:**
    * Secure registration for Tourists and role-based access for Guides and Administrators.
    * JWT-based authentication and authorization to protect all system endpoints.
    * User profile management, including personal information and interests.

* **Tour Management (for Guides):**
    * Full CRUD (Create, Read, Update, Delete) functionality for tours.
    * Tour lifecycle management with states: `Draft`, `Published`, `Archived`, and `Canceled`.
    * Creation of interactive tour **Keypoints** with geographical coordinates (latitude/longitude), descriptions, and images.
    * Automatic calculation of tour distance based on keypoints.

* **Tour Interaction (for Tourists):**
    * Advanced tour filtering and browsing.
    * A complete **shopping cart** and purchasing system, including the use of **bonus points**.
    * System for rating and reviewing tours after their completion date.

* **Advanced Features & Design Patterns:**
    * **Problem Reporting System:** A feature allowing tourists to report issues with tours. This system is implemented using **Event Sourcing** to track the complete history of a problem's state changes (`Pending`, `Resolved`, `UnderReview`, `Dismissed`).
    * **Automated Email Notifications:** Scheduled jobs for sending email reminders for upcoming tours and monthly sales reports to guides.
    * **Recommendation System:** A service that recommends new tours to tourists based on their registered interests.
    * **Reputation System:** A mechanism for identifying and flagging potentially malicious users (both tourists and guides) based on their activity (e.g., repeated invalid problem reports, frequent tour cancellations).

* **Testing:**
    * The application is developed following the **Test-Driven Development (TDD)** principle, with a comprehensive suite of unit and integration tests to ensure code quality and correctness.

## Technology Stack

* **Framework:** .NET Core / .NET 8
* **Architecture:** Clean Architecture, Domain-Driven Design (DDD), Event Sourcing
* **Database:** PostgreSQL / MySQL with Entity Framework Core
* **Authentication:** ASP.NET Core Identity with JWT
* **API:** RESTful API documented with Swagger (OpenAPI)
* **Testing:** xUnit / NUnit

## Setup and Installation

1.  **Prerequisites:**
    * .NET SDK (Version 8.0+)
    * A running instance of PostgreSQL or MySQL.
    * An SMTP server for email notifications.

2.  **Configuration:**
    * Update the `appsettings.json` file with your database connection string, JWT secret key, and email server configuration.

3.  **Database Migration:**
    * Run the following command to apply database migrations:
        ```bash
        dotnet ef database update
        ```

4.  **Running the Application:**
    * Run the application from your IDE or using the command line:
        ```bash
        dotnet run
        ```

5.  **API Documentation:**
    * Once the application is running, the Swagger UI for API documentation and testing is available at `/swagger`.
