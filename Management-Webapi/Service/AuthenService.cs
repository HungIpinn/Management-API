using Management_Webapi.Model.Dto;
using Management_Webapi.Service.Interface;
using Microsoft.AspNetCore.Authentication;

namespace Management_Webapi.Service
{
    public class AuthenService : IAuthenService
    {
        List<UserDto> _users = new List<UserDto>();

        public UserDto RegisterUser(string username, string password)
        {
            var user = new UserDto { Id = Guid.NewGuid().ToString(), Username = username, Password = password };
            _users.Add(user);
            return user;
        }

        public UserDto AuthenticateUser(string username, string password)
        {
            return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public UserDto GetUserById(string userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }
    }
}
