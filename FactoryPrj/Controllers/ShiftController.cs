using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FactoryPrj.Models;

namespace FactoryPrj.Controllers
{
    public class ShiftController : Controller
    {
        ShiftBL shiftBL = new ShiftBL();
        LoginBL loginBL = new LoginBL();

        public ActionResult Index()
        {
            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);
            if (isActionAllowed == true && (bool)Session["authenticated"] == true)
            {
                var emps = shiftBL.GetShiftsWithEmployees();
                var shif = shiftBL.GetShifts();
                ViewBag.shifts = shif;
                ViewBag.emps = emps;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult EditEmployee(int id)
        {
            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);
            if (isActionAllowed == true && (bool)Session["authenticated"] == true)
            {
                return RedirectToAction("EditEmp","Employee", new { id = id });
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult AddNewShift()
        {
            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);
            if (isActionAllowed == true && (bool)Session["authenticated"] == true) 
            {
                return View("AddShift");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult GetNewShifts(Shift shif)
        {
            bool isActionAllowed = loginBL.IsCrossedLImitPerDay((string)Session["userName"]);
            if (isActionAllowed == true && (bool)Session["authenticated"] == true)
            {
                shiftBL.AddShift(shif);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}