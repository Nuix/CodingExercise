# Instructions and Description
This solution is a C# .Net 6 Web API application 
**Instructions**
-Intstall Visual Studio 2022 with .Net 6
-Running InvestmentPerformance.Host should automatically open your browser to the Swagger index page

Projects in solution:
**InvestmentPerformance.Host**
- Web API project
- Configured to use Swagger when debugging to test the APIs
- This should be set as the startup project
**InvestmentPerformance.Data**
- Contains a local database file and models of the data structure created with Entity Framework
- Entity Framework context used to interact with the database file
- Referenced by Business project
**InvestmentPerformance.Business**
- Business logic to map domain models to view models
- Entity Framework calls to database
**InvestmentPerformance.Tests**
- Tests for the Host and Business projects

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