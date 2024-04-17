using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinShop_frontdesk.Models;
using System.Data.Entity.Validation;

namespace MinShop_frontdesk.Controllers
{
    public class homeController : Controller
    {
        mineshopEntities db = new mineshopEntities();
        // GET: home
        public ActionResult Index()
        {
            var stocks = db.stock.ToList();
            if (Session["Member"] == null)
            {
                return View("Index", "_Layout",stocks);
            }
            else
            {
                return View("Index", "_Layout_Login", stocks);
            }
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
        [HttpPost]
        public ActionResult Login(string email,string pass)
        {
            var me = db.member.Where(m => m.email == email && m.password == pass).FirstOrDefault();
            if (me == null)
            {
                ViewBag.Message = "帳密錯誤,登入失敗";
                return View();
            }
            else
            {
                Session["Member"] = me;
                Session["MemberName"] = me.name;
                Session["Welcome"] = me.name + "-歡迎光臨";
                return RedirectToAction("index");
            }
        }
        public ActionResult register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult register(string name, string email, string pass1)
        {
            member m = new member();
            for(int i = 0; i < 10; i++)
            {
                m.memberId = GenerateSerialNumber();
            }
            m.name = name;
            m.email = email;
            m.password = pass1;
            db.member.Add(m);
            db.SaveChanges();
            return RedirectToAction("Login");
        }

        public static string GenerateSerialNumber()
        {
            int counter = 0;
            counter++;
            return "M" + counter.ToString().PadLeft(6, '0'); // 假设流水号以SN开头，后跟递增的数字，且总长度为8位
        }
    }
}