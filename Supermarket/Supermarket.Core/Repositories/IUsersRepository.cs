using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Core.Models;

namespace Supermarket.Core
{
    public interface IUsersRepository : IDisposable
    {
        IQueryable<UserProfile> GetUsers();
        UserProfile GetUser(int id);
        void AddUser(UserProfile userInfo, string password);
        void DeleteUser(int id);
        void UpdateUser(UserProfile user);
        int Save();
    }
}
