using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
using ViewModels;

namespace RepositoryEF.Repositories
{


    public class ProjectRepository : IProjectRepository
    {
        private readonly Database _context;
        public ProjectRepository(Database context)
        {
            _context = context;
        }

        public void Add(Project item)
        {
            _context.Projects.Add(item);
            item.DateCreated = DateTime.UtcNow;

        }

        public List<ProjectAndTaskCountViewModel> GetNumOfTasksPerProject()
        {
            return _context.Projects.Include(p => p.Tasks)
                .Select(p => new ProjectAndTaskCountViewModel() {TaskCount = p.Tasks.Count, ProjectName = p.Name})
                .ToList();

        }

        public Project Get(int id)
        {
            return _context.Projects.Find(id);
        }

        public string GetNameById(int id)
        {
            return _context.Projects.Single(p => p.ID == id).Name;
        }

        public Project GetWithCustomer(int id)
        {
            var dbProject = _context.Projects.Find(id);
            _context.Entry(dbProject).Reference(p => p.Customer).Load();
            return dbProject;
        }



        public IEnumerable<Project> GetAllActive()
        {
            return _context.Projects.Where(p => p.IsActive).Include(p => p.Customer).ToList();
        }

        public void Deactivate(int id)
        {
            var dbProject = Get(id);
            if (dbProject != null) dbProject.IsActive = false;
        }

        public bool Exists(int id)
        {
            var dbProject = _context.Projects.Find(id);
            if (dbProject == null) return false;
            return true;
        }

        public void UpdateProject(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
        }

        public int ActiveProjectsCount()
        {
            return _context.Projects.Count(p => p.IsActive);
        }

        public List<Project> GetAllActiveWithPagination(int page, int pageSize)
        {
            return _context.Projects.Where(p => p.IsActive)
                .OrderBy(p => p.ID)
                .Skip(page*pageSize)
                .Take(pageSize)
                .Include(p => p.Customer)
                .ToList();
        }
    }
}
