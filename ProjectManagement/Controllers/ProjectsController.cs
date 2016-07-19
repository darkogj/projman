using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using ProjectManagement.Models;
using ProjectManagement.Services;
using RepositoryEF;

namespace ProjectManagement.Controllers
{
    
    public class ProjectsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new RepositoryEF.Database(), new ApplicationDbContext());

        [Authorize(Roles = CustomRoles.Admin)]
        public ActionResult IndexWithCreate(int page = 0, int pageSize = 20)
        {
            var count = unitOfWork.Projects.ActiveProjectsCount();
            var projects = unitOfWork.Projects.GetAllActiveWithPagination(page, pageSize);
            ViewBag.MaxPage = (count / pageSize) - (count % pageSize == 0 ? 1 : 0);
            ViewBag.Page = page;
            return View(projects.ToList());
        }

        [Authorize(Roles = CustomRoles.Admin + "," + CustomRoles.User)]
        public ActionResult Index(int page = 0, int pageSize = 20)
        {
            var count = unitOfWork.Projects.ActiveProjectsCount();
            var projects = unitOfWork.Projects.GetAllActiveWithPagination(page, pageSize);
            ViewBag.MaxPage = (count / pageSize) - (count % pageSize == 0 ? 1 : 0);
            ViewBag.Page = page;
            return View(projects.ToList());
        }

        [Authorize(Roles = CustomRoles.Admin + "," + CustomRoles.User)]
        public ActionResult Details(int id)
        {

            Project project = unitOfWork.Projects.GetWithCustomer(id);

            return PartialView("_Details", project);
        }

        [Authorize(Roles = CustomRoles.Admin)]
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(unitOfWork.Customers.GetAllActive(), "ID", "Name");
            return PartialView("_Create");
        }
        
        [HttpPost]
        [Authorize(Roles = CustomRoles.Admin)]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                project.DateCreated = DateTime.Now;
                unitOfWork.Projects.Add(project);
                unitOfWork.Complete();
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CustomerId = new SelectList(db.Customers, "ID", "Name", project.CustomerId);
            return View(project);
        }
        
        [HttpPost]
        [Authorize(Roles = CustomRoles.Admin)]
        public ActionResult CreateNewProject(Project project)
        {
            if (ModelState.IsValid)
            {
                project.DateCreated = DateTime.Now;
                unitOfWork.Projects.Add(project);
                unitOfWork.Complete();
                var projects = unitOfWork.Projects.GetAllActiveWithPagination(0, 20);
                return PartialView("_ProjectsTable", projects);
            }
            return View("index");
        }

        [Authorize(Roles = CustomRoles.Admin + "," + CustomRoles.User)]
        // GET: Projects/Edit/5
        public ActionResult Edit(int id)
        {

            ViewBag.CustomerId = new SelectList(unitOfWork.Customers.GetAllActive(), "ID", "Name");
            var dbProject = unitOfWork.Projects.GetWithCustomer(id);
            return PartialView("_Edit", dbProject);
            //return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = CustomRoles.Admin + "," + CustomRoles.User)]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
               unitOfWork.Projects.UpdateProject(project);
               unitOfWork.Complete();
                var projects = unitOfWork.Projects.GetAllActiveWithPagination(0, 20);
                return PartialView("_ProjectsTable", projects);
            }
            ViewBag.CustomerId = new SelectList(unitOfWork.Customers.GetAllActive(), "ID", "Name");
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = CustomRoles.Admin + "," + CustomRoles.User)]
        public ActionResult Delete(int id)
        {

            Project project = unitOfWork.Projects.GetWithCustomer(id);
            return View(project);
        }

        // POST: Projects/Delete/5
        [Authorize(Roles = CustomRoles.Admin + "," + CustomRoles.User)]
        [HttpPost]
        public void DeleteConfirmed(int id)
        {
            unitOfWork.Projects.Deactivate(id);
            unitOfWork.Complete();
        }

        [Authorize(Roles = CustomRoles.Admin + "," + CustomRoles.User)]
        public ActionResult SeeTasks(int projectId)
        {
            ViewBag.ProjectId = projectId;
            var tasksForProject = unitOfWork.Tasks.GetTasksInclusive(projectId);

            TasksByStatus tasksByStatus = new TasksByStatus(tasksForProject);


            return View(tasksByStatus);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        //db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
