# Task Management API

This is a RESTful API built using C# (ASP.NET Core) to manage projects and tasks. It integrates with AWS Cognito for user authentication, allowing you to create users, reset passwords, and obtain access tokens. Once authenticated, you can perform CRUD (Create, Read, Update, Delete) operations on projects and tasks.

## Features

**Authentication Routes:**

* **Create User:** Registers a new user in the AWS Cognito User Pool.
    * `POST /api/Auth/signUp`
* **Reset Password:** Allows the user to reset their password to a permanent one.
    * `POST /api/Auth/changeTempPassword`
* **Get Access Token:** Retrieves an access token for an authenticated user using their username and password.
    * `POST /api/auth/getAccessToken`

**Project CRUD Operations:**

* **Create Project:** Creates a new project.
    * `POST /api/Project`
* **Get All Projects:** Fetches all projects with pagination (page number and page size).
    * `GET /api/Project`
* **Get Project by ID:** Retrieves a specific project by its unique ID.
    * `GET /api/Project/{id}`
* **Update Project:** Updates a project by its ID.
    * `PUT /api/Project`
* **Delete Project:** Deletes a project by its ID.
    * `DELETE /api/Project/{id}`

**Task CRUD Operations:**

* **Create Task:** Creates a new task associated with a project (by project ID).
    * `POST /api/Task`
* **Get All Tasks:** Fetches all tasks with pagination.
    * `GET /api/Task`
* **Get All Tasks by Project ID:** Fetches all tasks associated with a specific project ID with pagination.
    * `GET /api/Task/byProjectId`
* **Get Task by ID:** Retrieves a specific task by its ID.
    * `GET /api/Task/{id}`
* **Update Task:** Updates a task by its ID.
    * `PUT /api/Task/{id}`
* **Delete Task:** Deletes a task by its ID.
    * `DELETE /api/Task/{id}`

## Prerequisites

Before using the API, ensure you have the following installed:

* **.NET SDK:** .NET 8.0 or later.
* **HTTP Client:** Postman, Insomnia, or any other tool to test REST APIs. (Swagger UI is included and recommended.)

## Setup and Installation

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/0mriki/TaskManagement.git](https://github.com/0mriki/TaskManagement.git)
    ```
2.  **Navigate to the project directory:**
    ```bash
    cd TaskManagement
    ```
3.  **Configuration:**
   * Open appsettings.json in the project directory
   *  Replace the marked parameters with the ones provided in mail
      ![image](https://github.com/user-attachments/assets/a2d94ded-a684-4f19-9c9a-68e0c4271471)
4.  **Running the Application:**

    * **Using Command Line (CMD):**
        1.  Open CMD in the directory containing the `.csproj` file.
        2.  Restore NuGet packages: `dotnet restore`
        3.  Run the application: `dotnet run`
        4.  Open Swagger UI in your browser: `http://localhost:5064/swagger/index.html`

    * **Using Visual Studio:**
        1.  Open the `.sln` file in Visual Studio 2022.
        2.  Click the "Run" button.
        3.  Open Swagger UI in your browser: `https://localhost:7265/swagger/index.html`

## Authentication Routes

These routes allow you to create and manage user accounts. If you prefer, you can use the default user:

* **Username:** `moveouser`
* **Password:** `Aa123456!`

Use the "Get Access Token" route to obtain an authentication token.

**1. Create User**

* `POST /api/Auth/signUp`
* **Request Body:**

    ```json
    {
      "username": "user@example.com",
      "email": "user@example.com",
      "fullName": "your full name",
      "temporaryPassword": "yourtmppassword123"
    }
    ```

**2. Reset Password**

* `POST /api/Auth/changeTempPassword`
* **Request Body:**

    ```json
    {
      "username": "user@example.com",
      "newPassword": "yournewpassword123"
    }
    ```

**3. Get Access Token**

* `POST /api/auth/getAccessToken`
* **Request Body:**

    ```json
    {
      "username": "user@example.com",
      "password": "yourpassword123"
    }
    ```
* **Response:**

    ```json
    {
      "accessToken": "your-jwt-access-token"
    }
    ```

## Project Routes

**1. Create Project**

* `POST /api/Project`
* **Request Body:**

    ```json
    {
      "name": "New Project",
      "description": "Description of the new project"
    }
    ```
* **Response:**

    ```json
    {
      "projectId": "project-id"
    }
    ```

**2. Get All Projects**

* `GET /api/Project`
* **Query Parameters:**
    * `pageSize`: Number of projects per page.
    * `pageNumber`: Page number.
* **Response:**

    ```json
    {
      "totalRecords": 100,
      "totalPages": 4,
      "currentPage": 1,
      "pageSize": 25,
      "records": [
        {
          "id": "project-id",
          "name": "New Project",
          "description": "Description of the new project",
          "tasks": []
        }
      ]
    }
    ```

**3. Get Project by ID**

* `GET /api/Project/{id}`
* **Response:**

    ```json
    {
      "id": "project-id",
      "name": "New Project",
      "description": "Description of the new project",
      "tasks": []
    }
    ```

**4. Update Project**

* `PUT /api/Project/{id}`
* **Request Body:**

    ```json
    {
      "id": "project-id",
      "name": "Updated Project Name",
      "description": "Updated project description"
    }
    ```
* **Response:** `StatusCode 200`

**5. Delete Project**

* `DELETE /api/Project/{id}`
* **Response:** `StatusCode 200`

## Task Routes

**1. Create Task**

* `POST /api/Task`
* **Request Body:**

    ```json
    {
      "projectId": "project-id",
      "title": "Task Title",
      "description": "Task Description",
      "status": "InProgress"
    }
    ```
* **Response:**

    ```json
    {
      "taskId": "task-id"
    }
    ```

**2. Get All Tasks**

* `GET /api/Task`
* **Query Parameters:**
    * `pageSize`: Number of tasks per page.
    * `pageNumber`: Page number.
* **Response:**

    ```json
    {
      "totalRecords": 100,
      "totalPages": 4,
      "currentPage": 1,
      "pageSize": 25,
      "records": [
        {
          "id": "task-id",
          "projectId": "project-id",
          "title": "Task Title",
          "description": "Task Description",
          "status": "InProgress"
        }
      ]
    }
    ```

**3. Get All Tasks by Project ID**

* `GET /api/Task/byProjectId`
* **Query Parameters:**
    * `pageSize`: Number of tasks per page.
    * `pageNumber`: Page number.
    * `projectId`: The ID of the project to retrieve tasks from.
* **Response:** (Same format as "Get All Tasks")

**4. Get Task by ID**

* `GET /api/Task/{id}`
* **Response:**

    ```json
    {
      "id": "task-id",
      "projectId": "project-id",
      "title": "Task Title",
      "description": "Task Description",
      "status": "InProgress"
    }
    ```

**5. Update Task**

* `PUT /api/Task/{id}`
* **Request Body:**

    ```json
    {
      "title": "Updated Task Title",
      "description": "Updated task description",
      "status": "Done"
    }
    ```
* **Response:** `StatusCode 200`

**6. Delete Task**

* `DELETE /api/Task/{id}`
* **Response:** `StatusCode 200`

## Authentication

All CRUD routes for projects and tasks require authentication. Include the access token in the `Authorization` header as a Bearer token.

Example: `Authorization: Bearer <your-access-token>`

## HTTP Status Codes

* **200 OK:** Request successful.
* **401 Unauthorized:** Invalid access token.
* **404 Not Found:** Resource not found.
* **500 Internal Server Error:** Server error.

## Deployment Suggestion

### Optimized Deployment Approach

To handle high traffic, especially with 10k daily users, I recommend deploying the project in Kubernetes containers to ensure scalability and reliability. 

- **Microservices Architecture**: I would separate the `TaskService` and `ProjectService` into two distinct microservices, each deployed across multiple containers using a master-slave architecture. This approach helps with easier maintenance and ensures continuous availability.
  
- **Message Queue Integration**: To manage load and enable smooth communication between services, I would integrate a message queuing system like **RabbitMQ** or **Kafka**.

- **Data Integrity**: To maintain consistency and prevent overwriting of data during concurrent access, I would implement additional locking mechanisms in the application logic.

- **Caching Mechanism**: User session data and cookies would be cached using **Redis** to reduce database load and enhance performance.

- **Database**: I would migrate from **SQLite** to a more robust and scalable relational database such as **PostgreSQL** or **MySQL**, hosted in a cloud environment for high availability and better performance.

### Deployment with Current Codebase

Given the existing monolithic code structure:

- I would still leverage **Kubernetes** to deploy multiple instances of the API for load distribution and fault tolerance.
- Use **NGINX** as a load balancer to efficiently route incoming traffic to the available API instances.
- To prevent inconsistent states and ensure data integrity under high concurrency, I would implement locking mechanisms at the database level for critical operations.
