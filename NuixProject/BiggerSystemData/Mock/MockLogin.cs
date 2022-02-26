using Nuix_Project.APIObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nuix_Project.Data
{
    public class MockLoginLayer : ILoginLayer
    {
    

        //Typically we would make an expire time, but will pass it for now.
        public string GetLoginToken(string username, string password)
        {
            return Guid.NewGuid().ToString();
        }

        //Were mocking! let everyone in today!
        public bool ValidateToken(String g)
        {
            return true;
        }
    }
}
