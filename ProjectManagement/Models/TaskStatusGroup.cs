using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace ProjectManagement.Models
{
    public class TaskStatusGroup
    {
        public Task ToDo { get; set; }
        public Task InProgress { get; set; }
        public Task InTesting { get; set; }
        public Task Done { get; set; }

        public TaskStatusGroup(Task toDo, Task inProgress, Task inTesting, Task done)
        {
            ToDo = toDo;
            InProgress = inProgress;
            InTesting = inTesting;
            Done = done;
        }
    }
}