using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryPrj.Models
{
    public class EmpBL
    {
        FactoryEntities1 db = new FactoryEntities1();
        public List<DataForSearch> Serach(string str)
        {
            var result = from emp in db.Employees
                         join dep in db.Departments on emp.DepID equals dep.DepID

                         select new DataForSearch
                         {
                             empID = emp.EmpID,
                             DepName = dep.DepName,
                             FirstName = emp.FirstName,
                             LastName = emp.LastName
             
                         };

            return result.Where(x => x.DepName.Contains(str) || x.FirstName.Contains(str) || x.LastName.Contains(str)).ToList();
        }
        public List<empIDandShifts> getAllEmpsWithDateAsString()
        {
            var result = from emp in db.Employees
                         join allShif in db.EmployeeShifts on emp.EmpID equals allShif.EmpID
                         join shif in db.Shifts on allShif.ShiftID equals shif.ShiftID

                         select new empIDandShifts
                         {
                          
                             empShifID = allShif.EmpShiftID,
                             EmpID = allShif.EmpID,
                             DateAsString = shif.Date.ToString() + " , "+ shif.StartTime.ToString() + " - " +shif.EndTime.ToString(),
                             shiftID = shif.ShiftID
                         };

                       return result.ToList();
        }

   
        public Employee GetEmployee(int id)
        {
            return db.Employees.Where(x => x.EmpID == id).First();
        }

        public List<Employee> GetEmployees()
        {
            return db.Employees.ToList();
        }

        public void UpdateEmp(Employee emp)
        {
            var empToChange = db.Employees.Where(x => x.EmpID == emp.EmpID).First();
            empToChange.FirstName = emp.FirstName;
            empToChange.LastName = emp.LastName;
            empToChange.StartWorkYear = emp.StartWorkYear;
            empToChange.DepID = emp.DepID;

            db.SaveChanges();
        }

        public void DeleteEmp(int id)
        {
            Employee emp = db.Employees.Where(x => x.EmpID == id).First();
            db.Employees.Remove(emp);

            var result = db.EmployeeShifts.Where(x => x.EmpID == id).ToList();
             foreach (var empshif in result)
            {
                db.EmployeeShifts.Remove(empshif);
            }

            db.SaveChanges();
        }




    }

}