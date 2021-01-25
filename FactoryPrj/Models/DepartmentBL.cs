using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryPrj.Models
{
    public class DepartmentBL
    {
        FactoryEntities1 db = new FactoryEntities1();
        public List<Department> GetDepartments()
        {
            return db.Departments.ToList();
        }

        

        public Department GetDepartment(int id)
        {
            return db.Departments.Where(x => x.DepID == id).First();
        }

        public void UpdateDep(Department dep)
        {
            var depToChange = db.Departments.Where(x => x.DepID == dep.DepID).First();
            depToChange.DepName = dep.DepName;
            depToChange.DepManager = dep.DepManager;

            db.SaveChanges();
        }
        
        public void AddDep(Department dep)
        {
            db.Departments.Add(dep);
            db.SaveChanges();
        }

        public void DeleteDep(int id)
        {
            var depToDel = db.Departments.Where(x => x.DepID == id).First();
            db.Departments.Remove(depToDel);
            db.SaveChanges();

        }
        
    }
}