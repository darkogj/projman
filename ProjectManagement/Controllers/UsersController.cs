using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectManagement.Models;
using ProjectManagement.Services;
using RepositoryEF;

namespace ProjectManagement.Controllers
{

    [Authorize(Roles = CustomRoles.Admin)]
    public class UsersController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new RepositoryEF.Database(), new ApplicationDbContext());

        // GET: Users
        public ActionResult Activate()
        {
            return View(_unitOfWork.Users.GetAllInActive());
        }

        public ActionResult ActivateAction(string id)
        {
            _unitOfWork.Users.Activate(id);
            _unitOfWork.Complete();

            return RedirectToAction("ActivateConfirmation");

        }

        public ActionResult ActivateConfirmation()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(RegisterViewModel model)
        {
            UserService.CreateUser(model.Email, model.DisplayName, model.Password);
            if (model.IsActivated)
            {
                _unitOfWork.Users.AssociateEmailWithUserRole(model.Email);
                _unitOfWork.Users.MarkAsActiveByEmail(model.Email);
                _unitOfWork.Complete();
            }
            return RedirectToAction("AddConfirmation");
        }

        public ActionResult AddConfirmation()
        {
            return View();
        }
    }
}
