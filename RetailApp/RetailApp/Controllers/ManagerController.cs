using Microsoft.AspNet.Identity;
using RetailApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace RetailApp.Controllers
{
    public class ManagerController : Controller
    {
        ApplicationDbContext db;
       
        public ManagerController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult ManagerHome()
        {
            IList<Employee> employeeList = new List<Employee>();
            foreach (var emp in db.Employees)
            {
                employeeList.Add(emp);
            }
            return View(employeeList);
        }
    
        public ActionResult EmployeeSales(Employee employee)
        {
            int id = employee.EmployeeId;
            SalesForEmployee(id);

            return View(employee);
        }
        public List<EmployeeSale> SalesForEmployee(int id)
        {
            //List<EmployeeSale> employeeSales = new List<EmployeeSale>();
            var employee = db.Employees.Where(e => e.EmployeeId == id).SingleOrDefault();
            var salesForEmp = db.EmployeeSales.Where(s => s.EmployeeId == employee.EmployeeId).ToList();
            return salesForEmp;
        }
        public ActionResult VisualizeEmpData(int id)
        {
            return Json(SalesForEmployee(id), JsonRequestBehavior.AllowGet);
        }

        // GET: Manager
        public ActionResult VisualizeData()
        {
            return Json(InventoryResults(), JsonRequestBehavior.AllowGet);
        }
    
        public List<Inventory> InventoryResults()
        {
            List<Inventory> salesResults = new List<Inventory>();

            salesResults = db.Inventories.ToList();

            return salesResults;
        }
        public ActionResult Inventory()
        {
            var allInventory = db.Inventories.ToList();
            return View(allInventory);
        }
        public ActionResult AllSchedules()
        {
            var allSchedules = db.Schedules.ToList();
            return View(allSchedules);
        }

        public ActionResult EditInventory(int id)
        {
            var editedInventory = db.Inventories.Where(i => i.InventoryId == id).SingleOrDefault();
            return View(editedInventory);
        }

        [HttpPost]
        public ActionResult EditInventory(int id, Inventory inventory)
        {
            var editedInventory = db.Inventories.Where(i => i.InventoryId == id).SingleOrDefault();
            editedInventory.BrandName = inventory.BrandName;
            editedInventory.ModelName = inventory.ModelName;
            editedInventory.SKU = inventory.SKU;
            editedInventory.Price = inventory.Price;
            editedInventory.Count = inventory.Count;
            editedInventory.Category = inventory.Category;
            db.SaveChanges();
            return RedirectToAction("Inventory");
        }
        // GET: Manager/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        public ActionResult AddInventory()
        {
            Inventory inventory = new Inventory();

            return View(inventory);
        }

        [HttpPost]
        public ActionResult AddInventory(Inventory inventory)
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
