using GroupWork.Data;
using GroupWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Data;
using System.Security;

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
        [HttpPost]
        public async Task<IActionResult> UpdatePermission(PermissionModel permissionModel)
        {
            var existingPermission = await managementDataContextClass.tbPermissions.FirstOrDefaultAsync(r => r.RoleId == permissionModel.RoleId);

            if (existingPermission != null)
            {
                existingPermission.CanRead = Convert.ToInt32(permissionModel.CanRead == 1 || permissionModel.CanUpdate == 1 || permissionModel.CanDelete == 1 || permissionModel.CanInsert == 1);
                existingPermission.CanUpdate = permissionModel.CanUpdate == 1 ? 1 : 0;
                existingPermission.CanDelete = permissionModel.CanDelete == 1 ? 1 : 0;
                existingPermission.CanInsert = permissionModel.CanInsert == 1 ? 1 : 0;
                managementDataContextClass.tbPermissions.Update(existingPermission);
                await managementDataContextClass.SaveChangesAsync();
            }
            return RedirectToAction("ManagePermission");
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
        public async Task<IActionResult> ManageUsers()
        {
            var user = await managementDataContextClass.tbUsers.ToListAsync();
            ViewData["Authorized"] = "Admin";
            return View(user);
        }
        public async Task<IActionResult> UpdateUser(UserModel usermodel)
        {
            var user = await managementDataContextClass.tbUsers.FirstOrDefaultAsync(x => x.Id == usermodel.Id);
            if (user != null)
            { 
                user.UserPassword = usermodel.UserPassword;
                user.IsActive = usermodel.IsActive;
                user.RoleId = usermodel.RoleId;
                user.BranchId = usermodel.BranchId;
                user.CompanyId = usermodel.CompanyId;
                user.UpdatedBy = usermodel.UpdatedBy;
                user.UpdatedDate = DateTime.Now;
                managementDataContextClass.tbUsers.Update(user);
                await managementDataContextClass.SaveChangesAsync();
            }
            return RedirectToAction("ManageUsers");
        }
        public async Task<IActionResult> DeleteUser(UserModel usermodel)
        {
            var user = await managementDataContextClass.tbUsers.FirstOrDefaultAsync(x => x.Id == usermodel.Id);
            if(user!= null)
            {
                managementDataContextClass.tbUsers.Remove(user);
                await managementDataContextClass.SaveChangesAsync();
            }
            return RedirectToAction("ManageUsers");
        }
        [HttpGet]
        public async Task<IActionResult> ViewUser(int id)
        {
            ViewData["Authorized"] = "Admin";
            var user = await  managementDataContextClass.tbUsers.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                var Viewuser = new UserModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    UserPassword = user.UserPassword,
                    IsActive = user.IsActive,
                    EmpId = user.EmpId,
                    RoleId = user.RoleId,
                    CompanyId = user.CompanyId,
                    BranchId = user.BranchId,
                    AddedBy = user.AddedBy,
                    AddedDate = user.AddedDate,
                    UpdatedBy = user.UpdatedBy,
                    UpdatedDate = user.UpdatedDate
                };
                return View(Viewuser);
            }
            return RedirectToAction("ManageUsers");
        }
        public async Task<IActionResult> ManageCompany()
        {
            ViewData["Authorized"] = "Company";
            var company = await managementDataContextClass.Company.ToListAsync();
            return View(company);
        }
        public IActionResult AddCompany()
        {
            ViewData["Authorized"] = "Company";
            return View();
        }
        public async Task<IActionResult> CompanyAddition(CompanyModel companyModel)
        {
            var Company = new CompanyModel
            {
                Name = companyModel.Name,
                Address = companyModel.Address,
                Email = companyModel.Email,
                ContactNo = companyModel.ContactNo,
                FaxNo = companyModel.FaxNo,
                Logo = companyModel.Logo,
                CountryId = companyModel.CountryId,
                CityId = companyModel.CityId,
                BankName = companyModel.BankName,
                BankIban = companyModel.BankIban,
                IsActive = companyModel.IsActive,
                AddedBy = companyModel.AddedBy,
                AddedDate = DateTime.Now,
                UpdatedBy = companyModel.UpdatedBy,
                UpdatedDate = DateTime.Now
            };
            await managementDataContextClass.Company.AddAsync(Company);
            await managementDataContextClass.SaveChangesAsync();
            return RedirectToAction("ManageCompany");
        }
        public async Task<IActionResult> DeleteCompany(int ID)
        {
            var company = await managementDataContextClass.Company.FindAsync(ID);

            if (company != null)
            {
                managementDataContextClass.Company.Remove(company);
                var branches = await managementDataContextClass.CompanyBranch.Where(b => b.CompanyId == ID).ToListAsync();
                if (branches.Count > 0)
                {
                    managementDataContextClass.CompanyBranch.RemoveRange(branches);
                }
                await managementDataContextClass.SaveChangesAsync();
            }
            return RedirectToAction("ManageCompany");
        }

        public IActionResult ManageEmployees()
        {
            ViewData["Authorized"] = "Admin";
            return View();
        }
        public async Task<IActionResult> ManagePermission()
        {
            var roles = await managementDataContextClass.tbRoles.ToListAsync();
            var rolePermissions = new Dictionary<int, PermissionModel>();

            foreach (var roleModel in roles)
            {
                var check = await managementDataContextClass.tbPermissions.FirstOrDefaultAsync(r => r.RoleId == roleModel.Id);
                if (check != null)
                {
                    rolePermissions[roleModel.Id] = check;
                }
            }
            ViewData["Authorized"] = "Admin";
            return View((roles, rolePermissions));
        }
        public IActionResult Logout()
        {
            return RedirectToAction("LoginView", "User");
        }
        public async Task<IActionResult> ManageBranch()
        {
            if (TempData.ContainsKey("Message"))
            {
                ViewData["Added"] = TempData["Message"];
            }
            var branch = await managementDataContextClass.CompanyBranch.ToListAsync();
            ViewData["Authorized"] = "Branch";
            return View(branch);
        }
        public IActionResult AddBranch()
        {
            ViewData["Authorized"] = "Branch";
            return View();
        }
        public async Task<IActionResult> BranchAddition(BranchModel branchModel)
        {
            var branch = await managementDataContextClass.Company.FirstOrDefaultAsync(r=>r.Id==branchModel.CompanyId);
            if (branch != null)
            {
                var Branchinfo = new BranchModel
                {
                    CompanyId = branchModel.CompanyId,
                    Name = branchModel.Name,
                    CityId = branchModel.CityId,
                    CountryId = branchModel.CountryId,
                    Address = branchModel.Address,
                    Phone = branchModel.Phone,
                    AddedBy = branchModel.AddedBy,
                    AddedDate = branchModel.AddedDate,
                    UpdatedBy = branchModel.UpdatedBy,
                    UpdatedDate = branchModel.UpdatedDate,

                };
                await managementDataContextClass.CompanyBranch.AddAsync(Branchinfo);
                await managementDataContextClass.SaveChangesAsync();
                TempData["Message"] = "Done";
                return RedirectToAction("ManageBranch");
            }
            else
            {
                return View("AddBranch");
            }
        }
        public async Task<IActionResult> DeleteBranch(int ID)
        {
            var branch = await managementDataContextClass.CompanyBranch.FindAsync(ID);
            if (branch != null)
            {
                managementDataContextClass.CompanyBranch.Remove(branch);
                await managementDataContextClass.SaveChangesAsync();
            }
            return RedirectToAction("ManageBranch");
        }
        public async Task<IActionResult> Permission(PermissionModel permissionModel)
        {
            var permission = new PermissionModel
            {
                RoleId = permissionModel.Id,
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
            var perm = await managementDataContextClass.tbPermissions.FirstOrDefaultAsync(r => r.RoleId == permissionModel.Id);
            if (perm == null)
            {
                await managementDataContextClass.tbPermissions.AddAsync(permission);
                await managementDataContextClass.SaveChangesAsync();
            }
            else
            {
                ViewData["permission"] = "Exist";
            }
            return RedirectToAction("ManagePermission");
        }
    }
}
