using DocumentFormat.OpenXml.InkML;
using EmployeePayroll_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace EmployeePayroll_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        Employee_Context employee_Context;
        private IConfiguration _config;
        public EmployeeController(Employee_Context employee_Context, IConfiguration _config)
        {
            this.employee_Context = employee_Context;
            this._config = _config;
        }
        //Add
        [HttpGet]
        public IActionResult Add(int Id=0)
        {
            try
            {
                if (Id == 0)
                {
                    return  View(new EmployeeModel());
                }
                return View();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost,ActionName("Add")]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind("EmployeeId,FirstName,LastName,EmployeeAge,Department,Salary")] EmployeeModel employeeModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(employeeModel != null)
                    {
                        employeeModel.StartDate = DateTime.Now;
                        employee_Context.Details.Add(employeeModel);
                        employee_Context.SaveChanges();
                    }
                    return Json(new { IsValid = true,html = Helper.RenderRazorViewToString(this,"GetAll",this.employee_Context.Details.ToList())}) ;
                }
                return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "Add", this.employee_Context.Details.ToList()) });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return View(employee_Context.Details.ToList());
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
      
        public IActionResult GetById(int Id)
        {
            try
            {
                var ID=employee_Context.Details.Where(x => x.EmployeeId==Id).FirstOrDefault();
                return View(ID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            try
            {
                var EmpDetail = employee_Context.Details.Where(x => x.EmployeeId == Id).FirstOrDefault();
                if (EmpDetail == null)
                {
                    return NotFound();
                }
                return View(EmpDetail);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost, ActionName("Delete")]         //Delete is used here as a connection between Delete method and DeleteId
        public IActionResult DeleteID(int Id)
        {
            try
            {
                var EmpDetail = employee_Context.Details.Find(Id);
                employee_Context.Details.Remove(EmpDetail);
                employee_Context.SaveChanges();
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "GetAll", this.employee_Context.Details.ToList()) });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            try
            {
                var EmpDetail = employee_Context.Details.Where(x => x.EmployeeId == Id).FirstOrDefault();
                if (EmpDetail == null)
                {
                    return NotFound(EmpDetail);
                }
                return View(EmpDetail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditDetail([Bind("EmployeeId,FirstName,LastName,EmployeeAge,Department,Salary")] EmployeeModel employeeModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (employeeModel != null)
                    {
                        employeeModel.StartDate = DateTime.Now;
                        employee_Context.Details.Update(employeeModel);
                        employee_Context.SaveChanges();
                    }
                    return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "GetAll", this.employee_Context.Details.ToList()) });
                }
                return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "Add", this.employee_Context.Details.ToList()) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
