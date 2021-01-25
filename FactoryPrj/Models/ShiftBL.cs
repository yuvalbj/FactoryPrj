using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryPrj.Models
{
    public class ShiftBL
    {
       FactoryEntities1 db = new FactoryEntities1();
       public List<Shift> GetShifts()
        {
            return db.Shifts.ToList();
        }

        public void AddShift(Shift s)
        {
            db.Shifts.Add(s);
            db.SaveChanges();
        }

        public object GetShiftsWithEmployees()
        {
            var result = from emp in db.Employees
                         join allShif in db.EmployeeShifts on emp.EmpID equals allShif.EmpID
                         join shif in db.Shifts on allShif.ShiftID equals shif.ShiftID

                         select new ShiftIDAndEmployess
                         {
                             shiftID = shif.ShiftID,
                             empsInShift = emp.FirstName + " " + emp.LastName,
                             EmpID = allShif.EmpID
                             
                         };
            return result.ToList();
        }
    }
}