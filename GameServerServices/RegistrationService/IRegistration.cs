using System.ServiceModel;

namespace GameServerServices.RegistrationService
{
    [ServiceContract]
    public interface IRegistration
    {
        [OperationContract]
        bool Register(string email, string nickname, string password);

    }
}