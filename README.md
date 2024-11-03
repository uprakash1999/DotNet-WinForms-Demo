WinFormsDemo - User Management Application
A simple yet functional .NET WinForms application for managing user records with CRUD (Create, Read, Update, Delete) operations using a SQL Server database. This application is designed for ease of use and demonstrates basic database integration, UI interaction, and form-based navigation in a desktop environment.

Key Features
Add New Users: Easily add user details, including name, email, gender, contact, and location information, to the database.
Retrieve User Information: Fetch details of a specific user by their unique ID, displaying the information in a form for easy review.
Update Existing Records: Modify user data and save changes directly to the database, with a user-friendly form-based interface.
Delete Users: Remove user records from the database with a confirmation prompt to avoid accidental deletions.
Multi-Form Navigation: Dedicated forms for each action (Add, View, Update, Delete) for a clean and modular user experience.
Confirmation and Error Handling: Alerts and messages guide the user through each operation, ensuring safe and error-free data management.
Technologies Used
.NET WinForms for the desktop application
SQL Server for database management, using SQL Server LocalDB for easy integration
Stored Procedures to handle database operations (CRUD) securely and efficiently
Requirements
.NET Framework installed on the system (target version specified in the project)
SQL Server (LocalDB is recommended for local development)
Visual Studio (for editing, building, and running the application)
Setup Instructions
1. Clone the Repository
sh
Copy code
git clone https://github.com/uprakash1999/DotNet-WinForms-Demo.git
cd WinFormsDemo
2. Open the Project in Visual Studio
Open the WinFormsDemo.sln solution file in Visual Studio to work with the project.

3. Configure SQL Server Database Connection
Ensure SQL Server (LocalDB or another SQL instance) is running on your machine. Update the connection string in the application code to match your database server configuration:

csharp
Copy code
SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UserDB;Integrated Security=True");
4. Create the Database and Table
Run the following SQL commands in SQL Server Management Studio or another SQL tool to create the UserDB database and the userData table:

sql
Copy code
CREATE DATABASE UserDB;
USE UserDB;

CREATE TABLE userData (
    UserId INT PRIMARY KEY IDENTITY,
    Name VARCHAR(30),
    Gender VARCHAR(20),
    EmailId VARCHAR(50),
    MobileNo VARCHAR(13),
    Country VARCHAR(20),
    State VARCHAR(20)
);
5. Create Stored Procedures
The application uses stored procedures for managing user records. Execute the following stored procedures in SQL Server:

sql
Copy code
CREATE PROCEDURE GetUserById
    @UserId INT
AS
BEGIN
    SELECT UserId, Name, Gender, EmailId, MobileNo, Country, State
    FROM userData
    WHERE UserId = @UserId;
END;

CREATE PROCEDURE DeleteUser
    @UserId INT
AS
BEGIN
    DELETE FROM userData
    WHERE UserId = @UserId;
END;

CREATE PROCEDURE UpdateUser
    @UserId INT,
    @Name VARCHAR(30),
    @Gender VARCHAR(20),
    @EmailID VARCHAR(50),
    @MobileNo VARCHAR(13),
    @Country VARCHAR(20),
    @State VARCHAR(20)
AS
BEGIN
    UPDATE userData
    SET Name = @Name,
        Gender = @Gender,
        EmailID = @EmailID,
        MobileNo = @MobileNo,
        Country = @Country,
        State = @State
    WHERE UserId = @UserId;
END;
6. Build and Run the Application
In Visual Studio, build the solution and then start the application. This will open the WinForms interface where you can begin managing user records.

Usage Guide
Add Users: Use the "Add" form to enter new user data, including their name, gender, email, contact number, country, and state. Press "Save" to add the record to the database.

Retrieve User Data: Enter a user ID in the "View" form to retrieve specific details about that user, displaying the data in a structured format.

Update User Information: To modify user details, retrieve the user data first by entering their ID. Make the necessary changes and press "Update" to save changes to the database.

Delete Users: Enter the user ID in the "Delete" form to see the user details. Confirm the deletion when prompted. This action is permanent.

Future Improvements
Enhanced Validation: Add additional validation on input fields to further ensure data accuracy.
Error Logging: Implement logging for detailed error tracking and easier debugging.
Search Functionality: Enable searching users by name or other criteria to improve user retrieval.
License
This project is licensed under the MIT License. See the LICENSE file for details.
