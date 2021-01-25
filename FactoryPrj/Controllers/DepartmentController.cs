using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FactoryPrj.Models;
namespace FactoryPrj.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentBL depBL = new DepartmentBL();
        LoginBL loginBL = new LoginBL();
        EmpBL empBL = new EmpBL();
        
        // GET: Department
        public ActionResult Index()
        {
            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);

            if (isActionAllowed == true && (bool)Session["authenticated"] == true)
            {
                var deps = depBL.GetDepartments();
                var emps = empBL.GetEmployees();
                ViewBag.deps = deps;
                ViewBag.emps = emps;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
           
        }

        public ActionResult EditDep(int id)
        {
            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);

            if (isActionAllowed == true && (bool)Session["authenticated"] == true)
            {
                var dep = depBL.GetDepartment(id);
                return View("EditDep", dep);

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public ActionResult GetUpdatedDep(Department dep)
        {
            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);

            if (isActionAllowed == true && (bool)Session["authenticated"] == true)
            {
                depBL.UpdateDep(dep);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
     
        public ActionResult AddNewDep()
        {
            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);

            if (isActionAllowed == true && (bool)Session["authenticated"] == true)
            {
                return View("AddNewDep");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult GetNewDeps(Department dep)
        {
            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);

            if (isActionAllowed == true && (bool)Session["authenticated"] == true) 
            {
                depBL.AddDep(dep);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }


        }

        public ActionResult DeleteDep(int id)
        {
            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);

            if (isActionAllowed == true && (bool)Session["authenticated"] == true)
            {
                depBL.DeleteDep(id);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Login");

            }

        }
    }
}