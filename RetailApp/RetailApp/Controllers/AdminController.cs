using Newtonsoft.Json;
using RetailApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Text;
using System.Net.Http.Headers;

namespace RetailApp.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db;

        public AdminController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Admin
        public ActionResult AdminHome()
        {
            try
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
                        newCatalog.Product_Id = item.Product_Id;
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
            catch
            {
                return View();
            }
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
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

        // GET: Admin/Create
        public ActionResult Create()
        {
            Catalog catalog = new Catalog();
            return View(catalog);
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(Catalog catalog)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:54150/api/Product");
                    var response = client.PostAsync("", new StringContent(new JavaScriptSerializer().Serialize(catalog), Encoding.UTF8, "application/json")).Result;
                    response.EnsureSuccessStatusCode();
                }
                return RedirectToAction("AdminHome");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
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

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Catalog foundCatalog)
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

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
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

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Catalog foundCatalog)
        {            
            // TODO: Add delete logic here
            try
            {
                string idString = id.ToString();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:54150/api/Product/" + idString);
                    var response = client.DeleteAsync("").Result;
                    response.EnsureSuccessStatusCode();
                }
                    return RedirectToAction("AdminHome");
            }
                catch
                {
                    return View();
                }
           
            

        }
    }
}
