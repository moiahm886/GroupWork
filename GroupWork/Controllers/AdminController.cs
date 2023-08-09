using GroupWork.Data;
using GroupWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Data;
using System.Diagnostics;

namespace GroupWork.Controllers
{
    public class AdminController : Controller
    {
        private readonly GroupWorkDBContext groupWorkDBContext;

        public AdminController(GroupWorkDBContext groupWorkDBContext)
        {
            this.groupWorkDBContext = groupWorkDBContext;
        }
        public async Task<IActionResult> ApprovalDeletion(AdminAllowanceModelClass adminAllowanceModelClass)
        {
            var approved = await groupWorkDBContext.AdminTable.FindAsync(adminAllowanceModelClass.ID);
            if (approved != null) {
                groupWorkDBContext.AdminTable.Remove(approved);
                await groupWorkDBContext.SaveChangesAsync();
                return RedirectToAction("ApprovedView");
            }
            var request = await groupWorkDBContext.Registration.FindAsync(adminAllowanceModelClass.UserID);
            return RedirectToAction("ApprovedView");
        }
        public IActionResult Employees()
        {
            return RedirectToAction("ManageEmployee");
        }
        public IActionResult ManageEmployee() {
            List<RegistrationModelClass> requests = groupWorkDBContext.Registration.ToList();
            var employeeRequests = requests.Where(r => r.RoleID == 1).ToList();
            return View(employeeRequests);
        }
        public IActionResult Users() {
            return RedirectToAction("ManageUser");
        }
        public IActionResult ManageUser() {
            List<RegistrationModelClass> requests = groupWorkDBContext.Registration.ToList();
            var UserRequests = requests.Where(r => r.RoleID == 2).ToList();
            return View(UserRequests);
        }

        public IActionResult Company()
        {
            return RedirectToAction("ManageCompany");
        }
        public IActionResult ManageCompany()
        {
            List<RegistrationModelClass> requests = groupWorkDBContext.Registration.ToList();
            var CompanyRequests = requests.Where(r => r.RoleID == 3).ToList();
            return View(CompanyRequests);
        }

        public IActionResult CompanyBranch()
        {
            return RedirectToAction("ManageCompanyBranch");
        }
        public IActionResult ManageCompanyBranch()
        {
            List<RegistrationModelClass> requests = groupWorkDBContext.Registration.ToList();
            var BranchRequests = requests.Where(r => r.RoleID == 4).ToList();
            return View(BranchRequests);
        }
        public async Task<IActionResult> ManageApproval(AdminAllowanceModelClass adminAllowanceModelClass,string currentView) {
            var approval = new AdminAllowanceModelClass()
            {
                UserID = adminAllowanceModelClass.UserID,
                UserName = adminAllowanceModelClass.UserName,
                UserPassword = adminAllowanceModelClass.UserPassword,
                IsActive = adminAllowanceModelClass.IsActive,
                AddedBy = adminAllowanceModelClass.AddedBy,
                AddedDate = adminAllowanceModelClass.AddedDate,
                UpdatedBy = adminAllowanceModelClass.UpdatedBy,
                UpdatedDate = adminAllowanceModelClass.UpdatedDate,
            };
            var request = await groupWorkDBContext.Registration.FindAsync(approval.UserID);
            if (request != null) { 
                request.AdminApproval = true;
            }
            var check = await groupWorkDBContext.AdminTable.FindAsync(approval.UserID);
            if (check == null)
            {
                await groupWorkDBContext.AdminTable.AddAsync(approval);
            }
            await groupWorkDBContext.SaveChangesAsync();
            return RedirectToAction(currentView);
        }
        public async Task<IActionResult> ManageRejection(AdminAllowanceModelClass admin, string currentView)
        {
            var found = await groupWorkDBContext.Registration.FindAsync(admin.ID);
            if (found != null)
            {
                groupWorkDBContext.Registration.Remove(found);
                await groupWorkDBContext.SaveChangesAsync();
                return RedirectToAction(currentView);
            }
            return RedirectToAction(currentView);
        } 
        public async Task<IActionResult> ApprovedView()
        {
            var Approved = await groupWorkDBContext.AdminTable.ToListAsync();
            return View(Approved);
        }
    }
}
