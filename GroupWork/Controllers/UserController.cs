using GroupWork.Models;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using GroupWork.Data;
using Microsoft.EntityFrameworkCore;

namespace GroupWork.Controllers
{
    public class UserController : Controller
    {
        private readonly ManagementDataContextClass managementDataContextClass;

        public UserController(ManagementDataContextClass managementDataContextClass)
        {
            this.managementDataContextClass = managementDataContextClass;
        }
        public IActionResult RegistrationView()
        {
            return View();
        }
        public IActionResult LoginView()
        {
            return View();
        }
        public async Task<IActionResult> ManageLogin(UserModel userModel)
        {
            if (userModel.UserName == "Admin" && userModel.UserPassword == "Admin")
            {
                ViewData["Authorized"] = "Admin";
                return RedirectToAction("AdminDashboard");
            }
            else
            {
                var User = await managementDataContextClass.tbUsers.FirstOrDefaultAsync(r => r.UserName == userModel.UserName);
                if (User == null)
                {
                    ViewData["LoginResult"] = "User Does not exist";
                    return View("LoginView");
                }
                else if(userModel.UserPassword!=User.UserPassword)
                {
                    ViewData["LoginResult"] = "Incorrect Password";
                    return View("LoginView");
                }
                else if(userModel.IsActive==0)
                {
                    ViewData["Username"] = userModel.UserName;
                    return RedirectToAction("UserDashboard", new { username = userModel.UserName });
                }
                else if(userModel.IsActive == 1)
                {
                    ViewData["Authorized"] = "Employee";
                    return View("EmployeeDashboard");
                }
                return View("LoginView");
            }
        }
        public IActionResult UserDashboard(string username)
        {
            HttpContext.Session.SetString("UserName", username);
            ViewData["Authorized"] = "GuestUser";
            ViewData["Username"] = username;
            return View();
        }
        public IActionResult AdminDashboard()
        {
            ViewData["Authorized"] = "Admin";
            return View();
        }
        public async Task<IActionResult> UserInfo()
        {
            var user = HttpContext.Session.GetString("UserName");
            ViewData["Authorized"] = "GuestUser";
            ViewData["Username"] = user;
            if (user != null) {
                var obj = await managementDataContextClass.tbUsers.FirstOrDefaultAsync(u => u.UserName == user);
                if (obj != null) {
                    return View(obj);
                }
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return View("LoginView");
        }
    }
}
