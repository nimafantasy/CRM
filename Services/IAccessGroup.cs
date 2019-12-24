using Core.Domain.AccessGroups;
using Core.Domain.Departments;
using Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAccessGroup
    {
        void AddNewAccessGroup(AccessGroup AccessGroup);

        void RemoveAccessGroup(AccessGroup AccessGroup);

        void EditAccessGroup(AccessGroup AccessGroup);


        List<AccessGroup> GetAllAccessGroups();
        List<AccessGroup> SearchAccessGroups(string FirstName, string LastName, string PersonelId, string Username);

        AccessGroup GetAccessGroupById(int id);
    }
}
