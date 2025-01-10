using InvestmentAPINuix_JBF.Models;

namespace InvestmentAPINuix_JBF.Classes
{
    public static class InvestmentHelper
    {
        //Used to get all investments + details of those investments associated to the user
        public static List<Investment> GetInvestmentByUser (InvestmentAPIDatabase ctx, int userId)
        {
           
             
            List<Investment> foundInvestments =
            (
                from
                    inv 
                in
                    ctx.Investments

                where 
                    inv.Investor == userId

                select 
                    inv
            ).ToList();

            return foundInvestments;
        }
        //Used for finding just the investment id and name
        public static List<Object> GetOnlyInvestmentById (InvestmentAPIDatabase ctx, int userId)
        {
           
             
            List<Object> foundInvestment =
            (
                from
                    inv 
                in
                    ctx.Investments

                where 
                    inv.Investor == userId
                select 
                    new{
                        inv.Id, inv.Company
                    }
                    
            ).ToList<Object>();

            return foundInvestment;
        }

        //Used to find details related to an investment by supplying the investment ID
        public static List<Investment> GetInvestmentDetailsById (InvestmentAPIDatabase ctx, int invId)
        {
           
             
            List<Investment> foundInvestmentDetails =
            (
                from
                    inv 
                in
                    ctx.Investments

                where 
                    inv.Id == invId

                select 
                    inv
            ).ToList();

            return foundInvestmentDetails;
        }
    }
}
