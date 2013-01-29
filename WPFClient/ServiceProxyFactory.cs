using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace WPFClient
{
    public static class ServiceProxyFactory
    {
        private const string BaseUrl = "http://localhost:16014";

        public static string AuthenticationToken { get; set; }

        public static T Create<T>() where T : class
        {
            var binding = new BasicHttpBinding();
            var serviceUrl = string.Format("{0}/{1}.svc", BaseUrl, typeof (T).Name.TrimStart('I'));
            var factory = new ChannelFactory<T>(binding,serviceUrl);
            AddAuthTokenBehavior(factory);
            return factory.CreateChannel();
        }

        private static void AddAuthTokenBehavior<T>(ChannelFactory<T> factory)
        {
            var behavior = factory.Endpoint.Behaviors.Find<AuthTokenAttachBehaviour>();

            if (behavior == null)
            {
                factory.Endpoint.Behaviors.Add(new AuthTokenAttachBehaviour());
            }
        }
    }
}
