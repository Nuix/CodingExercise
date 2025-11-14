# How to Run
### With IDE

 - Open the solution file in an IDE and run the InvestmentPerformance.Api project
 - You may need to run NuGet package restore
 - Swagger should open and be able to test the api functionality

### Without IDE

 - Open a command prompt window
 - Navigate to the directory of the project and run ```dotnet build InvestmentPerformance.Api.csproj```
 - Navigate into the project folder and run ```dotnet run```
 - Open a web browser and navigate to [Swagger](htttp://localhost:5283/swagger)


 # Assumptions

 - I've assumed since the project will be part of a larger system the database object in SQLServer exists for an Investment, I have mocked the data in the service and created my model according to that.

 - Assuming data coming in to be validated before it was committed to the DB.

 - I opted not to create a front end interface for this feature since the "User stories" were about creating an API and I felt it would be out of scope and the front end would be a part of the larger system.