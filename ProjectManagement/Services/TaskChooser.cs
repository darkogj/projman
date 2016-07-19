using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;

namespace ProjectManagement.Services
{
    public class TaskChooser
    {
        public static IEnumerable<SelectListItem> GetAvailableOptionsBasedOn(TaskStatus taskStatus)
        {
            var selectList = Enum.GetValues(typeof(TaskStatus))
                .Cast<TaskStatus>();

            switch (taskStatus)
            {
                case (TaskStatus.ToDo):

                    selectList = selectList.Where(t => t == TaskStatus.ToDo ||t == TaskStatus.InProgress || t == TaskStatus.InTesting);
                    break;

                case (TaskStatus.InProgress):
                    selectList = selectList.Where(t => t == TaskStatus.InProgress ||t == TaskStatus.ToDo || t == TaskStatus.InTesting || t == TaskStatus.Done);
                    break;

                case (TaskStatus.InTesting):
                    selectList = selectList.Where(t => t == TaskStatus.InTesting || t == TaskStatus.ToDo || t == TaskStatus.Done);
                    break;

                case (TaskStatus.Done):
                    selectList = selectList.Where(t => t == TaskStatus.Done);
                    break;

            }

            return selectList.Select(e => new SelectListItem
            {
                Value = ((int)e).ToString(),
                Text = e.ToString()
            });
        }
    }
}