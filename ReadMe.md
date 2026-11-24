# Investment Performance API

A read-only ASP.NET Core API for listing a user's investments and calculating their current value, holding term, and unrealized gain or loss. Implements the [Investment Performance Web API exercise](InvestmentPerformanceWebAPI.md).

**Live demo:** [nuix.keehar.net/docs](https://nuix.keehar.net/docs)
**OpenAPI:** [nuix.keehar.net/openapi/v1.json](https://nuix.keehar.net/openapi/v1.json)

The demo contains fictional users and fixed price snapshots. No API key or external market-data service is needed.

## Run with Docker

Prerequisite: Docker with Docker Compose v2. Both ARM64 and x86-64 hosts are supported.

```sh
docker compose up --build -d --wait
curl http://localhost:8080/health
```

Open [localhost:8080/docs](http://localhost:8080/docs) to explore the API. SQLite migrations and seed data are applied automatically on startup. The named volume preserves data across container restarts. The application runs as a non-root user, with a memory limit and bounded logs.

```sh
docker compose logs api
docker compose down
```

Stopping the stack retains its database. To reset this demo's data explicitly, run `docker compose down --volumes` before starting it again. This deletes the demo volume.

## Run with the .NET SDK

Prerequisite: .NET 10 SDK. A separate database installation is unnecessary.

```sh
dotnet restore
dotnet run --project src/InvestmentPerformance.Api --urls http://localhost:8080
```

The local database is created at `src/InvestmentPerformance.Api/data/investments.db`. Override its location with the `ConnectionStrings__Investments` environment variable, for example `Data Source=/path/to/investments.db`.

## Test

```sh
dotnet test --configuration Release
```

The tests cover calculation accuracy, fractional shares, final rounding, the calendar-year holding boundary, leap years, invalid data, HTTP validation and errors, user filtering, database persistence, and repeatable initialization. HTTP integration tests run the application in Production mode with a separate temporary SQLite database and a fixed clock. They exercise the real database provider rather than an in-memory substitute.

GitHub Actions builds and tests the solution, then builds the Linux container and verifies its startup, health check, sample calculations, and restart with a persistent volume.

## Endpoints

| Request | Result |
| --- | --- |
| `GET /api/users/{userId}/investments` | Investment IDs and names for that user |
| `GET /api/users/{userId}/investments/{investmentId}` | Investment details and performance metrics |
| `GET /health` | Database connectivity check |
| `GET /docs` | Interactive API reference |
| `GET /openapi/v1.json` | Machine-readable API definition |

IDs must be positive 32-bit integers. Invalid IDs return `400`; unknown users or investments return `404`. Looking up an investment belonging to another user returns the same `404` as an absent investment. A known user without investments receives `200` with `[]`.

Errors use `application/problem+json` with a `traceId` for log correlation. Unexpected exceptions are logged server-side and return a generic `500` without stack traces or database details. Requests produce structured JSON logs with method, path, status, duration, and trace ID.

## Try the sample data

| User ID | Investment IDs | Purpose |
| --- | --- | --- |
| 1 | 1, 2, 3 | Stock gain, bond loss, mutual fund break-even |
| 2 | 4 | A separate user's holding |
| 3 | None | Empty portfolio |

```sh
curl http://localhost:8080/api/users/1/investments
curl http://localhost:8080/api/users/1/investments/1
```

Investment 1 returns the following values. `valuationDate` uses the current UTC date; the example below is for November 23, 2026.

```json
{
  "id": 1,
  "name": "Example Technology Stock",
  "assetType": "stock",
  "shares": 12.5,
  "costBasisPerShare": 80,
  "currentValue": 1150.00,
  "currentPrice": 92,
  "term": "long",
  "totalGainLoss": 150.00,
  "currency": "USD",
  "purchasedOn": "2024-05-10",
  "valuationDate": "2026-11-23",
  "priceAsOf": "2026-11-20T21:00:00+00:00"
}
```

## Calculation rules and assumptions

- Each investment represents **one user-owned purchase lot**, with one purchase date and purchase price per share. Repeated purchases of the same security would be separate lots.
- `currentValue = shares × currentPrice`.
- `totalGainLoss = (shares × currentPrice) − (shares × costBasisPerShare)`.
- The anniversary of the purchase remains `short` term; dates strictly after that anniversary are `long` term. The comparison uses calendar years, not 365-day intervals. A February 29 purchase has a February 28 anniversary in a non-leap year.
- All prices and results are USD. Shares and per-share prices retain decimal precision. Only final monetary totals are rounded to two decimal places, using midpoint-to-even rounding. Gain/loss is calculated before rounding either component.
- Calculations use C# `decimal`. SQLite stores these values as exact text, avoiding binary floating-point conversion. Queries filter and sort on IDs; monetary arithmetic is performed in C# after retrieval.
- Shares must be greater than zero; prices may be zero. Each input amount is limited to one billion to bound calculations. Invalid stored values or future purchase/quote dates produce a logged server error rather than an incorrect valuation.
- Prices are fixed demonstration snapshots from November 20, 2026. `priceAsOf` identifies the quote snapshot; `valuationDate` is the date used to determine the holding term. Neither represents a live market quote.
- Performance means unrealized price gain/loss. Fees, dividends, bond coupons, accrued interest, splits, tax rules, and currency conversion are outside this exercise. Bond holdings use the same per-unit price calculation specified in the assignment.

## Structure and integration considerations

`src/InvestmentPerformance.Api` contains the HTTP endpoints, database model/migrations, initialization, and a separate pure calculation function. `tests/InvestmentPerformance.Api.Tests` contains unit and HTTP integration tests. The clock is injectable, so date-dependent behavior is reproducible.

SQLite keeps this small read-only service portable and easy to evaluate, while C# and ASP.NET Core align with the requested stack. A SQL Server deployment would require its own EF Core provider, type mappings, migrations, and database integration tests; it is not configured in this submission.

The demo deliberately exposes only fictional records and has no authentication. Before integrating real accounts, derive the user identity from validated authentication and enforce authorization against that identity. A supplied user ID and query filtering alone are not access control.

This deployment uses one application instance with a local SQLite volume. Startup migrations and initialization are suitable for that model. A larger system should manage schema changes as a deployment step, integrate an authoritative price service and lot ledger, define quote freshness requirements, and select a database/concurrency strategy for its workload. Trading and account-management endpoints are outside this read-only component.

The container listens on port 8080. The public demo uses an external HTTPS reverse proxy; the local Compose configuration binds the application only to loopback. The reviewer can run the full service locally without any access to the demo hosting environment.

The original [Online Ordering SQL exercise](OnlineOrderingSQL.md) is retained unchanged.

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
