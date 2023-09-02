using GroupWork.DAL;
using GroupWork.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace GroupWork.Controllers
{
    public class SkillsController : Controller
    {
        private readonly EmployeeInterface employeeInterface;
        private readonly SkillsInterface skillsInterface;
        private readonly PermissionInterface permissionInterface;

        public SkillsController(EmployeeInterface employeeInterface, SkillsInterface skillsInterface, PermissionInterface permissionInterface)
        {
            this.employeeInterface = employeeInterface;
            this.skillsInterface = skillsInterface;
            this.permissionInterface = permissionInterface;
        }
        public IActionResult CheckPermission()
        {
            var role = permissionInterface.role();
            if (role != null)
            {
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
        public async Task<IActionResult> AddSkills()
        {
            ViewData["Authorized"] = "Employee";
            var EmpCode = HttpContext.Session.GetString("EmpCode");
            ViewData["EmpCode"] = EmpCode;
            if (EmpCode != null)
            {
                var employee = await employeeInterface.getEmployee(EmpCode);
                if (employee != null)
                {
                    return View(employee);
                }
            }
            return View();
        }
        public async Task<IActionResult> Add(SkillsModel skillsModel)
        {
            var skills = new SkillsModel
            {
                EmpId = skillsModel.EmpId,
                EmpSkillTypeId = skillsModel.EmpSkillTypeId,
                ExperienceInYears = skillsModel.ExperienceInYears,
                CompanyId = skillsModel.CompanyId,
                BranchId = skillsModel.BranchId,
                AddedBy = 1,
                AddedDate = DateTime.Now
            };
            await skillsInterface.AddAsync(skills);
            return RedirectToAction("AddSkills");
        }
        public async Task<IActionResult> ViewSkills()
        {
            CheckPermission();
            ViewData["Authorized"] = "Employee";
            var EmpCode = HttpContext.Session.GetString("EmpCode");
            ViewData["EmpCode"] = EmpCode;
            if (EmpCode != null)
            {
                var Employee = await employeeInterface.getEmployee(EmpCode);
                var Skills = await skillsInterface.GetByEmpId(Employee.Id);
                if (Skills != null)
                {
                    return View(Skills);
                }
                else
                {
                    return RedirectToAction("AddSkills");
                }
            }
            return View();
        }
        public async Task<IActionResult> UpdatePage(int Id)
        {
            CheckPermission();
            ViewData["Authorized"] = "Employee";
            var EmpCode = HttpContext.Session.GetString("EmpCode");
            ViewData["EmpCode"] = EmpCode;
            var Skills = await skillsInterface.GetByIdAsync(Id);
            return View(Skills);
        }
        public async Task<IActionResult> Update(SkillsModel skillsModel, int Id)
        {
            ViewData["Authorized"] = "Employee";
            var EmpCode = HttpContext.Session.GetString("EmpCode");
            ViewData["EmpCode"] = EmpCode;
            var Skills = await skillsInterface.GetByIdAsync(Id);
            if (Skills != null)
            {
                Skills.Id = Id;
                Skills.EmpId = skillsModel.EmpId;
                Skills.EmpSkillTypeId = skillsModel.EmpSkillTypeId;
                Skills.ExperienceInYears = skillsModel.ExperienceInYears;
                Skills.CompanyId = skillsModel.CompanyId;
                Skills.BranchId = skillsModel.BranchId;
                Skills.UpdatedBy = 1;
                Skills.UpdatedDate = DateTime.Now;
                await skillsInterface.UpdateAsync(Skills);
                TempData["AlertScript"] = "Swal.fire('Success!', 'Skills Updated Successfully', 'success');";
            }
            return RedirectToAction("UpdatePage", new { Id = Id });
        }
    }
}
