using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Supermarket.Core.Repositories;
using Supermarket.Core.Models;
using WebMatrix.WebData;

namespace Supermarket.Main.DataInfrastructure
{
    public class UsersRepository : IUsersRepository
    {
        private readonly SupermarketDB _context = new SupermarketDB();

        public void AddUser(string userName, string password, string email, string firstName = "", string lastName = "")
        {
            WebSecurity.CreateUserAndAccount(userName, password, new { Email = email, FirstName = firstName, LastName = lastName });
        }

        public IEnumerable<UserProfile> GetUsers()
        {
            var users = _context.UserProfiles;
            return users.AsEnumerable();
        }

        public UserProfile GetUser(int id)
        {
            var user = _context.UserProfiles.SingleOrDefault(u => u.UserId == id);
            return user;
        }

        public void DeleteUser(int id)
        {
            var user = _context.UserProfiles.SingleOrDefault(u => u.UserId == id);
            if (user == null)
            {
                throw new InvalidOperationException("The user for deletion does not exist");
            }
            _context.UserProfiles.Remove(user);
        }

        public void UpdateUser(int id, string email, string firstName, string lastName)
        {
            var user = _context.UserProfiles.SingleOrDefault(u => u.UserId == id);
            if (user == null)
            {
                throw new InvalidOperationException("The user for edit does not exist");
            }
            user.Email = email;
            user.FirstName = firstName;
            user.LastName = lastName;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}