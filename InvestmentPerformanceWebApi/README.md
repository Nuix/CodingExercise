# INITIAL NOTES
- this is eventually going to be a bigger system, which might or might not have a database already existing, with user and/or investment table
- will eventually have buy, sell, deposit, withrawal, report etc. on investments
- two required functionalities, 
	1. get investments for user (return investmentid and name) 
		- IMPORTANT: doesnt note if its the name of the INVESTMENT OR name of the USER
	2. Get investment by ID (should return shares, cost per share, current value, current price, term, and total gain/loss)
		

# ASSUMPTIONS:
- For the sake of the exercise, i am going to assume that the database and tables do not exist since we were not given any model/entity requirements of what a user and investment are
- We are ONLY returning data, there are no posts/inserts into the database, so entity validations arent necessarily required at this stage
- Assumed this is an online portal that you can log into, we only want the logged in user to access their own investments
	- I would probably adjust the endpoints, if there was an admin or view all page for needs not specific to one users investments (i.e. /investments standalone endpoint)
- Assumed that investments dont exist without a user
- Assumed that an investment can only belong to one user
- I am going to assume that for the current price of the investment, there is another process that updates the prices and current value on the investments table dynamically
	- for stocks, the prices change constantly - so these values wouldnt necesarily be stored in the actual investment entity itself maybe another reference table or pulled from a live API
	- Id possibly consider storing the derived values on the Investmain domain object in the database, potentially in the same process that updates the prices
- assumed that the stock system deals in a single currency - USD $$
- assumed that the "name" required to return was the stock name, not the users name


# RUNNING APPLICATION:
- This Application uses Sqlite, with auto seeding registered in the program.cs. no database migrations need to be run for this to spin up and database to get created
- endpoints are as follows: users/{userId}/investments/ and users/{userId}/investments/{investmentId}
- check the startup window for the port that the API is listening on locally
- Seed data can be found in AppDbContext.cs
	- Included user 1 and investments 1, 2 and 3.	

# NOTES
- debated on wheter to use 3 model layer (view, domain model with logic, and db entity)
	- but wanted to keep the view separate from the domain, since we can remove/add fields to the view model as needed
    - if the calculated fields were already stored in database, this would more than likely change


