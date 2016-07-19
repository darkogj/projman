using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace ProjectManagement.Models
{
    public class TasksByStatus
    {
        public List<Task> ToDo { get; set; }
        public List<Task> InProgress { get; set; }
        public List<Task> InTesting { get; set; }
        public List<Task> Done { get; set; }

        public TasksByStatus(List<Task> tasksForProject)
        {
            ToDo = tasksForProject.Where(ab => ab.Status == TaskStatus.ToDo).ToList();
            InProgress = tasksForProject.Where(ab => ab.Status == TaskStatus.InProgress).ToList();
            InTesting = tasksForProject.Where(ab => ab.Status == TaskStatus.InTesting).ToList();
            Done = tasksForProject.Where(ab => ab.Status == TaskStatus.Done).ToList();
        }
    }
}