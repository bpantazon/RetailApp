using Newtonsoft.Json;
using RetailApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace RetailApp.Controllers
{
    public class AdminController : Controller
    {
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
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
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

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
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
