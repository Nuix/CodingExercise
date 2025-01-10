using InvestmentAPINuix_JBF.Models;

namespace InvestmentAPINuix_JBF.Classes
{
    public static class UserHelper
    {
        public static List<User> GetAllUsers(InvestmentAPIDatabase ctx)
        {


            List<User> users =
            (
                from
                    user
                in
                    ctx.Users

                select user

            ).ToList();
 
            return users;
        }
    }
}
