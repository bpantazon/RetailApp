﻿using Microsoft.AspNet.Identity;
using MailKit.Net.Smtp;
using MimeKit;
using RetailApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stripe;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

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
            var employeeSchedules = db.Schedules.Where(s => s.EmployeeId == emp.EmployeeId).ToList().OrderBy(e => e.StartTime);
            
            return View(employeeSchedules);         
        }

        public ActionResult MySales()
        {
            return View();
        }

        public ActionResult VisualizeData()
        {
            return Json(EmployeeResults(), JsonRequestBehavior.AllowGet);
        }

        public List<EmployeeSale> EmployeeResults()
        {
            List<EmployeeSale> employeeSales = new List<EmployeeSale>();
            var user = User.Identity.GetUserId();
            var currentEmployee = db.Employees.Where(e => e.ApplicationUserId == user).SingleOrDefault();
            employeeSales = db.EmployeeSales.Where(s => s.EmployeeId == currentEmployee.EmployeeId).ToList();

            return employeeSales;
        }

        // GET: Employee/Details/5
        public ActionResult InventoryDetails(int id)
        {
            try
            {
                string idString = id.ToString();
                Catalog foundCatalog = new Catalog();
                using (var client = new HttpClient(new HttpClientHandler
                { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
                {
                    client.BaseAddress = new Uri("http://localhost:54150/api/Product/" + idString);
                    HttpResponseMessage response = client.GetAsync("").Result;
                    response.EnsureSuccessStatusCode();
                    var result = response.Content.ReadAsStringAsync().Result;
                    foundCatalog = JsonConvert.DeserializeObject<Catalog>(result);

                    return View(foundCatalog);
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Catalog()
        {
            IList<Catalog> catalogs = new List<Catalog>();
            List<Catalog> catalogItems = new List<Catalog>();
            using (var client = new HttpClient(new HttpClientHandler
            { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("http://localhost:54150/api/Product");
                HttpResponseMessage response = client.GetAsync("").Result;
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;
                catalogs = JsonConvert.DeserializeObject<List<Catalog>>(result);

                foreach (var item in catalogs)
                {
                    Catalog newCatalog = new Catalog();
                   
                    newCatalog.BrandName = item.BrandName;
                    newCatalog.ModelName = item.ModelName;
                    newCatalog.Price = item.Price;
                    newCatalog.SKU = item.SKU;
                    newCatalog.Category = item.Category;
                    newCatalog.Feature1 = item.Feature1;
                    newCatalog.Feature2 = item.Feature2;
                    newCatalog.Summary = item.Summary;
                    catalogItems.Add(newCatalog);                  
                }
            }
                return View(catalogItems);
        }


        // GET: Employee/Create
        public ActionResult Create()
        {
            Employee employee = new Employee();
            return View("Create", employee);           
        }

        public ActionResult Inventory()
        {
            var inventory = db.Inventories.ToList().OrderBy(i => i.BrandName);
            return View(inventory);
        }
        
        public ActionResult CreateAppointment()
        {
            Appointment appointment = new Appointment();
            return View("CreateAppointment", appointment);
        }

        public ActionResult EditAppointment(int id)
        {
            var appointment = db.Appointments.Where(a => a.AppointmentId == id).SingleOrDefault();
            return View(appointment);
        }
        [HttpPost]
        public ActionResult EditAppointment(int id, Appointment appointment)
        {
            var editedAppt = db.Appointments.Where(e => e.AppointmentId == id).SingleOrDefault();
            editedAppt.FirstName = appointment.FirstName;
            editedAppt.LastName = appointment.LastName;
            editedAppt.AppointmentDate = appointment.AppointmentDate;
            editedAppt.Email = appointment.Email;
            editedAppt.PhoneNumber = appointment.PhoneNumber;
            editedAppt.Description = appointment.Description;
            db.SaveChanges();
            return RedirectToAction("Appointments");
        }
        public ActionResult AppointmentDetails (int id)
        {
            var appointment = db.Appointments.Where(a => a.AppointmentId == id).SingleOrDefault();
            return View(appointment);
        }

        public ActionResult DeleteAppointment(int id)
        {
            var appointment = db.Appointments.Where(a => a.AppointmentId == id).SingleOrDefault();
            return View(appointment);
        }

        [HttpPost]
        public ActionResult DeleteAppointment(int id, Appointment appointment)
        {            
            try
            {
                var deletedAppt = db.Appointments.Where(d => d.AppointmentId == id).SingleOrDefault();
                db.Appointments.Remove(deletedAppt);
                db.SaveChanges();

                return RedirectToAction("Appointments");
            }
            catch
            {
                return View();
            }            
        }
        public ActionResult CreateCharge(int id)
        {
            Models.Inventory inventory = db.Inventories.Where(i => i.InventoryId == id).SingleOrDefault();
            if (inventory.Count > 0)
            {
                return View(inventory);
            }
            else
            {           
                return RedirectToAction("Inventory");
            }
            
        }
        public ActionResult Appointments()
        {
            var appointments = db.Appointments.ToList();
            var appointmentsByDate = appointments.OrderBy(a => a.AppointmentDate);
            return View(appointmentsByDate);
        }

        [HttpPost]
        public ActionResult CreateCharge(string stripeToken, Models.Inventory inventory)
        {
            var foundProduct = db.Inventories.Where(i => i.InventoryId == inventory.InventoryId).SingleOrDefault();
            long cost = (long)Convert.ToDouble(foundProduct.Price);
            try
            {
                StripeConfiguration.SetApiKey("sk_test_BnYYnTyy5klJvy4gt3AyaMoI");

                var options = new ChargeCreateOptions
                {
                    Amount = cost * 100,
                    Currency = "usd",
                    Description = "Charge for retailmanagertest@gmail.com",
                    SourceId = stripeToken // obtained with Stripe.js,
                };
                var service = new ChargeService();
                Charge charge = service.Create(options);
                var model = new ChargeViewModel();               
                model.ChargeId = charge.Id;

                foundProduct.Count--;
                foundProduct.Sold++;
                
                var currentUser = User.Identity.GetUserId();
                var currentEmployee = db.Employees.Where(e => e.ApplicationUserId == currentUser).SingleOrDefault();
                var checkSale = db.EmployeeSales.Where(s => s.Model == foundProduct.ModelName && s.EmployeeId == currentEmployee.EmployeeId).SingleOrDefault();
                if (checkSale == null)
                {
                    var newEmpSale = new EmployeeSale
                    {
                        EmployeeId = currentEmployee.EmployeeId,
                        Model = foundProduct.ModelName,
                        NumberSold = 1
                    };
                    db.EmployeeSales.Add(newEmpSale);
                    db.SaveChanges();
                }
                else
                {
                    checkSale.NumberSold++;
                    db.SaveChanges();
                }
              
                return View("OrderStatus", model);
            }
            catch
            {
                return View();
            }
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
        [HttpPost]
        public ActionResult CreateAppointment(Appointment appointment)
        {
            try
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();


                var message = new MimeMessage();
                message.From.Add(new MailboxAddress($"{appointment.FirstName}", "retailManagerTest@gmail.com"));
                message.To.Add(new MailboxAddress($"{appointment.FirstName}, {appointment.LastName}", "retailManagerTest@gmail.com")); //sending email to the test address for testing purposes
                message.Subject = "Your Appointment";

                message.Body = new TextPart("plain")
                {
                    Text =
                $@"Hello {appointment.FirstName},

                    This email is confirming your appointment on {appointment.AppointmentDate}. We'll see you soon!

                From, 

                The Retail Team"
                };
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("retailManagerTest", "Ret@il1!");
                    client.Send(message);
                    client.Disconnect(true);
                }
                return RedirectToAction("Appointments");
            }
            catch
            {
                return View();
            }
            
        }
        public ActionResult AddInventory()
        {
            Models.Inventory inventory = new Models.Inventory();

            return View(inventory);
        }

        [HttpPost]
        public ActionResult AddInventory(Models.Inventory inventory)
        {
            try
            {
                var newProduct = new Models.Inventory
                {
                    BrandName = inventory.BrandName,
                    ModelName = inventory.ModelName,
                    SKU = inventory.SKU,
                    Price = inventory.Price,
                    Count = inventory.Count,
                    Category = inventory.Category,
                    Sold = 0
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
        public ActionResult EditInventory(int id, Models.Inventory inventory)
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
