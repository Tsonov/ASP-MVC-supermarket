using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Supermarket.Core;
using Supermarket.Core.Models;
using WebMatrix.WebData;

namespace Supermarket.Main.DataInfrastructure
{
    public class UsersContext : DbContext, IUsersRepository
    {
        private DbSet<UserProfile> _userProfiles { get; set; }

        public UsersContext()
            : base("DefaultConnection")
        {
           // _userProfiles = 
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<UsersContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public IQueryable<UserProfile> GetUsers()
        {
            return this._userProfiles;
        }

        public UserProfile GetUser(int id)
        {
            var result = _userProfiles.SingleOrDefault(user => user.UserId == id);
            if (result == null)
            {
                //TODO
            }
            return result;
        }

        public void AddUser(UserProfile userInfo, string password)
        {
            WebSecurity.CreateUserAndAccount(userInfo.UserName, password,
                new { Email = userInfo.Email, FirstName = userInfo.FirstName, LastName = userInfo.LastName });
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(UserProfile user)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            return base.SaveChanges();
        }
    }
}