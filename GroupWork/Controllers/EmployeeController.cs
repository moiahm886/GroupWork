using GroupWork.DAL;
using GroupWork.Data;
using GroupWork.Models;
using Microsoft.AspNetCore.Mvc;
namespace GroupWork.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly CompanyInterface companyInterface;
        private readonly EmployeeInterface employeeInterface;
        private readonly PermissionInterface permissionInterface;
        private readonly ExperienceInterface experienceInterface;

        public EmployeeController(CompanyInterface companyInterface,EmployeeInterface employeeInterface, PermissionInterface permissionInterface,ExperienceInterface experienceInterface)
        {
            this.companyInterface = companyInterface;
            this.employeeInterface = employeeInterface;
            this.permissionInterface = permissionInterface;
            this.experienceInterface = experienceInterface;
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
        public async Task<IActionResult> AddExperience()
        {
            ViewData["Authorized"] = "Employee";
            var EmpCode = HttpContext.Session.GetString("EmpCode");
            ViewData["EmpCode"] = EmpCode;
            if(EmpCode != null) {
                var Employee = await employeeInterface.getEmployee(EmpCode);
                var companyId = Employee.CompanyId;
                var Company = await companyInterface.FindCompanyByID(companyId);
                if (Company != null)
                {
                    return View((Employee, Company));
                }
            }
            TempData["AlertScript"] = "Swal.fire('Error!', 'Employee not found.', 'error');";
            return View();
        }
        public async Task<IActionResult> AddedExperience(ExperienceModel experienceModel)
        {
            var EmpCode = HttpContext.Session.GetString("EmpCode");
            ViewData["EmpCode"] = EmpCode;
            if (EmpCode != null) {
                var Employee = await employeeInterface.getEmployee(EmpCode);
                var experience = new ExperienceModel
                {
                    EmpId = Employee.Id,
                    EmpJobTitle = experienceModel.EmpJobTitle,
                    EmpCompanyName = experienceModel.EmpCompanyName,
                    EmpPostionId = experienceModel.EmpPostionId,
                    EmpCountryId = experienceModel.EmpCountryId,
                    EmpWorkEndDate = experienceModel.EmpWorkEndDate,
                    EmpWorkStartDate = experienceModel.EmpWorkStartDate,
                    EmpResponsibilities = experienceModel.EmpResponsibilities,
                    BranchId = experienceModel.BranchId,
                    CompanyId = experienceModel.CompanyId,
                    AddedDate = DateTime.Now,
                    AddedBy = 1
                };
                await experienceInterface.AddExperience(experience);
                TempData["AlertScript"] = "Swal.fire('Success!', 'Experience Added Successfully', 'success');";
                return RedirectToAction("AddExperience");
            }
            return View("AddExperience");
        }
        public async Task<IActionResult> ViewExperience()
        {
            CheckPermission();
            ViewData["Authorized"] = "Employee";
            var EmpCode = HttpContext.Session.GetString("EmpCode");
            ViewData["EmpCode"] = EmpCode;
            if (EmpCode != null)
            {
                var Employee = await employeeInterface.getEmployee(EmpCode);
                var experience = await experienceInterface.GetExperienceByEmpId(Employee.Id);
                if (experience != null)
                {
                    return View(experience);
                }
                else
                {
                    return RedirectToAction("AddExperience");
                }
                
            }
            TempData["AlertScript"] = "Swal.fire('Error!', 'Experience not found.', 'error');";
            return View();
        }
        public async Task<IActionResult> ExperienceUpdatePage(int Id)
        {
            CheckPermission();
            ViewData["Authorized"] = "Employee";
            var EmpCode = HttpContext.Session.GetString("EmpCode");
            ViewData["EmpCode"] = EmpCode;
            var expereince = await experienceInterface.GetExperienceById(Id);
            return View(expereince);
        }
        public async Task<IActionResult> UpdateExperience(ExperienceModel experienceModel,int Id)
        {
            ViewData["Authorized"] = "Employee";
            var EmpCode = HttpContext.Session.GetString("EmpCode");
            ViewData["EmpCode"] = EmpCode;
            var experience = await experienceInterface.GetExperienceById(Id);
            if (experience != null)
            {
                experience.Id = Id;
                experience.EmpId = experienceModel.EmpId;
                experience.EmpJobTitle = experienceModel.EmpJobTitle;
                experience.EmpCompanyName = experienceModel.EmpCompanyName;
                experience.EmpPostionId = experienceModel.EmpPostionId;
                experience.EmpCountryId = experienceModel.EmpCountryId;
                experience.EmpWorkEndDate = experienceModel.EmpWorkEndDate;
                experience.EmpWorkStartDate = experienceModel.EmpWorkStartDate;
                experience.EmpResponsibilities = experienceModel.EmpResponsibilities;
                experience.BranchId = experienceModel.BranchId;
                experience.CompanyId = experienceModel.CompanyId;
                experience.UpdatedDate = DateTime.Now;
                experience.UpdatedBy = 1;
                await experienceInterface.UpdateAsync(experience);
                TempData["AlertScript"] = "Swal.fire('Success!', 'Experience Updated Successfully', 'success');";
            }
            return RedirectToAction("ExperienceUpdatePage", new { Id = Id });
        }
    }
}
