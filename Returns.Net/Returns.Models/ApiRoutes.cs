namespace Returns.Models;

public static class ApiRoutes
{
    public static class User
    {
        public const string Base = "user";
    }
    public static class Investment
    {
        public const string Base = "investment";
        public const string Query = Base + "/Query";
    }
}

/*
 
 user - has a share
 share - has a price, term
 
 current value and total gain/loss can be calculated props
 
Cost basis per share: this is the price of 1 share of stock at the time it was purchased
Current value: this is the number of shares multiplied by the current price per share
Current price: this is the current price of 1 share of the stock
   
Term: this is how long the stock has been owned. <=1 year is short term, >1 year is long term
   
Total gain or loss: this is the difference between the current value, and the amount paid for all shares when they were purchased

*/