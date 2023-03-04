# Solution to Investment Performance Web API

The solution is writteen using .Net 6 and EF core in-memory database. The solution creates two endpoints to query investment data. 
1. **(GET) /api​/Invest​/userid** This returns the investment ID and Name of all the shares owned by the given user. 

2. **(GET) /api​/Invest​/userid/investmentId** This returns the specified investment details such as number of shares, current price, etc. of a specific investment of the given user.

## Solution Structure
* Solution: [InvestmentSolution](/tree/master/InvestmentSolution)
    - Web API project: [InvestmentPerformance](/tree/master/InvestmentSolution/InvestmentPerformance)
    - Unit Test project: [TestInvestment](/tree/master/InvestmentSolution/TestInvestment)

## Instructions

The projects could be executed using both command line or visual studio. First, install the .Net 6.0 SDK from here: https://dotnet.microsoft.com/en-us/download/dotnet/6.0 

### Run on command line

* Run the API project
    - Go to the root directory of the project (`/InvestmentPerformance/InvestmentPerformance`).
    - Run the project: `dotnet run`
    - The REST endpoint can be tested via Swagger on browser: https://localhost:7051/swagger/index.html
    - This will show two endpoints. Click on the endpoint > Try it out > enter ID > Execute. The following mock IDs have been created for quick testing.
```
        User Id: f78c3d80-857d-407e-b7dd-8d2e9416fc78
        Investment Id: 480a8608-6611-46a7-a3a4-16d7b5cdfe9f
```
* Run test 
    - Go to the root directory of the test project (`/InvestmentPerformance/TestInvestment`).
    - Run the tests: `dotnet test`
    - This will show the test results.

The above two projects could be also run on Visual Studio. Open the solution (`/InvestmentPerformance/InvestmentSolution.sln`). Then select the API project or the Test project and Run.   

### Database
The EF core in-memory database has been used to manipulate data. There are three tables: 1) User, 2) Investmentment, 3) InvestmentDetails. The InvestmentDetails table has two foreign keys from the other two tables. When querying for investments, a join is performed between Investmentment and InvestmentDetails and then the required columns are selected. 
 
 ------------------

# Coding Exercise
> This repository holds coding exercises for candidates going through the hiring process.

You should have been assigned one of the coding exercises in this list.  More details can be found in the specific md file for that assignment.

Instructions: Fork this repository, do the assigned work, and submit a pull request for review.

[Investment Performance Web API](InvestmentPerformanceWebAPI.md#investment-performance-web-api)

[Online Ordering SQL](OnlineOrderingSQL.md#online-ordering)

# License

```
Copyright 2021 Nuix

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
```
