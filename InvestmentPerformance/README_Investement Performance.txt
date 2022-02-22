Nuix Investment Performance
===========================
This contains a .net Core Web Api and a small simple SQL Server definition.  There is a SQL DDL as well as a .bak backup.  It was created with a code first approach, defining the basic User and Investment classes and allowing Entity Framework to generate the tables.  

/api/Users
	- list all users in the system
	
/api/Users/{id}
	- list ID and Name of specific user investments
	
/api/Users/{id}/investment/{investmentID}
	- display details of a specific user investment
	
Any customer id or investmentID that is not found will return an Http 404 Not Found response
	
