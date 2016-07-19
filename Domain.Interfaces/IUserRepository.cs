using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using ViewModels;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        void Activate(string id);
        void MarkAsActiveByEmail(string email);
        void AssociateWithUserRole(string userId);
        void AssociateEmailWithUserRole(string email);
        User Get(string id);
        List<Domain.Entities.Task> GetTasksByUser(string userId);
        List<UserAndTasksCountViewModel> GetUsersWithTasksCount();
        bool IsUserActive(string email);
        string GetDisplayNameFromEmail(string email);
        bool Exists(string email);
        bool IsUserInActive(string email);
        IEnumerable<User> GetAllInActive();
        IEnumerable<User> GetAllActive();
    }
}
