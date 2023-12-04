DROP TABLE IF EXISTS dbo.Investments;
DROP TABLE IF EXISTS dbo.Users;

-- Table to store user information
CREATE TABLE Users (
    UserId INT PRIMARY KEY,
    UserName VARCHAR(50) NOT NULL,
);

-- Table to store investment information
CREATE TABLE Investments (
    InvestmentId INT PRIMARY KEY,
    UserId INT,
    StockName VARCHAR(100) NOT NULL,
    Shares INT NOT NULL,
    PurchasePrice DECIMAL(18, 2) NOT NULL,
    CurrentPricePerShare DECIMAL(18, 2) NOT NULL,
    PurchaseDate DATE NOT NULL,
    CONSTRAINT FK_Investments_Users FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- Inserting Data into user table.
INSERT INTO Users (UserId, UserName) VALUES (1, 'Tyler Pingree');
INSERT INTO Users (UserId, UserName) VALUES (2, 'Bruce Wayne');
INSERT INTO Users (UserId, UserName) VALUES (3, 'Tony Stark');
INSERT INTO Users (UserId, UserName) VALUES (4, 'Kenny Pickett');


-- Inserting Data into Investments table
INSERT INTO Investments (InvestmentId, UserId, StockName, Shares, PurchasePrice, CurrentPricePerShare, PurchaseDate)
VALUES
    (1, 1, 'Tech Stock A', 100, 50.00, 60.00, '2022-01-01'),
    (2, 1, 'Energy Stock B', 75, 30.00, 35.00, '2022-03-15'),
    (3, 1, 'Pharma Stock C', 50, 45.00, 40.00, '2022-02-10'),
	(4, 3, 'Tech Stock B', 34, 39.00, 28.00, '2021-03-13'),
	(5, 4, 'Tech Stock C', 28, 28.00, 30.00, '2022-04-23');
    -- Add more investments as needed