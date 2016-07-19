using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using ViewModels;

namespace Domain.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        Project GetWithCustomer(int id);
        void UpdateProject(Project project);
        string GetNameById(int id);
        int ActiveProjectsCount();
        List<Project> GetAllActiveWithPagination(int page, int pageSize);
        List<ProjectAndTaskCountViewModel> GetNumOfTasksPerProject();
    }
}
