# InvestmentPerformance Web API
Submitted by Ben Sgroi

## Running the Code
The API is written in .NET Core 8. From the InvestmentPerformance folder, run `dotnet run` to start the web server. When run locally, it will create a SQL Server Express LocalDB with seeded data (see `DbInitializer.cs`). Use Postman or similar tools to run the APIs. The current user ID should be set in a request header "UserID".

The latest version of AutoMapper requires a license key. [Create one on their site](https://automapper.io/) and set it in `appsettings.json` or in an environment variable named `AutoSettings__LicenceKey`.

## Assumptions & Notes
- I didn't implement authentication, as this seemed out of scope for a simple Web API that's part of a larger system. I decided to use a request header "UserID" to specify the user, but in a real system, the user would likely be derived from the authorization header.
- I assumed that an investment record is immutable, that additional stocks bought would result in a new investment record. Otherwise, fields like "term" and "cost basis per share" don't make sense.
- A RDBMS like SQL Server is the right choice for this application, since consistency is critical in fast-moving domains like stock transactions.
- AutoMapper may have been overkill for a small application like this, but it can be useful for larger projects/models.

## Future Enhancements
- Add more unit test coverage.
- Use a centralized logging solution. 
- A StockPrices table could be added to track the performance of a stock over time.
- Depending on the anticipated load, we may need to scale resources. For web servers, we can rely on a load balancer or API manager for horizontal scaling. Distributed caching via Redis or similar may be required to improve performance. Auto-scaling can help if the user load is not predictable.

***

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
