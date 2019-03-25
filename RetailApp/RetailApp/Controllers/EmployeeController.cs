using Microsoft.AspNet.Identity;
using RetailApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetailApp.Controllers
{
    public class EmployeeController : Controller
    {
        ApplicationDbContext db;

        public EmployeeController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Employee
        public ActionResult EmployeeHome()
        {
            var user = User.Identity.GetUserId();           
            var emp = db.Employees.Where(e => e.ApplicationUserId == user).Single();
            var employeeSchedules = db.Schedules.Where(s => s.EmployeeId == emp.EmployeeId).ToList();
            return View(employeeSchedules);         
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            Employee employee = new Employee();
            return View("Create", employee);           
        }

        public ActionResult Inventory()
        {
            var inventory = db.Inventories.ToList();
            return View(inventory);
        }
        
        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                // TODO: Add insert logic here
                db.Employees.Add(employee);
                employee.ApplicationUserId = User.Identity.GetUserId();
                db.SaveChanges();
                return RedirectToAction("EmployeeHome");
            }
            catch
            {
                return View();
            }
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


        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
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

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
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
