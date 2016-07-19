using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using ProjectManagement.Models;
using RepositoryEF;

namespace ProjectManagement.Controllers
{
   // [Authorize(Roles = CustomRoles.Admin + "," + CustomRoles.User)]
    public class ReportsController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new Database(), new ApplicationDbContext());
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UsersAndTasksCount()
        {
            var activeUsers = _unitOfWork.Users.GetUsersWithTasksCount();
            return View(activeUsers);

        }

        public ActionResult TasksPerProject()
        {
            var projectsAndTaskCount = _unitOfWork.Projects.GetNumOfTasksPerProject();
            return View(projectsAndTaskCount);
        }


    }
}