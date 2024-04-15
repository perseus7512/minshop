using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinShop_Backstage.Models;

namespace MinShop_Backstage.Controllers
{
    public class homeController : Controller
    {
        mineshopEntities db = new mineshopEntities();
        // GET: home
        public ActionResult CMS()
        {
            return View();
        }
    }
}