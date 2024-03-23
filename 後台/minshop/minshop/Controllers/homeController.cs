using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
namespace minshop.Controllers
{
    public class homeController : Controller
    {
        string constr = @"Data Source=.;Initial Catalog=mineshop;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
        private void executeSql(string sql)
        {
            SqlConnection cnn = new SqlConnection(constr);
            cnn.Open();
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
        private DataTable querySql(string sql)
        {
            SqlConnection cnn = new SqlConnection(constr);
            cnn.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, cnn);
            da.Fill(ds);
            cnn.Close();
            return ds.Tables[0];
        }
        // GET: home
        public ActionResult CMS()
        {
            DataTable t = querySql("select * from member");
            return View(t);
        }
        public ActionResult delete(string memberId)
        {
            string sql = "delete from member where memberId='" + memberId + "'";
            executeSql(sql);
            return RedirectToAction("CMS");
        }
        public ActionResult PMS()
        {
            DataTable t = querySql("select * from stock");
            return View(t);
        }
    }
}