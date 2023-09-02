using GroupWork.DAL;
using GroupWork.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace GroupWork.Controllers
{
    public class QualificationController : Controller
    {
        private readonly EmployeeInterface employeeInterface;
        private readonly QualificationInterface qualificationInterface;
        private readonly PermissionInterface permissionInterface;

        public QualificationController(EmployeeInterface employeeInterface, QualificationInterface qualificationInterface, PermissionInterface permissionInterface)
        {
            this.employeeInterface = employeeInterface;
            this.qualificationInterface = qualificationInterface;
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
        public async Task<IActionResult> AddQualifications()
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
        public async Task<IActionResult> Add(QualificationModel qualificationModel)
        {
            var qualification = new QualificationModel
            {
                EmpId = qualificationModel.EmpId,
                EmpDegreeName = qualificationModel.EmpDegreeName,
                EmpStartYear = qualificationModel.EmpStartYear,
                EmpEndYear = qualificationModel.EmpEndYear,
                EmpInstituteName = qualificationModel.EmpInstituteName,
                EmpInstituteCountryId = qualificationModel.EmpInstituteCountryId,
                EmpInstituteCityId = qualificationModel.EmpInstituteCityId,
                EmpObtaineGPAPercentage = qualificationModel.EmpObtaineGPAPercentage,
                EmpQuilficationTypeId = qualificationModel.EmpQuilficationTypeId,
                EmpDescription = qualificationModel.EmpDescription,
                CompanyId = qualificationModel.CompanyId,
                BranchId = qualificationModel.BranchId,
                AddedBy = 1,
                AddedDate = DateTime.Now
            };
            await qualificationInterface.AddAsync(qualification);
            return RedirectToAction("AddQualifications");
        }
        public async Task<IActionResult> ViewQualifications()
        {
            CheckPermission();
            ViewData["Authorized"] = "Employee";
            var EmpCode = HttpContext.Session.GetString("EmpCode");
            ViewData["EmpCode"] = EmpCode;
            if (EmpCode != null)
            {
                var Employee = await employeeInterface.getEmployee(EmpCode);
                var qualification = await qualificationInterface.GetByEmpId(Employee.Id);
                if (qualification != null)
                {
                    return View(qualification);
                }
                else
                {
                    return RedirectToAction("AddQualifications");
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
            var qualification = await qualificationInterface.GetByIdAsync(Id);
            return View(qualification);
        }
        public async Task<IActionResult> Update(QualificationModel qualificationModel, int Id)
        {
            ViewData["Authorized"] = "Employee";
            var EmpCode = HttpContext.Session.GetString("EmpCode");
            ViewData["EmpCode"] = EmpCode;
            var qualification = await qualificationInterface.GetByIdAsync(Id);
            if (qualification != null)
            {
                qualification.EmpId = qualificationModel.EmpId;
                qualification.EmpDegreeName = qualificationModel.EmpDegreeName;
                qualification.EmpStartYear = qualificationModel.EmpStartYear;
                qualification.EmpEndYear = qualificationModel.EmpEndYear;
                qualification.EmpInstituteName = qualificationModel.EmpInstituteName;
                qualification.EmpInstituteCountryId = qualificationModel.EmpInstituteCountryId;
                qualification.EmpInstituteCityId = qualificationModel.EmpInstituteCityId;
                qualification.EmpObtaineGPAPercentage = qualificationModel.EmpObtaineGPAPercentage;
                qualification.EmpQuilficationTypeId = qualificationModel.EmpQuilficationTypeId;
                qualification.EmpDescription = qualificationModel.EmpDescription;
                qualification.CompanyId = qualificationModel.CompanyId;
                qualification.BranchId = qualificationModel.BranchId;
                qualification.UpdatedBy = 1;
                qualification.UpdatedDate = DateTime.Now;
                await qualificationInterface.UpdateAsync(qualification);
                TempData["AlertScript"] = "Swal.fire('Success!', 'Qualifications Updated Successfully', 'success');";
            }
            return RedirectToAction("UpdatePage", new { Id = Id });
        }
    }
}
