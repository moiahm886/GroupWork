using GroupWork.Data;
using GroupWork.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Azure.Core;
using System.Diagnostics;

namespace GroupWork.Controllers
{
    public class RegistrationController : Controller
    {
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
        private readonly GroupWorkDBContext Db;
     
        public RegistrationController(GroupWorkDBContext DB)
        {
            Db = DB;
        }

        public IActionResult LoginView()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ManageRegistration(RegistrationModelClass RMC) {
            if (await Db.Registration.AnyAsync(r => r.UserName == RMC.UserName))
            {
                ModelState.AddModelError("UserName", "Username is already taken.");
                return View("RegistrationView", RMC);
            }
            var register = new RegistrationModelClass()
            {
                ID = RMC.ID,
                UserName = RMC.UserName,
                UserPassword = HashPassword(RMC.UserPassword),
                RoleName = RMC.RoleName,
                AdminApproval = false,
            };
            if(RMC.RoleName == "Employee")
            {
                register.RoleID = 1;
            }
            else if (RMC.RoleName == "User")
            {
                register.RoleID = 2;
            }
            else if (RMC.RoleName == "Company")
            {
                register.RoleID = 3;
            }
            else if (RMC.RoleName == "CompanyBranch")
            {
                register.RoleID = 4;
            }
            await Db.Registration.AddAsync(register);
            await Db.SaveChangesAsync();
            return RedirectToAction("LoginView");
        }
        public IActionResult RegistrationView() { 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(RegistrationModelClass RMC)
        {
            if(RMC.UserPassword=="Admin" && RMC.UserName == "Admin")
            {
                return RedirectToAction("AdminView");
            }
            var User = await Db.Registration.FirstOrDefaultAsync(r=>r.UserName == RMC.UserName);
            if (User == null)
            {
                ModelState.AddModelError("UserName", "Username is not found.");
                return RedirectToAction("LoginView");
            }
            else 
            {
                if (User.UserPassword == HashPassword(RMC.UserPassword))
                {
                    if (!User.AdminApproval)
                    {
                        ViewData["IsWaitingAdminApproval"] = true;
                        ModelState.AddModelError("AdminApproval", "Waiting For Admin Approval");
                    }
                    else
                    {
                        return View();
                    }
                }
                return RedirectToAction("LoginView");
            }
        }
        public IActionResult AdminView()
        {
            return View();
        }

    }
}
