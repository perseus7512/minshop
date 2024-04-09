using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinShop_Backstage.Models;
using System.Data.Entity.Validation;

namespace MinShop_Backstage.Controllers
{
    public class homeController : Controller
    {
        mineshopEntities db = new mineshopEntities();
        // GET: home
        public ActionResult CMS()
        {
            var member = db.member.ToList();
            return View(member);
        }
        public ActionResult DeleteMember(string memberId)
        {
            var delete = db.member.Where(m => m.memberId == memberId).FirstOrDefault();
            db.member.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("CMS");
        }
        public ActionResult PMS()
        {
            var product = db.stock.ToList();
            return View(product);
        }
        public ActionResult release()
        {
            return View();
        }
        [HttpPost]
        public ActionResult release(string name, int inventory, int price)
        {
            stock product = new stock();
            product.name = name;
            product.inventory = inventory;
            product.price = price;
            product.releaseDate = DateTime.Now;
            db.stock.Add(product);
            db.SaveChanges();
            return RedirectToAction("PMS");
        }
        public ActionResult removal(int productId)
        {
            var product = db.stock.Where(m => m.productId == productId).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult removal(stock pro)
        {
            if (ModelState.IsValid)
            {
                var producct = db.stock.Where(m => m.productId == pro.productId).FirstOrDefault();
                producct.name = pro.name;
                producct.inventory = pro.inventory;
                producct.price = pro.price;
                producct.removalDate = pro.removalDate;
                db.SaveChanges();
                return RedirectToAction("PMS");
            }
            else
            {
                return View(pro);
            }
        }
        public ActionResult editProduct()
        {

            return View();
        }
        public ActionResult AMS()
        {

            return View();
        }
        public ActionResult CSMS()
        {

            return View();
        }
        public ActionResult OMS()
        {

            return View();
        }
        public ActionResult LMS()
        {

            return View();
        }
        public ActionResult create()
        {
            return View();
        }
    }
}