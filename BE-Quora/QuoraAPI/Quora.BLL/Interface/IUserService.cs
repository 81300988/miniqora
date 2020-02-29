using Quora.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.BLL.Interface
{
    public interface IUserService
    {
        void Logout();
        User Register(Register user);
        void ChangePassword();
        User Login(LoginModel model);
        User GetUserById(int userId);
    }
}
