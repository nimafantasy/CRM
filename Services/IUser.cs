using Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IUser
    {
        void AddNewUser(User User, int arg1, int arg2);

        bool RemoveUser(User User);

        bool EditUser(User User);

        bool AssignRoleToUser();

        List<User> GetAllUsers();
        List<User> SearchUsers(string FirstName, string LastName, string PersonelId, string Username);

        User GetUserById(int id);
    }
}
