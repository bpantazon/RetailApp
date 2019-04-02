using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetailApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("Manager"))
            {
                return RedirectToAction("ManagerHome", "Manager");
            }
            else if (User.IsInRole("Employee"))
            {
                return RedirectToAction("EmployeeHome", "Employee");
            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("AdminHome", "Admin");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}