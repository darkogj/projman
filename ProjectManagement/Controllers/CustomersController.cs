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
    [Authorize(Roles = CustomRoles.Admin)]
    public class CustomersController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new Database(), new ApplicationDbContext());
        // GET: Customers
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Customer customer)
        {

            _unitOfWork.Customers.Add(customer);
            _unitOfWork.Complete();
            return RedirectToAction("AddConfirmation");
        }

        public ActionResult AddConfirmation()
        {
            return View();
        }
    }
}