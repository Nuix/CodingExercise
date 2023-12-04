The following are the instructions to setup the database needed to run the code!

1. Ensure Microsoft SQL Server is installed on the computer
2. Create a new database using the InvestmentPerformance.mdf file in the \Data directory. (If this doesn't work just create a Database with the name InvestmentPerformance)
3. run the initialDbSetup.sql script to populate the newly created database with data!

To launch the program please run the following command:
dotnet run
You can also utilize Visual Studio and run CTRL + F5 if needed!

Testing:
You can access the api via browser here are the following example requests:

To get a list of investments for a userId:
curl -X 'GET' \
  'https://localhost:<port>/api/InvestmentPerformance/getUserInvestments/<userId' \
  -H 'accept: */*'

  example response should be something like:
  [
  {
    "UserId": 1,
    "UserName": "Tyler Pingree",
    "Investments": [
      {
        "InvestmentId": 1,
        "StockName": "Tech Stock A"
      },
      {
        "InvestmentId": 2,
        "StockName": "Energy Stock B"
      },
      {
        "InvestmentId": 3,
        "StockName": "Pharma Stock C"
      }
    ]
  }
]


To get details around a particular investment for a user:
curl -X 'GET' \
  'https://localhost:<port>/api/InvestmentPerformance/getInvestmentDetails/<userId>/<investmentId>' \
  -H 'accept: text/plain'

  example response:
  {
  "InvestmentId": 1,
  "UserId": 1,
  "StockName": "Tech Stock A",
  "Shares": 100,
  "PurchasePrice": 50.00,
  "CurrentPricePerShare": 60.00,
  "PurchaseDate": "2022-01-01T00:00:00",
  "CurrentValue": 6000.00,
  "TotalGainOrLoss": 1000.00,
  "Term": "Long",
  "User": {
    "UserId": 1,
    "UserName": "Tyler Pingree"
  }
}

To run the tests you would navigate to the InvestmentPerforamnceApi\Tests folder and run the following command
dotnet test


