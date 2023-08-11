using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserCrud.DataBaseAccess;
using UserCrud.Model;

namespace UserCrud.Controller
{
    public class UserController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly DataBaseContext _context;
        public UserController(DataBaseContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index(DataBaseContext _context)
        {
            var user = await _context.Users.ToListAsync();
            return View(user);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public  async Task<IActionResult> Add(AddUserViewModel model)
        {
            var user = new UserModel()
            {
                UserName = model.UserName,
                UserPassword = model.UserPassword,
                IsActive = model.IsActive,
                EmpId = model.EmpId,
                RoleId = model.RoleId,
                CompanyId = model.CompanyId,
                BranchId = model.BranchId,
                AddedBy = model.AddedBy,
                AddedDate = model.AddedDate,
                UpdatedBy = model.UpdatedBy,
                UpdatedDate = model.UpdatedDate
            };
             await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Add");
        }
        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            { 
            var ViewUser = new UpdateUserViewModel()
            {
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
                return await Task.Run( () => View(ViewUser));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateUserViewModel model)
        {
            var user = await _context.Users.FindAsync(model.Id);
            if (user != null)
            {
                user.UserName = model.UserName;
                user.UserPassword= model.UserPassword;  
                user.IsActive = model.IsActive;
                user.EmpId = model.EmpId;
                user.RoleId = model.RoleId;
                user.CompanyId = model.CompanyId;
                user.BranchId = model.BranchId;
                user.AddedBy = model.AddedBy;
                user.AddedDate = model.AddedDate;
                user.UpdatedBy = model.UpdatedBy;
                user.UpdatedDate = model.UpdatedDate;  
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateUserViewModel model)
        {
            var user = await _context.Users.FindAsync(model.Id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
    }

