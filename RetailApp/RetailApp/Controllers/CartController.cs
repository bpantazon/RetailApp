using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetailApp.Models;

namespace RetailApp.Controllers
{
    public class CartController : Controller
    {
        ApplicationDbContext db;

        public CartController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

    }
}