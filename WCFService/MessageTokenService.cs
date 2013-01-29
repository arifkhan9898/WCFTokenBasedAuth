using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace WCFService
{
    public class MessageTokenService
    {
        static readonly ObjectCache Cache = new MemoryCache("MessageTokenCache");
 
        public string Generate(string userName)
        {
            var hash = CryptoProvider.CalculateHash(userName);
            var message = string.Format("{0,-30}{1,-20}{2}", hash, DateTime.Now.Ticks,userName);
            var encMessage = CryptoProvider.Encrypt(message);
            return encMessage;
        }

        public string Decode(string token)
        {
            if (Cache.Contains(token))
            {
                return Cache.Get(token) as string;
            }

            var message = CryptoProvider.Decrypt(token);

            // check the hash and confirm that the message is valid 
            var hash = message.Substring(0, 30).Trim();
            var userName = message.Substring(50).Trim();
            if (hash != CryptoProvider.CalculateHash(userName))
            {
                throw new Exception("Unable to verify message token");
            }

            Cache.Add(token, userName, DateTimeOffset.Now.AddHours(-1));

            return userName;
        }
    }
}