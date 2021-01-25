using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryPrj.Models
{
    public class EmplShiftBL
    {
        FactoryEntities1 db = new FactoryEntities1();

        public List<EmployeeShift> getAllshiftswithEmployees()
        {
            return db.EmployeeShifts.ToList();
        }

       public EmployeeShift getShiftsbyID(int id)
        {
            return db.EmployeeShifts.Where(x => x.EmpID == id).First();
        }

        public void AddEmpToShift(int shiftID, int empID)
        {
            EmployeeShift empshif = new EmployeeShift()
            {
                ShiftID = shiftID,
                EmpID = empID
            };
            db.EmployeeShifts.Add(empshif);
            db.SaveChanges();
        }
    }
}