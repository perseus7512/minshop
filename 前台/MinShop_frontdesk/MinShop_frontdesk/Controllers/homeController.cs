using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinShop_frontdesk.Models;

namespace MinShop_frontdesk.Controllers
{
    public class homeController : Controller
    {
        mineshopEntities db = new mineshopEntities();
        // GET: home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShoppingCar()
        {
            return View();
        }
        public ActionResult Users()
        {
            return View();
        }
        public ActionResult Tent()
        {
            return View();
        }
        public ActionResult Bedding()
        {
            return View();
        }
        public ActionResult LipGloss()
        {
            return View();
        }
        public ActionResult Order()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
    }
}