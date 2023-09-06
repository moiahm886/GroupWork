using GroupWork.DAL;
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
        private readonly UserInterface userInterface;
        private readonly PermissionInterface permissionInterface;
        private readonly CompanyInterface companyInterface;
        private readonly LookupInterface lookupInterface;

        public AdminController(UserInterface userInterface,PermissionInterface permissionInterface, CompanyInterface companyInterface,LookupInterface lookupInterface )
        {
            this.userInterface = userInterface;
            this.permissionInterface = permissionInterface;
            this.companyInterface = companyInterface;
            this.lookupInterface = lookupInterface;
        }
        public async Task<IActionResult> Users(UserModel userModel)
        {
            var isEmpIdUnique = !await userInterface.Uniqueness(userModel.EmpId);

            if (!isEmpIdUnique)
            {
                TempData["AlertScript"] = "Swal.fire('Error!', 'Employee ID is already associated with a user.', 'error');";
                return RedirectToAction("AddUsers"); 
            }
            var user = new UserModel
            {
                UserName = userModel.UserName,
                UserPassword = userModel.UserPassword,
                IsActive = userModel.IsActive,
                EmpId = userModel.EmpId,
                RoleId = userModel.RoleId,
                CompanyId = userModel.CompanyId,
                BranchId = userModel.BranchId,
                AddedBy = 1,
                AddedDate = DateTime.Now,
            };
            await userInterface.AddUser(user);
            TempData["AlertScript"] = "Swal.fire('Success!', 'User Added Successfully', 'success');";
            ViewData["Authorized"] = "Admin";
            return View("AddUsers");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePermission(PermissionModel permissionModel)
        {
            var existingPermission = permissionInterface.permission(permissionModel.RoleId);

            if (existingPermission != null)
            {
                existingPermission.CanRead = Convert.ToInt32(permissionModel.CanRead == 1 || permissionModel.CanUpdate == 1 || permissionModel.CanDelete == 1 || permissionModel.CanInsert == 1);
                existingPermission.CanUpdate = permissionModel.CanUpdate == 1 ? 1 : 0;
                existingPermission.CanDelete = permissionModel.CanDelete == 1 ? 1 : 0;
                existingPermission.CanInsert = permissionModel.CanInsert == 1 ? 1 : 0;
                existingPermission.UpdatedBy = 1;
                existingPermission.UpdatedDate = DateTime.Now;
                await permissionInterface.UpdatePermission(existingPermission);
                TempData["AlertScript"] = "Swal.fire('Success!', 'Permission Updated Successfully', 'success');";
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
                CompanyId = roleModel.CompanyId,
                BranchId = roleModel.BranchId,
                AddedBy = 1,
                AddedDate  = DateTime.Now,
            };
            await permissionInterface.AddRoles(role);
            TempData["AlertScript"] = "Swal.fire('Success!', 'Role Added Successfully', 'success');";
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
            var user = await userInterface.GetUsers();
            ViewData["Authorized"] = "Admin";
            return View(user);
        }
        public async Task<IActionResult> UpdateUser(UserModel usermodel)
        {
            var user = await userInterface.GetByIdAsync(usermodel.Id);
            if (user != null)
            { 
                user.UserPassword = usermodel.UserPassword;
                user.IsActive = usermodel.IsActive;
                user.RoleId = usermodel.RoleId;
                user.BranchId = usermodel.BranchId;
                user.CompanyId = usermodel.CompanyId;
                user.UpdatedBy = 1;
                user.UpdatedDate = DateTime.Now;
                await userInterface.UpdateUser(user);
            }
            TempData["AlertScript"] = "Swal.fire('Success!', 'User Updated Successfully', 'success');";
            if (user.IsActive==1)
            {
                return RedirectToAction("ManageEmployee", "Employee", new { id = usermodel.EmpId});
            }
            return RedirectToAction("ManageUsers");
        }
        public async Task<IActionResult> DeleteUser(UserModel usermodel)
        {
            var user = await userInterface.GetByIdAsync(usermodel.Id);
            if (user != null)
            {
                await userInterface.DeleteUser(user);
                TempData["AlertScript"] = "Swal.fire('Success!', 'User Deleted Successfully', 'success');";
            }
            return RedirectToAction("ManageUsers");
        }

        [HttpGet]
        public async Task<IActionResult> ViewUser(int id)
        {
            ViewData["Authorized"] = "Admin";
            var user = await userInterface.GetByIdAsync(id);
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
        public async Task<IActionResult> ManageCompany(int Id)
        {
            ViewData["Authorized"] = "Admin";
            if (Id == 0)
            {
                var company = await companyInterface.GetCompany();
                return View(company);
            }
            else
            {
                var company = await companyInterface.GetCompanyListByCountryAsync(Id);
                return View(company);
            }
        }
        public IActionResult AddCompany()
        {
            ViewData["Authorized"] = "Admin";
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
                AddedBy = 1,
                AddedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                UpdatedBy = 1,
            };
            await companyInterface.AddCompany(Company);
            TempData["AlertScript"] = "Swal.fire('Success!', 'Company Added Successfully', 'success');";
            var lookup = new LookupModel
            {
                LookupId = 1,
                LookupName = "Countries",
                IsActive = 1,
                TypeCode = Company.CountryId,
                AddedBy = 1,
                AddedDate = DateTime.Now,
                UpdatedBy = 1,
                UpdatedDate = DateTime.Now,
                CompanyId = Company.Id,
                BranchId = 1,
            };
            await lookupInterface.AddAsync(lookup);
            return RedirectToAction("ManageCompany");
        }
        public async Task<IActionResult> DeleteCompany(int ID)
        {
            var company = await companyInterface.FindCompanyByID(ID);
            if (company != null)
            {
                companyInterface.RemoveCompany(company);
                var branches = await companyInterface.FindBranches(ID);
                if (branches.Count > 0)
                {
                    companyInterface.RemoveBranches(branches);
                }
                var lookup = await lookupInterface.FindByCompanyIdAsync(ID);
                if (lookup != null)
                {
                    await lookupInterface.DeleteAsync(lookup);
                }

                await companyInterface.savechanges();
                TempData["AlertScript"] = "Swal.fire('Success!', 'Company Deleted Successfully', 'success');";
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
            var roles = await permissionInterface.GetRolesAsync();
            var rolePermissions = new Dictionary<int, PermissionModel>();

            foreach (var roleModel in roles)
            {
                var check =  permissionInterface.permission(roleModel.Id);
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
            var branch = await companyInterface.GetBranches();
            ViewData["Authorized"] = "Admin";
            return View(branch);
        }
        public IActionResult AddBranch()
        {
            ViewData["Authorized"] = "Admin";
            return View();
        }
        public async Task<IActionResult> BranchAddition(BranchModel branchModel)
        {
            var branch = await companyInterface.FindBranchThroughCompany(branchModel.CompanyId);
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
                    AddedBy = 1,
                    AddedDate = DateTime.Now
                };
                companyInterface.AddBranch(Branchinfo);
                TempData["AlertScript"] = "Swal.fire('Success!', 'Branch Added Successfully', 'success');";
                return RedirectToAction("ManageBranch");
            }
            else
            {
                return View("AddBranch");
            }
        }
        public async Task<IActionResult> DeleteBranch(int ID)
        {
            var branch = await companyInterface.FindBranchesById(ID);
            if (branch != null)
            {
                await companyInterface.RemoveBranch(branch);
                TempData["AlertScript"] = "Swal.fire('Success!', 'Branch Deleted Successfully', 'success');";
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
                AddedId = 1,
                AddedDate = DateTime.Now,
            };
            var perm = await permissionInterface.FindPermsission(permissionModel.Id);
            if (perm == null)
            {
                await permissionInterface.UpdatePermission(permission);
                TempData["AlertScript"] = "Swal.fire('Success!', 'Permission Added Successfully', 'success');";
            }
            else
            {
                ViewData["permission"] = "Exist";
            }
            return RedirectToAction("ManagePermission");
        }
    }
}
