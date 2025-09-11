# Investment Performance Web API

## Prerequisites
- .NET SDK 8.0+
- Docker + Docker Compose (optional, for containerized run)

## Run locally (using launchSettings profile)
1. Restore and build:
   ```bash
   dotnet restore
   dotnet build
   ```
2. Run with the "InvestmentPerformanceWebApi" launch profile:
   ```bash
   dotnet run --launch-profile "InvestmentPerformanceWebApi"
   ```
    - In IDEs, select the desired launch profile from the run configuration and start debugging/run.
3. Browse:
    - Swagger UI: http://localhost:5113/swagger/index.html
    - Health: http://localhost:5113/Investment/health

Note: The app seeds demo data on startup.

## Run with Docker Compose
1. Build and start:
   ```bash
   docker compose up --build
   ```
2. Browse (use the port published in your compose file/output):
    - Swagger UI: http://localhost:5001/swagger/index.html
    - Health: http://localhost:5001/Investment/health

## Run tests
From the solution root (or tests project directory):
   ```bash
   dotnet test
   ```
## Quick request examples
- Health:
  ```bash
  curl http://localhost:PORT/Investment/health
  ```
- Investments for a user:
  ```bash
  curl http://localhost:PORT/Investment/1001
  ```
- Investment details:
  ```bash
  curl http://localhost:PORT/Investment/1001/1
  ```

Replace PORT with the port configured by your selected launch profile or Docker Compose.
