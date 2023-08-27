using GroupWork.Data;
using GroupWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroupWork.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ManagementDataContextClass managementDataContextClass;
        public EmployeeController(ManagementDataContextClass managementDataContextClass)
        {
            this.managementDataContextClass = managementDataContextClass;
        }
        public async Task<IActionResult> ManageEmployee()
        {
            ViewData["Authorized"] = "Admin";
            var activeUsers = await managementDataContextClass.tbUsers
            .Where(user => user.IsActive == 1)
            .ToListAsync();
            var usersWithoutEmployee = new List<UserModel>();

            foreach (var user in activeUsers)
            {
                var employeeExists = await managementDataContextClass.tbEmployees
                    .AnyAsync(employee => employee.EmpCode == user.EmpId.ToString());
                if (!employeeExists)
                {
                    usersWithoutEmployee.Add(user);
                }
            }
            var Employee = new EmployeeModel();
            return View((usersWithoutEmployee,Employee));
        }
        public async Task<IActionResult> AddEmployee(EmployeeModel model)
        {
            if (model != null)
            {
                var isUnique = !await managementDataContextClass.tbEmployees.AnyAsync(e => e.EmpCode == model.EmpCode);
                if (!isUnique)
                {
                    TempData["AlertScript"] = "Swal.fire('Error!', 'Employee code already exists.', 'error');";
                    return RedirectToAction("ManageEmployee");
                }
                var employee = new EmployeeModel
                {
                    EmpCode = model.EmpCode,
                    EmpDepartmetId = model.EmpDepartmetId,
                    EmpFirstNameEn = model.EmpFirstNameEn,
                    EmpLastNameEn = model.EmpLastNameEn,
                    EmpEmail = model.EmpEmail,
                    EmpMobileNo = model.EmpMobileNo,
                    EmpDOB = model.EmpDOB,
                    EmpGenderId = model.EmpGenderId,
                    EmpSectionId = model.EmpSectionId,
                    CompanyId = model.CompanyId,
                    BranchId = model.BranchId,
                    IsActive = 1,
                    AddedBy = 1,
                    AddedDate = DateTime.Now,
                };
                await managementDataContextClass.tbEmployees.AddAsync(employee);
                await managementDataContextClass.SaveChangesAsync();
            }
            return RedirectToAction("ManageEmployee");
        }
        public IActionResult EmployeeDashboard(string EmpCode)
        {
            HttpContext.Session.SetString("EmpCode", EmpCode);
            return View();
        }
    }
}
