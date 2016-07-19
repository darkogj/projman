using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITaskRepository
    {
        IEnumerable<Task> GetAll();
        Task Get(int id);


        void Add(Task item);

        void Delete(int id);
        List<TaskComment> GetComments(int taskId);
        List<Task> GetTasksInclusive(int projectId=-1);
        Task GetInclusive(int id);
        void UpdateTask(Task task);
        void ChangeTaskStatusTo(TaskStatus status, int taskId);
        List<Task> GetAllTasksByUser(string userId);
    }
}
