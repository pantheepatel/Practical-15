using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task1.Controllers
{
    public class HomeController : Controller
    {
        private string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public ActionResult Index()
        {
            var userName = User.Identity.Name;
            var time = DateTime.Now;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "INSERT INTO AccessLogs (UserName, AccessTime) VALUES (@UserName, @AccessTime)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@AccessTime", time);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            ViewBag.User = userName;
            return View();
        }

        public ActionResult Log()
        {
            List<Tuple<string, DateTime>> logs = new List<Tuple<string, DateTime>>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "SELECT UserName, AccessTime FROM AccessLogs ORDER BY AccessTime DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    logs.Add(new Tuple<string, DateTime>(
                        reader["UserName"].ToString(),
                        Convert.ToDateTime(reader["AccessTime"])
                    ));
                }
            }

            ViewBag.Logs = logs;
            return View();
        }
    }
}