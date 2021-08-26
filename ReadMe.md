# Investment Performance Web API

## Setup Instructions:
1. Execute the SQL script from "Nuix.Api\Resources\Setup.sql".
2. Modify the "Nuix.Api\appsettings.json" file to point the created database.
3. Verify that the "Nuix.Api" project is set as the startup project for the solution.
4. Build and execute the solution which should display the Swagger UI for testing the API.  For manual testing purposes the two endpoints are "​/api​/users​/{userId}​/investments​/{investmentId}" and "​/api​/users​/{userId}​/investments".

## Notes:
I chose to implement the assignment using ASP.NET Core and EF Core. I had never implemented an API from scratch using .NET Core so it was a good learning experience.  I chose to do a database first EF implementation so that the database is properly designed.

If I were going to deploy this solution as a public facing API I would expect to add this to an existing product that already has authentication and authorization.