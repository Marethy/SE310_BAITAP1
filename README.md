
Farm Management System

Overview

This project is a Farm Management System designed to manage livestock, including cows, sheep, and goats. The system tracks livestock attributes such as milk production, reproduction, and hunger signals. It uses a three-layer architecture: GUI (Presentation Layer), BLL (Business Logic Layer), and DAL (Data Access Layer), with a database-first approach implemented using Entity Framework Core and SQL Server.


Setup and Installation
_ Clone the Repository
_ Set Up the Database
 + Create a SQL server database named FarmManagement.
 + Update the connection string in the OnConfiguring method of the FarmContext class or in the appsettings.json file:
_ Apply Migrations
 + Open Package Manager Console in Visual Studio.
 + Run the following command to apply the initial migration and create the database schema: Add-Migration InitialCreate, Update-Database

_ Build and run project

