using GroupWork.Models;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using GroupWork.Data;
using Microsoft.EntityFrameworkCore;
using GroupWork.DAL;

namespace GroupWork.Controllers
{
    public class UserController : Controller
    {
        private readonly UserInterface userInterface;
        private readonly EmployeeInterface employeeInterface;

        public UserController(UserInterface userInterface, EmployeeInterface employeeInterface)
        {
            this.userInterface = userInterface;
            this.employeeInterface = employeeInterface;
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
            var user = await userInterface.GetUserByUsernameAsync(userModel.UserName);
            if (userModel.UserName == "Admin" && userModel.UserPassword == "Admin")
            {
                ViewData["Authorized"] = "Admin";
                return RedirectToAction("AdminDashboard");
            }
            else
            {
                var User = await userInterface.GetUserByUsernameAsync(userModel.UserName);
                if (User == null)
                {
                    ViewData["LoginResult"] = "User Does not exist or incorrect password";
                    return View("LoginView");
                }
                else if(userModel.UserPassword!=User.UserPassword)
                {
                    ViewData["LoginResult"] = "User Does not exist or incorrect password";
                    return View("LoginView");
                }
                else if(user.IsActive==0)
                {
                    ViewData["Username"] = userModel.UserName;
                    return RedirectToAction("UserDashboard", new { username = userModel.UserName });
                }
                else if(user.IsActive == 1)
                {
                    if (user != null)
                    {
                        var EmpId = user.EmpId;
                        var employeeExists = await employeeInterface.existingEmployee(EmpId);
                        if (employeeExists)
                        {
                            ViewData["Authorized"] = "Employee";
                            return RedirectToAction("EmployeeDashboard","Employee",new {EmpCode = user.EmpId});
                        }
                        else
                        {
                            ViewData["Username"] = userModel.UserName;
                            return RedirectToAction("UserDashboard", new { username = userModel.UserName });
                        }
                    }
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
                var obj = await userInterface.GetUserByUsernameAsync(user);
                if (obj != null)
                {
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
