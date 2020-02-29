
using Quora.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.DAL.Interface
{
    public interface IUserRepository
    {
        User GetUser(LoginModel model);
        User GetUserById(int userId);
        User GetUsers();
        User AddUser(Register user);
    }
}

