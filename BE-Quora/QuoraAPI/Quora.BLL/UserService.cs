using Quora.BLL.Interface;
using Quora.DAL.Interface;
using Quora.Model;
using System;

namespace Quora.BLL
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userService)
        {
            this._userRepository = userService;
        }
        public void ChangePassword()
        {
            throw new NotImplementedException();
        }

        public User Login(LoginModel model)
        {
            // validate if user valid
            var user = _userRepository.GetUser(model);
            return user;
            // if not
        }

        public User GetUserById(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            return user;
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public User Register(Register user)
        {
           var newUer = _userRepository.AddUser(user);
           return newUer;
        }
    }
}
