using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Domain.Entities;
using Domain.Interfaces;

namespace RepositoryEF.Repositories
{
    public class TaskRepository : ITaskRepository
    {

        private readonly Database _context;
        private readonly ApplicationDbContext _identityContext;
        public TaskRepository(Database context, ApplicationDbContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
        }

        public void Add(Task item)
        {
            _context.Tasks.Add(item);

        }

        public void Delete(int id)
        {
            var dbTask = Get(id);
            if (dbTask != null) _context.Tasks.Remove(dbTask);
        }

        public Task Get(int id)
        {
            return _context.Tasks.Find(id);
        }

        public IEnumerable<Task> GetAll()
        {
            return _context.Tasks.ToList();
        }
        
        public List<TaskComment> GetComments(int taskId)
        {
            var dbTask = Get(taskId);
            return dbTask.Comments.ToList();
        }

        public List<Task> GetTasksInclusive(int projectId = -1)
        {
            var tasksWithProjects = new List<Task>();

            if (projectId == -1)
            {
                tasksWithProjects = _context.Tasks.Include(t => t.Project).ToList();
            }
            else
            {
                tasksWithProjects = _context.Tasks.Where(t => t.ProjectId == projectId).Include(t => t.Project).Include(t => t.Comments).ToList();
            }
            
            var tasksWithUsersAndProjects = new List<Task>();

            for (int i = 0; i < tasksWithProjects.Count; i++)
            {
                var t = tasksWithProjects[i];
                var appUser = _identityContext.Users.Find(t.UserId);
                t.User = appUser;
                tasksWithUsersAndProjects.Add(t);
            }

            return tasksWithUsersAndProjects;
        }

        public Task GetInclusive(int id)
        {
            
            var dbTask = _context.Tasks.Find(id);
            _context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            _context.Entry(dbTask).Reference(t => t.Project).Load();
            _context.Entry(dbTask).Collection(t => t.Comments).Load();
            var appUser = _identityContext.Users.Find(dbTask.UserId);
            dbTask.User = appUser;
            

            return dbTask;
        }

        public void UpdateTask(Task task)
        {
            _context.Entry(task).State = EntityState.Modified;
        }

        public void ChangeTaskStatusTo(TaskStatus status, int taskId)
        {
            var dbTask = _context.Tasks.Find(taskId);
            dbTask.Status = status;
        }

        public List<Task> GetAllTasksByUser(string userId)
        {
            return _context.Tasks.Where(t => t.UserId == userId).ToList();
        }
    }
}
