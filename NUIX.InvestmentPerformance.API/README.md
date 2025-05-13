# 📊 Investment Performance API

This is a production-based Web API built in **C# (.NET 8)** for an investment trading platform.
It allows querying of a user's investments and detailed performance data for individual investments.

## 🚀 Features

- Get a list of investments for a user
- Get detailed performance metrics of an individual investment
- SQL Server integration with Entity Framework Core
- Global exception handling middleware
- Unit-tested business logic

## ✅ Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/ssms/download-sql-server-management-studio-ssms)

## 🗃️ Database Setup

- Open SSMS and restore the database using the .bak file located at: /Database/InvestmentDb.bak

## 🔐 Configuration

- Ensure your connection string in appsettings.json looks like this or something similar to this:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=InvestmentDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

⚙️ Scalability Considerations
Here’s how I would scale this system if the opportunity presented itself:

- Indexing: Ensure DB indexes on UserID, InvestmentID
- Pagination & Filtering: Add skip/take parameters to endpoints
- Command Query Responsibility Segregation (CQRS): Split read/write logic if write and read scale differently.
