# Investment System - README

## Overview
**Current Required Functionalities:**  
1. **Get investments for a user**  
   - Returns investment ID and investment name.  
   - **Note:** This returns the **investment name**, not the user's name.  

2. **Get investment by ID**  
   - Returns details including:  
     - Shares  
     - Cost per share  
     - Current value  
     - Current price  
     - Term  
     - Total gain/loss  

---

## Assumptions
- The database and tables do **not exist** yet; no predefined entity models were provided.  
- This system is **read-only** at this stage (no inserts/updates).  
- The system is assumed to be an **online portal**, and users can only access **their own investments**.  
- Investments cannot exist without a user, and an investment belongs to **only one user**.  
- Current price and value of investments are updated dynamically (e.g., via a separate process or live API).  
- All investments are assumed to be in a **single currency (USD)**.  
- The "name" returned by the API refers to the **investment name**, not the user name.  

**Additional Notes on Design:**  
- Considered a 3-layer architecture (View, Domain Model with logic, DB Entity).  
  - Views(DTOs) are kept separate from the domain model for flexibility.  
  - If calculated fields were stored in the database, design decisions may change.  

---

## Running the Application
- Uses **SQLite** as the database.  
- Auto-seeding of data is configured in `Program.cs`. No migrations are needed.  
- **Endpoints:**  
  - `GET /users/{userId}/investments/` → returns all investments for a user  
  - `GET /users/{userId}/investments/{investmentId}` → returns investment details by ID  
- Check the startup window to see which **port** the API is running on locally.  
- **Seed Data:**  
  - Defined in `AppDbContext.cs`  
  - Includes User 1 and Investments 1, 2, and 3  

---

## Future Considerations
- Support for multiple currencies  
- Integration with live stock price APIs  
- Extended features for admin users or multi-user investment views  
- Buy/sell, deposit, withdrawal, and reporting functionalities  
- validations when data inserted into database, determine validation rules
- Is there need to calculate gain/loss and total value outside of investment domain model?
