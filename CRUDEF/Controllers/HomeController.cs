using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDEF.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDEF.Controllers
{
    public class HomeController : Controller
    {
        private EmployeeContext employeeContext ;
        public HomeController(EmployeeContext insEmployeeContext)
        {
            employeeContext = insEmployeeContext;
        }

        public IActionResult Index()
        {
            return View(employeeContext.Employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult Create_Post(Employee Employee)
        {
            if (ModelState.IsValid)
            {
                Employee.AddedOn = DateTime.Now;
                employeeContext.Employee.Add(Employee);
                employeeContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View();
        }

        public IActionResult Update(int id)
        {
            return View(employeeContext.Employee.Where(a => a.Id == id).FirstOrDefault());
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult Update_Post(Employee Employee)
        {
            employeeContext.Employee.Update(Employee);
            employeeContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var Employee = employeeContext.Employee.Where(a => a.Id == id).FirstOrDefault();
            employeeContext.Employee.Remove(Employee);
            employeeContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}