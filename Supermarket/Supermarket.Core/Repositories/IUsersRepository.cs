using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Core.Models;

namespace Supermarket.Core.Repositories
{
    public interface IUsersRepository : IDisposable
    {
        IQueryable<UserProfile> GetUsers();
        UserProfile GetUser(int id);
        void AddUser(string userName, string password, string email, string firstName = "", string lastName = "");
        void DeleteUser(int id);
        void UpdateUser(int id, string email, string firstName, string lastName);
        int Save();
    }
}
