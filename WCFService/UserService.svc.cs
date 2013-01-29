using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Facade;

namespace WCFService
{
    public class UserService : IUserService
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
