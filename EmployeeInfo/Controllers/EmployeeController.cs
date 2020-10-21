using EmployeeInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeInfo.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Get_AllCity()
        {
            using (EmployeeInfoContext Obj = new EmployeeInfoContext())
            {
              
                var citis = (from c in Obj.Cities
                                 select new
                                 { c.CityId, c.CityName})
                                 .ToList();
                return Json(citis, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Get_AllDepartment()
        {
            using (EmployeeInfoContext Obj = new EmployeeInfoContext())
            {
                var department = (from d in Obj.Departments
                                 select new
                                 { d.DeptId, d.DeptName })
                                 .ToList();
                return Json(department, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Get_AllEmployee()
        {
            using (EmployeeInfoContext Obj = new EmployeeInfoContext())
            {
                var employees = (from e in Obj.Employees
                                select new
                                { e.EmpId, e.EmpName, e.Gender, e.DeptId, e.CityId, e.Department.DeptName, e.City.CityName } )
                                .ToList();
                return Json(employees, JsonRequestBehavior.AllowGet);
            }
        }
       
        public JsonResult Get_EmployeeById(string Id)
        {
            using (EmployeeInfoContext Obj = new EmployeeInfoContext())
            {
                int EmpId = int.Parse(Id);
                return Json(Obj.Employees.Find(EmpId), JsonRequestBehavior.AllowGet);
            }
        }
      
        public string Insert_Employee(Employee Employe)
        {
            if (Employe != null)
            {
                using (EmployeeInfoContext Obj = new EmployeeInfoContext())
                {
                    Obj.Employees.Add(Employe);
                    Obj.SaveChanges();
                    return "Employee Added Successfully";
                }
            }
            else
            {
                return "Employee Not Inserted! Try Again";
            }
        }
       
        public string Delete_Employee(Employee Emp)
        {
            if (Emp != null)
            {
                using (EmployeeInfoContext Obj = new EmployeeInfoContext())
                {
                    var Emp_ = Obj.Entry(Emp);
                    if (Emp_.State == System.Data.Entity.EntityState.Detached)
                    {
                        Obj.Employees.Attach(Emp);
                        Obj.Employees.Remove(Emp);
                    }
                    Obj.SaveChanges();
                    return "Employee Deleted Successfully";
                }
            }
            else
            {
                return "Employee Not Deleted! Try Again";
            }
        }
      
        public string Update_Employee(Employee Emp)
        {
            if (Emp != null)
            {
                using (EmployeeInfoContext Obj = new EmployeeInfoContext())
                {
                    var Emp_ = Obj.Entry(Emp);
                    Employee EmpObj = Obj.Employees.Where(x => x.EmpId == Emp.EmpId).FirstOrDefault();
                    EmpObj.EmpName = Emp.EmpName;
                    EmpObj.DeptId = Emp.DeptId;
                    EmpObj.CityId = Emp.CityId;
                    Obj.SaveChanges();
                    return "Employee Updated Successfully";
                }
            }
            else
            {
                return "Employee Not Updated! Try Again";
            }
        }
    }
}