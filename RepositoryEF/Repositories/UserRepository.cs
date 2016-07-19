using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ViewModels;

namespace RepositoryEF.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly Database _dbContext;
        public UserRepository(ApplicationDbContext context, Database dbContext)
        {
            _context = context;
            _dbContext = dbContext;
        }

        public void Activate(string id)
        {
            var dbUser = Get(id);
            AssociateWithUserRole(id);
            dbUser.IsActive = true;
            
        }

        public void MarkAsActiveByEmail(string email)
        {
            var dbUser = _context.Users.Single(u => u.Email == email);
            dbUser.IsActive = true;
        }

        public void AssociateWithUserRole(string userId)
        {
            var roleId = _context.Roles.Single(r => r.Name == "User").Id;

            var sql = @"INSERT INTO[dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES(@userId, @roleId)";
            _context.Database.ExecuteSqlCommand(sql,
                new SqlParameter("@userId", userId),
                new SqlParameter("@roleId", roleId));
            
        }

        public void AssociateEmailWithUserRole(string email)
        {
            var roleId = _context.Roles.Single(r => r.Name == "User").Id;
            var userId = _context.Users.Single(u => u.Email == email).Id;
            var sql = @"INSERT INTO[dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES(@userId, @roleId)";
            _context.Database.ExecuteSqlCommand(sql,
                new SqlParameter("@userId", userId),
                new SqlParameter("@roleId", roleId));
        }


        public User Get(string id)
        {
            return _context.Users.Find(id);
        }

        public List<Domain.Entities.Task> GetTasksByUser(string userId)
        {
            var dbUser = Get(userId);
            var tasksByUser = _dbContext.Tasks.Where(t => t.UserId == dbUser.Id).ToList();
            return tasksByUser;
        }

        public List<UserAndTasksCountViewModel> GetUsersWithTasksCount()
        {
            var activeUsers = GetAllActive();
            var userAndTasksCount = new List<UserAndTasksCountViewModel>();
            foreach (var user in activeUsers)
            {
                var userView = new UserAndTasksCountViewModel()
                {
                    Id = user.Id,
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    TasksCount = _dbContext.Tasks.Count(t => t.UserId == user.Id)
                };
                userAndTasksCount.Add(userView);
            }

            return userAndTasksCount;
        }

        public bool IsUserActive(string email)
        {
            return _context.Users.Single(u => u.Email == email).IsActive;
        }

        public string GetDisplayNameFromEmail(string email)
        {
            return _context.Users.Single(u => u.Email == email).DisplayName;
        }


        public bool Exists(string email)
        {
            return _context.Users.Any(u => u.Email == email);

        }

        public bool IsUserInActive(string email)
        {
            return _context.Users.Single(u => u.Email == email).IsActive == false;
        }


        public IEnumerable<User> GetAllInActive()
        {
            return _context.Users.Where(u => u.IsActive == false).ToList();
        }

        public IEnumerable<User> GetAllActive()
        {
            return _context.Users.Where(u => u.IsActive).ToList();
        }
    }
}
