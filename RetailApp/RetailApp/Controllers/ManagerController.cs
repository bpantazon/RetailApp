using Microsoft.AspNet.Identity;
using RetailApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetailApp.Controllers
{
    public class ManagerController : Controller
    {
        ApplicationDbContext db;

        public ManagerController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Manager
        public ActionResult ManagerHome()
        {
            //default view = total sales
            return View();
        }
        public ActionResult AllSchedules()
        {
            var allSchedules = db.Schedules.ToList();
            return View(allSchedules);
        }

        // GET: Manager/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        public ActionResult AddProduct()
        {
            Inventory inventory = new Inventory();

            return View(inventory);
        }

        [HttpPost]
        ActionResult AddProduct(Inventory inventory)
        {
            try
            {
                var newProduct = new Inventory
                {
                    BrandName = inventory.BrandName,
                    ModelName = inventory.ModelName,
                    SKU = inventory.SKU,
                    Price = inventory.Price,
                    Count = inventory.Count,
                    Category = inventory.Category

                };
                db.Inventories.Add(newProduct);
                db.SaveChanges();
                return RedirectToAction("Inventory");
            }
            catch
            {
                return View();
            }
           
        }
        public ActionResult CreateSchedule()
        {
            Schedule schedule = new Schedule();
            return View("CreateSchedule", schedule);
        }

        [HttpPost]
        public ActionResult CreateSchedule(Schedule schedule)
        {
            try
            {
                var scheduledEmployee = db.Employees.Where(e => e.FirstName == schedule.FirstName && e.LastName == schedule.LastName).SingleOrDefault();
                var newSchedule = new Schedule
                {
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime,
                    FirstName = schedule.FirstName,
                    LastName = schedule.LastName,
                    EmployeeId = scheduledEmployee.EmployeeId
                };
                    
                db.Schedules.Add(newSchedule);
                db.SaveChanges();
                return RedirectToAction("AllSchedules");
            }
            catch
            {
                return View();
            }
        }

        // GET: Manager/Create
        public ActionResult CreateManager()
        {
            Manager manager = new Manager();

            return View("CreateManager", manager);
        }
        // POST: Manager/Create
        [HttpPost]
        public ActionResult CreateManager(Manager manager)
        {
            try
            {
                // TODO: Add insert logic here
                db.Managers.Add(manager);
                manager.ApplicationUserId = User.Identity.GetUserId();
                db.SaveChanges();
                return RedirectToAction("ManagerHome");
            }
            catch
            {
                return View();
            }
        }

        // GET: Manager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CreateEmployee()
        {
            Employee employee = new Employee();
            return View("CreateEmployee", employee);
        }

        // POST: Manager/CreateEmployee
        [HttpPost]
        public ActionResult CreateEmployee(Employee employee)
        {
            try
            {
                // TODO: Add insert logic here
                db.Employees.Add(employee);
                employee.ApplicationUserId = User.Identity.GetUserId();
                db.SaveChanges();
                return RedirectToAction("ManagerHome");
            }
            catch
            {
                return View();
            }
        }
        // GET: Manager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
