# Investment Performance Web API
This is my solution to the Web API challenge. I built it using C#.NET with a Sqlite database.
I chose to use Sqlite as it makes for an easy portable database.  The database is modeled on:
![Diagram of Database](images/InvestmentDatabase.png).

## Test Data

The database is currently loaded up with test data for exploratory purposes.

### User Table
![User Table Test Data](images/user.png).

### Stock Table
![Stock Table Test Data](images/stock.png).

### Stock Price Table
![Stock Price Table Test Data](images/stock_price.png).

### Investment Table
![Investment Table Test Data](images/investment.png).

## Running
To run this, navigate to InvestmentWebApi/InvestmentWebApi directory and
execute 
```dotnet run```

This should start a WebAPI on your machine.  It will output a URL where it is listening.

To get a list of current investments for the user, send a request to:
```
localhost:[port]/investment/foruser/[userId]
```
The database currently contains 5 users with IDs ranging from 1 to 5.


To get details for a user's investment, send a request to:
```
localhost:[port]/investment/foruser/[investmentId]
```
The database currently contains 11 investments with IDs ranging from 1 to 11.  Alternatively, you can pick an investmentId from the response to the foruser API call.

There is also a swagger interface running at:
```
localhost:[port]/swagger/index.html
```

## Testing
To run unit tests, navigate to InvestmentWebApi/Tests directory and
execute 
```dotnet test```
