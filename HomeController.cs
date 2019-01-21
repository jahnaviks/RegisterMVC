using RegisterMVC.DataAccess;
using PagedList; 
using RegisterMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegisterMVC.Controllers
{
    public class HomeController : Controller
    {
        static string mail = null;
        private const int pageSize = 3; 
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ClickEvent()
        {
            RegisterMapper regMap = new RegisterMapper();
            ViewBag.Message = regMap.Register(Request["email"], Request["fname"], Request["lname"], Request["address"], Request["mobile"], Request["password"]);
            return View();           
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Validate()
        {
            RegisterMapper regMap = new RegisterMapper();
            ViewBag.Message = regMap.Update(Request["email"], Request["password"]);
            mail = Request["email"];
            return View();
        }

        public ActionResult Update()
        {
            RegisterMapper regMap = new RegisterMapper();
            ViewBag.Message = regMap.GetUserDetails(mail);
            return View();
        }

        [HttpPost]
        public ActionResult UpdateClick()
        {
            RegisterMapper regMap = new RegisterMapper();
            regMap.UpdateClick(Request["email"], Request["fname"], Request["lname"], Request["address"], Request["mobile"], Request["password"]);
            return View();
        }

        public ActionResult Delete()
        {
            RegisterMapper regMap = new RegisterMapper();
            ViewBag.Message= regMap.GetUserDetails(mail);
            return View();
        }

        [HttpPost]
        public ActionResult DeleteClick()
        {
            RegisterMapper regMap = new RegisterMapper();
            regMap.DeleteClick(mail);
            return View();
        }
        
        [HttpGet]
        public ActionResult ViewList(int? page)
        {
            Registration reg = new Registration();
            reg.PageNumber = page.HasValue ? Convert.ToInt32(page) : 1;
            List<Registration> lmd = new List<Registration>();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RegisterDBEntities"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_GetDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);    
           foreach (DataRow dr in ds.Tables[0].Rows) {

                lmd.Add(new Registration  
                {  
                    FirstName = dr["FirstName"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    Email = dr["EmailId"].ToString(),
                    Address = dr["Address"].ToString(),
                    PhoneNumber = dr["MobileNumber"].ToString(),
                    Password = dr["Password"].ToString(),  
                });
           }
           return View(lmd.ToPagedList(reg.PageNumber, pageSize));
        }
    }
}