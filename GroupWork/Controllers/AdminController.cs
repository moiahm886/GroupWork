using GroupWork.Data;
using GroupWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace GroupWork.Controllers
{
    public class AdminController : Controller
    {
        private readonly ManagementDataContextClass managementDataContextClass;

        public AdminController(ManagementDataContextClass managementDataContextClass)
        {
            this.managementDataContextClass = managementDataContextClass;
        }
        public async Task<IActionResult> Users(UserModel userModel)
        {
            var user = new UserModel
            {
                UserName = userModel.UserName,
                UserPassword = userModel.UserPassword,
                IsActive = userModel.IsActive,
                EmpId = userModel.EmpId,
                RoleId = userModel.RoleId,
                CompanyId = userModel.CompanyId,
                BranchId = userModel.BranchId,
                AddedBy = userModel.AddedBy,
                AddedDate = userModel.AddedDate,
                UpdatedBy = userModel.UpdatedBy,
                UpdatedDate = userModel.UpdatedDate,
            };
            await managementDataContextClass.tbUsers.AddAsync(user);
            await managementDataContextClass.SaveChangesAsync();
            ViewData["Authorized"] = "Admin";
            return View("AddUsers");
        }
        public IActionResult AddUsers()
        {
            ViewData["Authorized"] = "Admin";
            return View();
        }
        public async Task<IActionResult> Roles(RoleModel roleModel)
        {
            var role = new RoleModel
            {
                Name = roleModel.Name,
                Description = roleModel.Description,
                IsActive = roleModel.IsActive,
                CompanyId   =roleModel.CompanyId,
                BranchId = roleModel.BranchId,
                AddedBy =roleModel.AddedBy,
                AddedDate  =roleModel.AddedDate,
                UpdatedById =roleModel.UpdatedById,
                UpdatedDate =roleModel.UpdatedDate
            };
            await managementDataContextClass.tbRoles.AddAsync(role);
            await managementDataContextClass.SaveChangesAsync();
            ViewData["Authorized"] = "Admin";
            return View("AddRoles");
        }
        public IActionResult AddRoles()
        {
            ViewData["Authorized"] = "Admin";
            return View();
        }
        public IActionResult ManageUsers()
        {
            ViewData["Authorized"] = "Admin";
            return View();
        }
        public IActionResult ManageEmployees()
        {
            ViewData["Authorized"] = "Admin";
            return View();
        }
        public async Task<IActionResult> ManagePermission()
        {
            var role = await managementDataContextClass.tbRoles.ToListAsync();
            ViewData["Authorized"] = "Admin";
            return View(role);
        }
        public IActionResult Logout()
        {
            return RedirectToAction("LoginView", "User");
        }
        public async Task<IActionResult> Permission(PermissionModel permissionModel)
        {
            var permission = new PermissionModel
            {
                RoleId = permissionModel.RoleId,
                MenuId = permissionModel.MenuId,
                ScreenId = permissionModel.ScreenId,
                CanUpdate = permissionModel.CanUpdate,
                CanInsert = permissionModel.CanInsert,
                CanRead = permissionModel.CanRead,
                CanDelete = permissionModel.CanDelete,
                BranchId = permissionModel.BranchId,
                CompanyId = permissionModel.CompanyId,
                AddedId = permissionModel.AddedId,
                AddedDate = permissionModel.AddedDate,
                UpdatedBy = permissionModel.UpdatedBy,
                UpdatedDate = permissionModel.UpdatedDate,
            };
            await managementDataContextClass.tbPermissions.AddAsync(permission);
            await managementDataContextClass.SaveChangesAsync();
            var perm = await managementDataContextClass.tbPermissions.FindAsync(permission.RoleId);
            if (perm != null)
            {
                ViewData["Permission"] = "Active";
            }
            return View("ManagePermission");
        }
    }
}
