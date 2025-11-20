## About:

Built with VS Community 2026 using C# 10.

## To Run:

- Install VS2026 + .NET 10 SDK
- Build and start debugging in VS via the UI/F5

Alternatively using the .NET CLI:
- Install .NET 10 SDK
- From the project directory, run:

```bash
dotnet restore
dotnet build
dotnet run
```

If it does not open automatically, open Swagger UI at the address given in the console output (eg. https://localhost:5001/swagger).

## Endpoint:

`GET /api/users/{userId}/investments`
- Returns a list of user's investments
`GET /api/users/{userId}/investments/{investmentId}`
- Returns details for a given investment

## Assumptions

- In-memory db for the sake of time
- Current prices are stored and fixed with the investments. In practice a current price would be sourced at runtime.