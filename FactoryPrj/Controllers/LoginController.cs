using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FactoryPrj.Models;

namespace FactoryPrj.Controllers
{

    
    public class LoginController : Controller
    {
        // GET: Login
        LoginBL loginBL = new LoginBL();
      

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetHomePage(string usrname, string pwd)
        {
            bool isAuthenticated = loginBL.IsAuthenticated(usrname, pwd);
            if (isAuthenticated == true)
            {
                var usr = loginBL.getUser(usrname);
                Session["usrFullName"] = usr.FullName;
                Session["userName"] = usr.UserName;
                Session["authenticated"] = true;
              

                return View("HomePage");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Logout()
        {
            
            Session.Clear();
            return View("Index");
        }
        public ActionResult GotoDepartment()
        {
            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);

            if(isActionAllowed == true && (bool)Session["authenticated"] == true)
            {
                return RedirectToAction("Index", "Department");
            }
            else
            {
               // run out of credit
                return View("Index");
            }

        }

        public ActionResult GotoShifts()
        {
            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);
            if(isActionAllowed == true && (bool)Session["authenticated"] == true)
            {
                return RedirectToAction("Index", "Shift");
            }
            else
            {
               // run out of credit
                return View("Index");
            }

        }

        public ActionResult GotoEmployee()
        {
            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);

            if (isActionAllowed == true && (bool)Session["authenticated"] == true)
            {
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                // run out of credit
                return View("Index");
            }

        }

 

       }
}