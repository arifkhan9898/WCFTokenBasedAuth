using System.ServiceModel;

namespace Facade
{
    [ServiceContract]
    public interface IAuthService
    {
        [OperationContract]
        string AuthenticateUser(string userName, string password);
    }
}
