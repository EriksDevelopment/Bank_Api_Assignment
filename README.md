Bank API – Backend Application

Project Description

This project is the backend part of a banking application designed to be used by both a mobile application and a web application.
The solution is implemented as a RESTful Web API using ASP.NET Web API and communicates with a provided database containing example data.

The system includes user authentication and authorization and is designed according to object-oriented principles.

⸻

User Roles

The application supports two types of users:
	•	Customers
	•	Administrators

The system uses role-based authorization to restrict access to functionality.

⸻

Administrator Features

Administrators can access administrative functionality only. Customers cannot access admin functionality, and administrators cannot access customer-only functionality.

An administrator can:
	•	Create new users (customers).
	•	Create login credentials for customers.
	•	Automatically create a bank account when a new customer is registered.
	•	Create bank accounts of different types (e.g. savings account, personal account).
	•	Create loans for customers.
	•	Deposit loan amounts into one of the customer’s bank accounts.

Only one administrator account is required. Administrators cannot create other administrators.

⸻

Customer Features

Customers can:
	•	Log in and view an overview of all their bank accounts.
	•	See the account type and current balance.
	•	View transaction history for each account.
	•	Create additional bank accounts of different types.
	•	Transfer money between their own accounts.
	•	Transfer money to other customers using their account number.

When transferring money:
	•	Funds are withdrawn from the sender’s account.
	•	Funds are deposited into the recipient’s account.

⸻

Database
	•	The application uses a provided database with example data.
	•	The database structure resembles a real banking system.
	•	The database may be modified if needed to adapt to the application’s design.

⸻

Security
	•	Authentication and authorization are implemented using JWT (JSON Web Tokens).
	•	User roles determine access to protected endpoints.

⸻

Technical Requirements
	•	The application is implemented using ASP.NET Web API.
	•	The solution follows object-oriented design principles and course guidelines.
	•	All API endpoints:
	•	can be tested using Postman
	•	return appropriate HTTP status codes and response data
	•	The Web API runs without errors.
