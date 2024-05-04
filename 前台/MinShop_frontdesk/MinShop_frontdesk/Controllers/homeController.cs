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
            var memberId = ((member)Session["Member"]).memberId;
            var shopping = db.shoppingCart.Where(m => m.memberId == memberId&&m.checkout==null).ToList();
            return View("ShoppingCar", "_Layout_Login", shopping);
        }
        public ActionResult AddCar(string productId)
        {
            var memberId = ((member)Session["Member"]).memberId;
            var currenCar = db.shoppingCart.Where(m => m.memberId == memberId && m.productId == productId).FirstOrDefault();
            if (currenCar == null)
            {
                var product = db.stock.Where(m => m.productId == productId).FirstOrDefault();
                shoppingCart sc = new shoppingCart();
                for (int i = 0; i < 1; i++)
                {
                    sc.cartId = ShoppingNumber();
                }
                sc.memberId = memberId;
                sc.productId = product.productId;
                sc.name = product.name;
                sc.price = product.price;
                sc.quantity = 1;
                sc.date = DateTime.Now;
                db.shoppingCart.Add(sc);
            }
            else
            {
                currenCar.quantity += 1;
            }
            // 嘗試儲存更改至資料庫
            db.SaveChanges();
            return RedirectToAction("ShoppingCar");
        }
        public ActionResult DeleteCar(string productId)
        {
            var od = db.shoppingCart.Where(m => m.productId == productId).FirstOrDefault();
            db.shoppingCart.Remove(od);
            db.SaveChanges();
            return RedirectToAction("ShoppingCar");
        }
        public ActionResult UpdateCar(string productId)
        {
            var edit = db.shoppingCart.Where(m => m.productId == productId).FirstOrDefault();
            return View(edit);
        }
        [HttpPost]
        public ActionResult UpdateCar(string productId, int quantity)
        {
            var edit = db.shoppingCart.Where(m => m.productId == productId).FirstOrDefault();
            edit.quantity = quantity;
            db.SaveChanges();
            return RedirectToAction("ShoppingCar");
        }
        public ActionResult Users()
        {
            var memberId = ((member)Session["Member"]).memberId;
            string email = Session["MemberEmail"].ToString();
            var user = db.member.Where(m => m.email == email).FirstOrDefault();
            return View("Users", "_Layout_Login", user);
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
            db.SaveChanges();
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
            var memberId = ((member)Session["Member"]).memberId;
            var order = db.shoppingCart.Where(m => m.memberId == memberId).ToList();
            return View("Order", "_Layout_Login", order);
        }
        [HttpPost]
        public ActionResult Order(string name, int phone, string address, string send,string ps,int totalAmount)
        {
            var memberId = ((member)Session["Member"]).memberId;
            order o = new order();
            var shopping = db.shoppingCart.Where(m => m.memberId == memberId).FirstOrDefault();
            for (int i = 0; i < 10; i++)
            {
                o.orderId = OrderNumber();
            }
            o.memberId = memberId;
            o.cartId = shopping.cartId;
            o.reci = name;
            o.reciPhone = phone;
            o.reciAddress = address;
            o.total = totalAmount;
            o.sendOption = send;
            o.ps = ps;
            o.date = DateTime.Now;
            db.order.Add(o);
            shopping.checkout = DateTime.Now;
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
            return RedirectToAction("index");
        }
        public ActionResult history()
        {
            var memberId = ((member)Session["Member"]).memberId;
            var orders = db.order.Where(m => m.memberId == memberId).ToList();
            return View("history", "_Layout_Login", orders);
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
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("index");
        }
        public ActionResult register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult register(string name, string email, string pass1, string sex, DateTime birthday, string phone, string address, string companyNumbers)
        {
            var me = db.member.Where(m => m.email == email).FirstOrDefault();
            member mem = new member();
            if (me == null)
            {

                for (int i = 0; i < 1; i++)
                {
                    mem.memberId = memberNumber();
                }
                mem.name = name;
                mem.email = email;
                mem.password = pass1;
                mem.sex = sex;
                mem.date = DateTime.Now;
                mem.birthday = birthday;
                mem.phone = phone;
                mem.address = address;
                mem.companyNumbers = companyNumbers;
                db.member.Add(mem);
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
            else
            {
                ViewBag.Message = "帳號已存在,註冊失敗";
                return View();
            }
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
        public static string ShoppingNumber()
        {
            using (var db = new mineshopEntities())
            {
                int counter;
                var max = db.shoppingCart.Max(m => m.cartId);
                if (max != null)
                {
                    counter = Convert.ToInt32(max.Substring(1));
                }
                else
                {
                    counter = 0;
                }
                counter++;
                return "S" + counter.ToString().PadLeft(6, '0');
            }
        }
        public static string OrderNumber()
        {
            using (var db = new mineshopEntities())
            {
                int counter;
                var max = db.order.Max(m => m.orderId);
                if (max != null)
                {
                    counter = Convert.ToInt32(max.Substring(1));
                }
                else
                {
                    counter = 0;
                }
                counter++;
                return "O" + counter.ToString().PadLeft(6, '0');
            }
        }
    }
}