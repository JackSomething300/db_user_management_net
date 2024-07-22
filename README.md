# User Management System

## Overview
This User Management System is a .NET project designed to manage users and their group affiliations through a web interface. The system allows for creating, reading, updating, and deleting (CRUD) user data. Additionally, during user creation or editing, users can be assigned to specific groups. This project consists of two main components: an API that handles data operations and a web application that interacts with the API to perform user management tasks.

## System Requirements
.NET 8: Ensure you have the .NET 8 SDK installed on your machine. You can download it from Microsoft's official .NET download page.
Microsoft SQL Server: This project uses Microsoft SQL Server. You must have access to a SQL Server instance. 
**The project is configured to connect to (localdb)\MSSQLLocalDB.**

## Getting Started
### Setup Instructions
1. Clone the Repository: Start by cloning this repository to your local machine using Git.
 **Please clean and rebuild the application to restore nuget packages and build the program. Thanks**
bash
```
git clone https://github.com/JackSomething300/db_user_management_net.git
```
2. Open the Solution: Open the solution file in Visual Studio. The solution contains 5 projects:

- UserManagement.API: The backend API project.
- UserManagement.Presentation: The frontend web application that consumes the API.
- UserManagement.Test Testing project 
- UserManagement_Application Contains all the DTO and web facing services
- UserManagement_Core Core layer, Contains all the actual DB entities so that are not public facing

3. Configure the Database Connection: The API is configured to use (localdb)\MSSQLLocalDB. If your SQL Server instance has a different configuration, update the connection string in the appsettings.json file in the API project.

4. Run the API Project: Open the API project in Visual Studio and run it. The API will automatically set up the database, apply migrations, and seed initial data upon startup.

5. Run the Web Project: In a separate instance of Visual Studio, open and run the Web project. Make sure that the API project is running, as the web application will make calls to the API.

## Using the Application
- Home Page: Navigate to the home page of the web application. Here, you will see a list of users and options to add, edit, or delete users.
- Add a User: Click on the "Add User" button and fill in the user details. You can also select which groups the user should belong to.
- Edit a User: Each user listing provides an "Edit" option that allows you to modify user details and their group memberships.
- Delete a User: Users can be deleted using the "Delete" option associated with each user listing.

## Project Architecture
- API: The API handles all data management tasks, including database operations and business logic. It serves as the backend for the web application.
- Web Application: The web application provides a user-friendly interface to interact with the API. It sends requests to the API to perform CRUD operations.
- Clean Code Class Libraries: Ensure clean code style of project layout with core project at the center. As well as application layer that contains application public facing business rules

## Key Features
- User Management: Create, update, view, and delete users.
- Group Management: Assign users to groups during creation or editing.
- Real API Calls: The web application makes actual web requests to the API, simulating a real-world application scenario.

## Development Tools
- Entity Framework Core: Used for ORM and managing database transactions in the API.
- ASP.NET MVC for Web App: Utilized for rendering pages and handling user interactions in the web application.

## Additional Notes
Ensure both the API and the Web application are running simultaneously on different ports to allow the web application to communicate with the API.
