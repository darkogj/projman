using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using ProjectManagement.Models;
using ProjectManagement.Services;
using RepositoryEF;

namespace ProjectManagement.Controllers
{

    [Authorize(Roles = CustomRoles.Admin + "," + CustomRoles.User)]
    public class TasksController : Controller
    {

        private UnitOfWork _unitOfWork = new UnitOfWork(new RepositoryEF.Database(), new ApplicationDbContext());

        // GET: Tasks
        public ActionResult Index()
        {
            var tasks = _unitOfWork.Tasks.GetTasksInclusive();
            return View(tasks.ToList());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int id)
        {
            var task = _unitOfWork.Tasks.GetInclusive(id);
            
            return PartialView("_Details", task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(_unitOfWork.Projects.GetAllActive(), "ID", "Name");
            ViewBag.UserId = new SelectList(_unitOfWork.Users.GetAllActive(), "Id", "Email");
            return PartialView("_Create");
        }

        public ActionResult CreateForProject(int projectId)
        {

            ViewBag.UserId = new SelectList(_unitOfWork.Users.GetAllActive(), "Id", "Email");
            ViewBag.ProjectName = _unitOfWork.Projects.GetNameById(projectId);
            return View();
        }

        [HttpPost]
        public ActionResult CreateForProject(Task task)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Tasks.Add(task);
                _unitOfWork.Complete();
                return RedirectToAction("SeeTasks", "Projects", new {projectId = task.ProjectId});
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult CreateNewTask(Task task)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Tasks.Add(task);
                _unitOfWork.Complete();
                var tasks = _unitOfWork.Tasks.GetTasksInclusive();
                return PartialView("_TasksTable", tasks);
            }
            return View("index");
        }

        // POST: Tasks/Create
        //[HttpPost]
        //public ActionResult Create(Task task)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Tasks.Add(task);
        //        _unitOfWork.Complete();
        //        return View("index");
        //    }

        //    ViewBag.ProjectId = new SelectList(_unitOfWork.Projects.GetAllActive(), "ID", "Name", task.ProjectId);
        //    ViewBag.UserId = new SelectList(_unitOfWork.Users.GetAllActive(), "Id", "Email", task.UserId);
        //    return View("index");
        //}

        // GET: Tasks/Edit/5
        public ActionResult Edit(int id)
        {
            Task task = _unitOfWork.Tasks.GetInclusive(id);

            ViewBag.ProjectId = new SelectList(_unitOfWork.Projects.GetAllActive(), "ID", "Name", task.ProjectId);
            ViewBag.UserId = new SelectList(_unitOfWork.Users.GetAllActive(), "Id", "Email", task.UserId);
            ViewBag.CurrentStatus = task.Status;
            var selectList = TaskChooser.GetAvailableOptionsBasedOn(task.Status);
            ViewBag.AvailableStatusChanges = selectList;


            return PartialView("_Edit", task);
        }

        // POST: Tasks/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Task task)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Tasks.UpdateTask(task);
        //        _unitOfWork.Complete();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ProjectId = new SelectList(_unitOfWork.Projects.GetAllActive(), "ID", "Name", task.ProjectId);
        //    ViewBag.UserId = new SelectList(_unitOfWork.Users.GetAllInActive(), "Id", "Email", task.UserId);
        //    return View(task);
        //}

        [HttpPost]
        public ActionResult EditTask(Task task)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Tasks.UpdateTask(task);
                _unitOfWork.Complete();
                var tasks = _unitOfWork.Tasks.GetTasksInclusive();
                return PartialView("_TasksTable", tasks);
            }
            ViewBag.ProjectId = new SelectList(_unitOfWork.Projects.GetAllActive(), "ID", "Name", task.ProjectId);
            ViewBag.UserId = new SelectList(_unitOfWork.Users.GetAllInActive(), "Id", "Email", task.UserId);
            return View("index");
        }



        // GET: Tasks/Delete/5
        public ActionResult Delete(int id)
        {
            Task task = _unitOfWork.Tasks.GetInclusive(id);
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _unitOfWork.Tasks.Delete(id);
            _unitOfWork.Complete();
            return HttpNotFound();
        }

        [HttpPost]
        public void ChangeTaskStatusTo(int taskStatusId, int id)
        {
            switch (taskStatusId)
            {
                case 0:
                    _unitOfWork.Tasks.ChangeTaskStatusTo(TaskStatus.ToDo, id);
                    break;

                case 1:
                    _unitOfWork.Tasks.ChangeTaskStatusTo(TaskStatus.InProgress, id);
                    break;

                case 2:
                    _unitOfWork.Tasks.ChangeTaskStatusTo(TaskStatus.InTesting, id);
                    break;

                case 3:
                    _unitOfWork.Tasks.ChangeTaskStatusTo(TaskStatus.Done, id);
                    break;
            }
            _unitOfWork.Complete();
        }

        public ActionResult Mine()
        {
            var loggedInUserId = User.Identity.GetUserId();
            var tasksByUser = _unitOfWork.Tasks.GetAllTasksByUser(loggedInUserId);
            TasksByStatus tasksByStatus = new TasksByStatus(tasksByUser);
            return View(tasksByStatus);

        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
