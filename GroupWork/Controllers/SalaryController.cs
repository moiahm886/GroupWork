using GroupWork.DAL;
using GroupWork.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Reflection.Emit;

namespace GroupWork.Controllers
{
    public class SalaryController : Controller
    {
        private readonly EmployeeInterface employeeInterface;
        private readonly SalaryInterface salaryInterface;
        public SalaryController(EmployeeInterface employeeInterface,SalaryInterface salaryInterface)
        {
            this.employeeInterface = employeeInterface;
            this.salaryInterface = salaryInterface;
        }
        public async Task<IActionResult> AddSalary(string EmpCode) {
            ViewData["Authorized"] = "Admin";
            var employee = await employeeInterface.getEmployee(EmpCode);
            if (employee != null)
            {
                return View(employee);
            }
            return View();
        }
        public async Task<IActionResult> Add(SalaryModel salaryModel)
        {
            var employee = await employeeInterface.FindAsync(salaryModel.EmpId);
            var salary = new SalaryModel
            {
                EmpId = salaryModel.EmpId,
                SalaryMonthYear = salaryModel.SalaryMonthYear,
                Date = salaryModel.Date,
                BasicSalary = salaryModel.BasicSalary, 
                TotalDeduction = (float)salaryModel.TotalDeduction,
                TotalAllowance = (float)salaryModel.TotalAllowance,
                TotalSalary = (float)salaryModel.TotalSalary,
                TaxDeduction = (float)salaryModel.TaxDeduction,
                ProvidentFundDeduction = (float)salaryModel.ProvidentFundDeduction,
                CompanyId = salaryModel.CompanyId,
                BranchId = salaryModel.BranchId,
                AddedBy = 1,
                AddedDate = DateTime.Now
            };
            await salaryInterface.AddAsync(salary);
            return RedirectToAction("AddSalary", new { EmpCode = employee.EmpCode});
        }
        public async Task<IActionResult> ViewSalary(string EmpCode)
        {
            ViewData["Authorized"] = "Admin";
            var Employee = await employeeInterface.getEmployee(EmpCode);
            var Salary = await salaryInterface.GetByEmpId(Employee.Id);
            if (Salary != null)
            {
                return View(Salary);
            }
            else
            {
                return RedirectToAction("AddSalary", new {EmpCode = EmpCode});
            }
        }
        public async Task<IActionResult> Update(SalaryModel salaryModel,int Id)
        {
            var Salary = await salaryInterface.GetByIdAsync(Id);
            var employee = await employeeInterface.FindAsync(salaryModel.EmpId);
            if (Salary != null)
            {
                Salary.Id = Id;
                Salary.EmpId = salaryModel.EmpId;
                Salary.SalaryMonthYear = salaryModel.SalaryMonthYear;
                Salary.Date = salaryModel.Date;
                Salary.BasicSalary = salaryModel.BasicSalary;
                Salary.TotalDeduction = (float)salaryModel.TotalDeduction;
                Salary.TotalAllowance = (float)salaryModel.TotalAllowance;
                Salary.TotalSalary = (float)salaryModel.TotalSalary;
                Salary.TaxDeduction = (float)salaryModel.TaxDeduction;
                Salary.ProvidentFundDeduction = (float)salaryModel.ProvidentFundDeduction;
                Salary.CompanyId = salaryModel.CompanyId;
                Salary.BranchId = salaryModel.BranchId;
                Salary.UpdatedBy = 1;
                Salary.UpdatedDate = DateTime.Now;
                await salaryInterface.UpdateAsync(Salary);
                TempData["AlertScript"] = "Swal.fire('Success!', 'Skills Updated Successfully', 'success');";
            }
            return RedirectToAction("ViewSalary", new { EmpCode = employee.EmpCode });
        }
        public async Task<IActionResult> DeleteSalary(int Id)
        {
            var Salary = await salaryInterface.GetByIdAsync(Id);
            await salaryInterface.DeleteAsync(Salary);
            return RedirectToAction("AddedEmployee", "Employee");
        }
    }
}
