# Approach

This is a .Net 6 Web API application which returns data from an EF core in-memory database. There are 3 REST endpoints which return the following:
- List of users. The Id and Name of a user is returned.
- List of investments for a user. Given the user Id, a list of investments with the just the investment Id and investment name are returned. 
- Details for an investment. Given the user Id and investment Id, the details for the specified investment is returned.

The REST endpoint can be tested via swagger at following URL: 
https://localhost:7063/swagger/index.html.

Pricing for stocks and mutual funds are obtained via https://www.alphavantage.co. You will need a key to get this to work. You can obtain a free key at https://www.alphavantage.co/support/#api-key. Once you get the key put it into the appsettings.json file relacing the ##APIKEY## value.

Prices for bonds are fixed to always return 100. 

User Id of 1 holds 4 stocks, User Id of 2 holds to mutual funds and two bonds. Id’s for stocks and mutual funds are the ticker symbol and Id’s for bonds are CUSIPs.

# Assumptions

- We only care about reading the investment data.
- We can price the investment data however we like.
- We only care about stocks, bonds, and mutual funds.
- We identified users via their Id. I created another endpoint to return the list of valid user Id’s.
- If the current price can’t be obtained, we can see null data for data points that rely on the current price.  

# Compiling

This is a .Net 6 project, you can download the SDK from https://dotnet.microsoft.com/en-us/download/dotnet/6.0.

1. From a console or terminal window and go to the root of directory of where you downloaded code. The same directory as the *InvestmentPerformanceWebApi.sln* file.
1. Type the following:
```
dotnet build
```

# Running the Tests
1. From a console or terminal window and go to the root of directory of where you downloaded code. The same directory as the InvestmentPerformanceWebApi.sln file.
1. Type the following:
```
dotnet test
```

# Running the Application
1. If you haven’t update the pricing ApiKey, go to https://www.alphavantage.co/support/#api-key and get one. Update the Pricing:ApiKey in the appsettings.json file with your key.
2. From a console or terminal window and go to the root of directory of where you downloaded code. The same directory as the InvestmentPerformanceWebApi.sln file.
3.	Type the following:
```
dotnet run --project InvestmentPerformanceWebApi
```
4.	Open a browser to https://localhost:7063/swagger/index.html
5.	Expand the **GET /api/v1/User** under the **User** section. Then click the "Try it out" button and click the "Execute" button to get a list of valid users. 
6.	Expand the **GET /api/v1/Holding/{userId}** under the **Holding** section. Then click the "Try it out" button, enter a User Id (from step 5) and click the "Execute" button to get a list of investment for the given user.
7.	Expand the **GET /api/v1/Holding/{userId}/{id}** under the **Holding** section. Then click the "Try it out" button, enter a User Id (from step 6) and the Id (form step6) and click the "Execute" button to get a details for specified investment.	
