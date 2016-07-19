using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RepositoryEF;

namespace ProjectManagement.Services
{
    public class UserService
    {
        public static void CreateUser(string email, string displayName, string password)
        {
            var store = new UserStore<User>(new ApplicationDbContext());
            var manager = new ApplicationUserManager(store);
            var user = new User { UserName = email, Email = email, DisplayName = displayName, IsActive = false, DateCreated = DateTime.Now };
            manager.Create(user, password);
        }
    }
}