Hello Nuix Team! I had an absolutely wonderful time working on this project and I very much appreciate the time being used to review this.

Note: Due to the API being deployed using Azure's Free Resources, on first load there may be a slight delay in communication with the API. 

To access this application:

Frontend Vue: 
investmentapinuixjbf.netlify.app

Description: This is a quick setup of a frontend Vue application with some default TailwindCSS. I created four separate users in my database, and you are able to select any of them to view their investments and details of those investments quickly. For this part, I assumed that the User Experience would be better if all investments and their details are shown in a table immediately upon selecting a user. 

API Calls:
Get List of Investments for The User
https://investmentapinuixchallenge-hseyfaefandtacd2.canadaeast-01.azurewebsites.net/api/investmentdata/nameIdOnly/1

Description: This will return the Id, and Name of the Company that the investment is related to based on the user entered. 

Get Investment Details
https://investmentapinuixchallenge-hseyfaefandtacd2.canadaeast-01.azurewebsites.net/api/investmentdata/details/{investmentId}

Description: Based on the Investment Id gathered in the previous API call, you can receive all details entered in the database for that particular investment (Cost Basis Per Share, Number of Shares, etc.)


Assumptions and Design Decision Notes:
1. I assumed that the data for stocks was stored and kept in house.
2. I assumed that the users only purchased stocks from a company in a singular batch. In this case, I did not include that stocks of the same company could be purchased at different time with a different cost basis per share.
3. I assumed that the current application is built on a SQL Server Database using a .NET EF Core backend, and Vue front end.
    a. I added two "new" tables to the database and input seed data, so that the API calls could return with data. The script for the tables and seed data can be found in SQL_FOR_DB folder.
    b. After adding the tables, I rescaffolded the .NET application to include the new tables in the DBContext, as well as build models for Users and Investments. 
    c. Locally I used SQL Server Management Studio to perform all database operations, and then moved the database to Azure SQL Server so that the application would work non-locally. 
    d. Once the API was built, and the Azure SQL Server database was up and running, I updated my connection strings, and deployed the API to an Azure App Service. 

4. I assumed the current frontend of the production application was built using Vue.
    a. I built the client side of the application using Vue and TailwindCSS. I had trouble installing and using ExtJS unfortunately, so I was unable to include this within this project. I am typically more comfortable with React, and have not used Vue professionally, however I enjoyed the process of learning it more in depth through the project. 
    b. I made the assumption on the frontend that production users would more than likely prefer less clicks to view all the details of their investments, so I created an additional API call to receive all investments and their details at once.
    c. I deployed this part of the application through Netlify.


