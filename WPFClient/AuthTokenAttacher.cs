using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace WPFClient
{
    public class AuthTokenAttacher : IClientMessageInspector
    {
        private const string AuthTokenHeaderName = "AuthToken";

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            if (ServiceProxyFactory.AuthenticationToken == null)
                return null;
            HttpRequestMessageProperty httpRequestMessage;
            object httpRequestMessageObject;
            if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out httpRequestMessageObject))
            {
                httpRequestMessage = (HttpRequestMessageProperty)httpRequestMessageObject;
                if (string.IsNullOrEmpty(httpRequestMessage.Headers[AuthTokenHeaderName]))
                {
                    httpRequestMessage.Headers[AuthTokenHeaderName] = ServiceProxyFactory.AuthenticationToken;
                }
            }
            else
            {
                httpRequestMessage = new HttpRequestMessageProperty();
                httpRequestMessage.Headers.Add(AuthTokenHeaderName, ServiceProxyFactory.AuthenticationToken);
                request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessage);
            }
            return null;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            
        }
    }

    public class AuthTokenAttachBehaviour : IEndpointBehavior
    {
        public void Validate(ServiceEndpoint endpoint)
        {
            
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new AuthTokenAttacher());
        }
    }
}
