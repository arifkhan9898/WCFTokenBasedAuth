using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using Facade;

namespace WCFService
{
    public class AuthService : IAuthService
    {
        public string AuthenticateUser(string userName, string password)
        {
            var isValidUser = new AuthenticationProvider().Authenticate(userName, password);
            if(!isValidUser)
                throw new FaultException("Invalid UserName/Password", new FaultCode("InvalidCredentials"));

            return new MessageTokenService().Generate(userName);
        }
    }
}
