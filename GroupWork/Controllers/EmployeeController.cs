using GroupWork.Data;
using GroupWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace GroupWork.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ManagementDataContextClass managementDataContextClass;
        public EmployeeController(ManagementDataContextClass managementDataContextClass)
        {
            this.managementDataContextClass = managementDataContextClass;
        }
        public IActionResult CheckPermission()
        {
            var role = managementDataContextClass.tbRoles.FirstOrDefault(e => e.Name == "Employee");
            if (role != null) {
                var permission = managementDataContextClass.tbPermissions.FirstOrDefault(u => u.RoleId == role.Id);
                if (permission != null)
                {
                    if (permission.CanRead == 1)
                    {
                        ViewData["Read"] = "Allow";
                    }
                    if (permission.CanUpdate == 1)
                    {
                        ViewData["Update"] = "Allow";
                    }
                    if (permission.CanDelete == 1)
                    {
                        ViewData["Delete"] = "Allow";
                    }
                }
            }
            return NoContent();
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
        public async Task<IActionResult> AddedEmployee()
        {
            ViewData["Authorized"] = "Admin";
            var employees = await managementDataContextClass.tbEmployees.ToListAsync();
            return View(employees);
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
                TempData["AlertScript"] = "Swal.fire('Success!', 'Employee Added Successfully', 'success');";
            }
            return RedirectToAction("ManageEmployee");
        }

        public IActionResult EmployeeDashboard(string EmpCode)
        {
            ViewData["Authorized"] = "Employee";
            HttpContext.Session.SetString("EmpCode", EmpCode);
            ViewData["EmpCode"] = EmpCode;
            return View();
        }
        public async Task<IActionResult> DeleteEmployee(EmployeeModel employeeModel)
        {
            var employee = await managementDataContextClass.tbEmployees.FindAsync(employeeModel.Id);
            if (employee != null)
            {
                managementDataContextClass.tbEmployees.Remove(employee);
                await managementDataContextClass.SaveChangesAsync();
                TempData["AlertScript"] = "Swal.fire('Success!', 'Employee deleted.', 'success');";
            }
            return RedirectToAction("AddedEmployee");
        }
        public async Task<IActionResult> Viewinfo()
        {
            ViewData["Authorized"] = "Employee";
            var EmpCode = HttpContext.Session.GetString("EmpCode");
            ViewData["EmpCode"] = EmpCode;
            CheckPermission();
            if (EmpCode != null)
            {
                var employee = await managementDataContextClass.tbEmployees.FirstOrDefaultAsync(e => e.EmpCode == EmpCode);
                if (employee != null)
                {
                    return View(employee);
                }
            }
            TempData["AlertScript"] = "Swal.fire('Error!', 'Employee not found.', 'error');";
            return View();
        }
        public async Task<IActionResult> UpdateEmployee(EmployeeModel employeeModel)
        {
            ViewData["Authorized"] = "Employee";
            var EmpCode = HttpContext.Session.GetString("EmpCode");
            ViewData["EmpCode"] = EmpCode;
            if (EmpCode != null)
            {
                var employee = await managementDataContextClass.tbEmployees.FirstOrDefaultAsync(e => e.EmpCode == EmpCode);
                if (employee != null)
                {
                    employee.EmpFirstNameEn = employeeModel.EmpFirstNameEn;
                    employee.EmpLastNameEn = employeeModel.EmpLastNameEn;
                    employee.EmpMobileNo = employeeModel.EmpMobileNo;
                    employee.EmpEmail = employeeModel.EmpEmail;
                    employee.EmpDOB = employeeModel.EmpDOB;
                    employee.UpdatedBy = 1;
                    employee.UpdatedDate = DateTime.Now;
                    managementDataContextClass.tbEmployees.Update(employee);
                    await managementDataContextClass.SaveChangesAsync();
                    TempData["AlertScript"] = "Swal.fire('Success!', 'Your Info Updated Successfully', 'success');";
                }
                return RedirectToAction("ViewInfo");
            }
            TempData["AlertScript"] = "Swal.fire('Error!', 'Employee not found.', 'error');";
            return View();
        }
        public async Task<IActionResult> UpdatePage()
        {
            ViewData["Authorized"] = "Employee";
            var EmpCode = HttpContext.Session.GetString("EmpCode");
            ViewData["EmpCode"] = EmpCode;
            CheckPermission();
            if (EmpCode != null)
            {
                var employee = await managementDataContextClass.tbEmployees.FirstOrDefaultAsync(e => e.EmpCode == EmpCode);
                if (employee != null)
                {
                    return View(employee);
                }
            }
            TempData["AlertScript"] = "Swal.fire('Error!', 'Employee not found.', 'error');";
            return View();
        }
    }
}
