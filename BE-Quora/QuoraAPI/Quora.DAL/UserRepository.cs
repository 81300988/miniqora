using Dapper;
using Quora.DAL.Interface;
using Quora.Model;
using System;
using System.Data;
using System.Linq;

namespace Quora.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;
        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public User AddUser(Register user)
        {
            var paramForAddAnswer = new DynamicParameters();
            paramForAddAnswer.Add("@FirstName", user.FirstName);
            paramForAddAnswer.Add("@LastName", user.LastName);
            paramForAddAnswer.Add("@Email", user.Email);
            paramForAddAnswer.Add("@Password", user.Password);

            var existedEmails = _dbConnection.Query<String>("SELECT Email from [User]").ToList();
            if (existedEmails.Contains(user.Email) == true)
            {
                return null;
            }
            var userId = _dbConnection.Query<int>(@"insert into [User](FirstName, LastName, Email, Password) 
                                                        values(@FirstName,@LastName,@Email,@Password);
                                                    SELECT CAST(SCOPE_IDENTITY() as int)", paramForAddAnswer).Single();

            var newUser = _dbConnection.QueryFirstOrDefault<User>("SELECT UserId, FirstName, LastName, Email, Password FROM [User] where UserId = @userId", new { userId = userId});
            return newUser;
            
        }

        public User GetUser(LoginModel model)
        {
            var user = _dbConnection.QueryFirstOrDefault<User>("SELECT * FROM [User] where Email=@Email and Password=@Password", new { Email = model.Email, Password = model.Password });

            return user;
        }

        public User GetUserById(int userId)
        {
            var user = _dbConnection.QueryFirstOrDefault<User>(@"SELECT     UserId, FirstName, LastName, Email, Avatar 
                                                                 FROM       [User] 
                                                                 where      UserId=@UserId", new { UserId = userId });

            return user;
        }

        public User GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
