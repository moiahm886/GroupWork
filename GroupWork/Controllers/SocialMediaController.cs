using GroupWork.DAL;
using GroupWork.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Reflection.Emit;

namespace GroupWork.Controllers
{
    public class SocialMediaController : Controller
    {
        private readonly EmployeeInterface employeeInterface;
        private readonly SocialInterface socialInterface;
        private readonly PermissionInterface permissionInterface;

        public SocialMediaController(EmployeeInterface employeeInterface,SocialInterface socialInterface,PermissionInterface permissionInterface)
        {
            this.employeeInterface = employeeInterface;
            this.socialInterface = socialInterface;
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
        public async Task<IActionResult> AddSocialMedia()
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
        public async Task<IActionResult> Add(SocialMediaModel socialMediaModel)
        {

            var social = new SocialMediaModel
            {
                EmpId = socialMediaModel.EmpId,
                FacebookLink = socialMediaModel.FacebookLink,
                InstagramLink = socialMediaModel.InstagramLink,
                TwitterLink = socialMediaModel.TwitterLink,
                LinkedinLink = socialMediaModel.LinkedinLink,
                WebsiteLink = socialMediaModel.WebsiteLink,
                OtherLink = socialMediaModel.OtherLink,
                CompanyId = socialMediaModel.CompanyId,
                BranchId = socialMediaModel.BranchId,
                AddedBy = 1,
                AddedDate = DateTime.Now
            };
            await socialInterface.AddAsync(social);
            return RedirectToAction("AddSocialMedia");
        }
        public async Task<IActionResult> ViewSocialMedia()
        {
            CheckPermission();
            ViewData["Authorized"] = "Employee";
            var EmpCode = HttpContext.Session.GetString("EmpCode");
            ViewData["EmpCode"] = EmpCode;
            if (EmpCode != null)
            {
                var Employee = await employeeInterface.getEmployee(EmpCode);
                var SocialMedia = await socialInterface.GetByEmpId(Employee.Id);
                if(SocialMedia!= null)
                {
                    return View(SocialMedia);
                }
                else
                {
                    return RedirectToAction("AddSocialMedia");
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
            var SocialMedia = await socialInterface.GetByIdAsync(Id);
            return View(SocialMedia);
        }
        public async Task<IActionResult> Update(SocialMediaModel socialMediaModel ,int Id)
        {
            ViewData["Authorized"] = "Employee";
            var EmpCode = HttpContext.Session.GetString("EmpCode");
            ViewData["EmpCode"] = EmpCode;
            var SocialMedia = await socialInterface.GetByIdAsync(Id);
            if(SocialMedia != null)
            {
                SocialMedia.Id = Id;
                SocialMedia.EmpId = socialMediaModel.EmpId;
                SocialMedia.FacebookLink = socialMediaModel.FacebookLink;
                SocialMedia.InstagramLink = socialMediaModel.InstagramLink;
                SocialMedia.TwitterLink = socialMediaModel.TwitterLink;
                SocialMedia.LinkedinLink = socialMediaModel.LinkedinLink;
                SocialMedia.WebsiteLink = socialMediaModel.WebsiteLink;
                SocialMedia.OtherLink = socialMediaModel.OtherLink;
                SocialMedia.CompanyId = socialMediaModel.CompanyId;
                SocialMedia.BranchId = socialMediaModel.BranchId;
                SocialMedia.UpdatedBy = 1;
                SocialMedia.UpdatedDate = DateTime.Now;
                await socialInterface.UpdateAsync(SocialMedia);
                TempData["AlertScript"] = "Swal.fire('Success!', 'Socials Updated Successfully', 'success');";
            }
            return RedirectToAction("UpdatePage", new { Id = Id });
        }
    }
}
