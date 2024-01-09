# Order Management System
This project is an Order Management System developed using WebAssembly Blazor.
## Project Structure

### Server
The server-side part of the application hosts the backend logic. It includes:
*Dependency Injection & Interfaces:* Utilized for managing services and contracts.
*Repository Pattern:* Implemented for data access abstraction.
*Separated Business Logic Layer (BLL) and Data Access Layer (DAL):* Segregating responsibilities for maintainability and clarity.
*Result Class:* Used for generating success and error responses, enabling robust error handling.
*Mappers:* Utilized for mapping between different layers of the application, enhancing data transformation and abstraction.
*Migrations:* Contains database migrations for maintaining data schema versions.
### Client
The client-side encompasses the frontend aspect of the application. It involves:
*Model and ModelsDTo:* Utilized for defining data structures.
*Dependency Injection:* Facilitating loosely coupled components.
*Shared Folder:* Contains common components or utilities used both on the client and server.
## Project Structure Overview
*Server:* Handles backend logic, dependency injection, repository pattern, mappers, and database migrations.
*Client:* Manages the frontend, models, dependency injection, and shared components.
*Shared:* Contains elements common to both the client and server.
This separation enhances maintainability, scalability, and readability of the codebase by isolating concerns and promoting modularity.
