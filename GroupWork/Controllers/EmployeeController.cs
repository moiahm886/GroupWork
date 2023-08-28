using GroupWork.DAL;
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
        private readonly EmployeeInterface employeeInterface;
        private readonly PermissionInterface permissionInterface;

        public EmployeeController(EmployeeInterface employeeInterface, PermissionInterface permissionInterface)
        {
            this.employeeInterface = employeeInterface;
            this.permissionInterface = permissionInterface;
        }
        public IActionResult CheckPermission()
        {
            var role = permissionInterface.role();
            if (role != null) {
                var permission = permissionInterface.permission(role.Id);
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
            var activeUsers = await employeeInterface.ActiveUser();
            var usersWithoutEmployee = new List<UserModel>();

            foreach (var user in activeUsers)
            {
                var employeeExists = await employeeInterface.existingEmployee(user.EmpId);
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
            var employees = await employeeInterface.GetEmployees();
            return View(employees);
        }
        public async Task<IActionResult> AddEmployee(EmployeeModel model)
        {
            if (model != null)
            {
                var isUnique = !await employeeInterface.checkuniqueniess(model.EmpCode);
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
                await employeeInterface.AddAsync(employee);
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
            var employee = await employeeInterface.FindAsync(employeeModel.Id);
            if (employee != null)
            {
                await employeeInterface.Remove(employee);
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
                var employee = await employeeInterface.getEmployee(EmpCode);
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
                var employee = await employeeInterface.getEmployee(EmpCode);
                if (employee != null)
                {
                    employee.EmpFirstNameEn = employeeModel.EmpFirstNameEn;
                    employee.EmpLastNameEn = employeeModel.EmpLastNameEn;
                    employee.EmpMobileNo = employeeModel.EmpMobileNo;
                    employee.EmpEmail = employeeModel.EmpEmail;
                    employee.EmpDOB = employeeModel.EmpDOB;
                    employee.UpdatedBy = 1;
                    employee.UpdatedDate = DateTime.Now;
                    await employeeInterface.UpdateAsync(employee);
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
                var employee = await employeeInterface.getEmployee(EmpCode);
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
