using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;

namespace WCFService
{
    public class MessageTokenAuthorizationManager : ServiceAuthorizationManager
    {
        private const string AuthTokenHeaderName = "AuthToken";

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            Message requestMessage = operationContext.RequestContext.RequestMessage;

            if (requestMessage.Headers.Action.Contains("IAuthService"))
                return true;

            string token = null;
            object httpRequestMessageObject;
            if (requestMessage.Properties.TryGetValue(HttpRequestMessageProperty.Name, out httpRequestMessageObject))
            {
                var httpRequestMessage = (HttpRequestMessageProperty)httpRequestMessageObject;
                token = httpRequestMessage.Headers[AuthTokenHeaderName];
            }

            if (token==null)
                return false;

            var userName = new MessageTokenService().Decode(token);

            //TODO: lookup the user and setup the correct identity/context

            return userName != null;
        }
    }
}