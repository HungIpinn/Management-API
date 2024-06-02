using Management_Webapi.Model.Dto;

namespace Management_Webapi.Service.Interface
{
    public interface IAuthenService
    {
        UserDto RegisterUser(string username, string password);
        UserDto AuthenticateUser(string username, string password);
        UserDto GetUserById(string userId);
    }
}
