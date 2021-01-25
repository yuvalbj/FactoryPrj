using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FactoryPrj.Models;
namespace FactoryPrj.Controllers
{
    public class EmployeeController : Controller
    {
        EmpBL empBL = new EmpBL();
        DepartmentBL depBL = new DepartmentBL();
        ShiftBL ShiftBL = new ShiftBL();
        EmplShiftBL emshifBL = new EmplShiftBL();
        LoginBL loginBL = new LoginBL();

        public ActionResult Index()
        {

            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);
            if (isActionAllowed == true && (bool)Session["authenticated"] == true)
            {
                var emps = empBL.GetEmployees();
                var shifts = empBL.getAllEmpsWithDateAsString();
                var deps = depBL.GetDepartments();
                ViewBag.deps = deps;
                ViewBag.emps = emps;
                ViewBag.shifts = shifts;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult SearchResult(string str)
        {
            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);
            if (isActionAllowed == true && (bool)Session["authenticated"] == true)
            {
                var results = empBL.Serach(str);
                ViewBag.emps = results;
                return View("SearchResult");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult EditEmp(int id)
        {
            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);
            if (isActionAllowed == true && (bool)Session["authenticated"] == true)
            {
                var deps = depBL.GetDepartments();
                ViewBag.deps = deps;
                var emp = empBL.GetEmployee(id);
                return View("EditEmp", emp);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult GetUpdatedEmp(Employee emp)
        {
            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);
            if (isActionAllowed == true && (bool)Session["authenticated"] == true)
            {
                empBL.UpdateEmp(emp);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult AddShiftToEmployee(int id)
        {
            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);
            if (isActionAllowed == true && (bool)Session["authenticated"] == true)
            {
                var shifts = ShiftBL.GetShifts();
                var emp = empBL.GetEmployee(id);
                ViewBag.emp = emp;
                ViewBag.shifts = shifts;
                return View("AddShiftToEmployee");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public ActionResult GetAddedShiftToEmp(int empID, int shiftID)
        {
            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);
            if (isActionAllowed == true && (bool)Session["authenticated"] == true)
            {
                emshifBL.AddEmpToShift(shiftID, empID);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult DelteEmployee(int id) 
        {
            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);
            if (isActionAllowed == true && (bool)Session["authenticated"] == true) 
            {
                empBL.DeleteEmp(id);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}