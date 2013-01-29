using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFService
{
    public class AuthenticationProvider
    {
        private static readonly IDictionary<string, string> Users =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "a", "a" },
                    { "Ajeesh", "Ajeesh123" },
                    { "Shrishail", "Shrishail123" },
                    { "Shreyas", "Shreyas123" },
                    { "Arun", "Arun123" },
                };

        public bool Authenticate(string userName, string password)
        {
            return Users.ContainsKey(userName) && Users[userName] == password;
        }
    }
}