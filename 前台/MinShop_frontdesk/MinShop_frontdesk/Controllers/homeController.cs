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
                return View("Index", "_Layout", stocks);
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
        public ActionResult AddCar(string productId)
        {
            var memberId = ((member)Session["Member"]).memberId;
            var currenCar = db.shoppingDetail.Where(m => m.memberId == memberId && m.productId == productId && m.IsApproved == "否").FirstOrDefault();
            if (currenCar == null)
            {
                var product = db.stock.Where(m => m.productId == productId).FirstOrDefault();
                shoppingDetail sd = new shoppingDetail();
                for (int i = 0; i < 1; i++)
                {
                    sd.cartId = ProductNumber();
                }
                sd.memberId = memberId;
                sd.IsApproved = "否";
                db.shoppingDetail.Add(sd);
            }
            else
            {
                currenCar.quantity += 1;
            }
            db.SaveChanges();
            return RedirectToAction("ShoppingCar");
        }
        public ActionResult Users()
        {
            string email = Session["MemberEmail"].ToString();
            var user = db.member.Where(m => m.email == email).FirstOrDefault();
            return View(user);
        }
        [HttpPost]
        public ActionResult Users(string name, string email, string sex, DateTime birthday, string phone, string address, string companyNumbers)
        {
            var user = db.member.Where(m => m.email == email).FirstOrDefault();
            user.name = name;
            user.email = email;
            user.sex = sex;
            user.birthday = birthday;
            user.phone = phone;
            user.address = address;
            user.companyNumbers = companyNumbers;
            try
            {
                // 嘗試儲存更改至資料庫
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // 處理驗證失敗的例外
                foreach (var entityErrors in ex.EntityValidationErrors)
                {
                    // 獲取驗證失敗的實體
                    var entity = entityErrors.Entry.Entity.GetType().Name;

                    // 獲取驗證失敗的屬性
                    foreach (var error in entityErrors.ValidationErrors)
                    {
                        var propertyName = error.PropertyName;
                        var errorMessage = error.ErrorMessage;

                        // 處理驗證失敗的屬性
                        // 例如，記錄錯誤、向使用者顯示錯誤等
                    }
                }
            }
            return RedirectToAction("Users");
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
        public ActionResult Login(string email, string pass)
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
                Session["MemberEmail"] = me.email;
                Session["Welcome"] = me.name + "-歡迎光臨";
                return RedirectToAction("index");
            }
        }
        public ActionResult register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult register(string name, string email, string pass1, string sex, DateTime birthday, string phone, string address, string companyNumbers)
        {
            member m = new member();
            for (int i = 0; i < 1; i++)
            {
                m.memberId = memberNumber();
            }
            m.name = name;
            m.email = email;
            m.password = pass1;
            m.sex = sex;
            m.date = DateTime.Now;
            m.birthday = birthday;
            m.phone = phone;
            m.address = address;
            m.companyNumbers = companyNumbers;
            db.member.Add(m);
            try
            {
                // 嘗試儲存更改至資料庫
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // 處理驗證失敗的例外
                foreach (var entityErrors in ex.EntityValidationErrors)
                {
                    // 獲取驗證失敗的實體
                    var entity = entityErrors.Entry.Entity.GetType().Name;

                    // 獲取驗證失敗的屬性
                    foreach (var error in entityErrors.ValidationErrors)
                    {
                        var propertyName = error.PropertyName;
                        var errorMessage = error.ErrorMessage;

                        // 處理驗證失敗的屬性
                        // 例如，記錄錯誤、向使用者顯示錯誤等
                    }
                }
            }
            return RedirectToAction("Login");
        }

        public static string memberNumber()
        {
            using (var db = new mineshopEntities())
            {
                int counter;
                var max = db.member.Max(m => m.memberId);
                if (max != null)
                {
                    counter = Convert.ToInt32(max.Substring(1));
                }
                else
                {
                    counter = 0;
                }
                counter++;
                return "M" + counter.ToString().PadLeft(6, '0');
            }
        }
        public static string ProductNumber()
        {
            using (var db = new mineshopEntities())
            {
                int counter;
                var max = db.shoppingDetail.Max(m => m.cartId);
                if (max != null)
                {
                    counter = Convert.ToInt32(max.Substring(1));
                }
                else
                {
                    counter = 0;
                }
                counter++;
                return "P" + counter.ToString().PadLeft(6, '0');
            }
        }
    }
}