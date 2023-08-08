using GroupWork.Models;
using Microsoft.AspNetCore.Mvc;

namespace GroupWork.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult LoginView()
        {
            return View();
        }
        public IActionResult RegistrationView() { 
            return View();
        }
        [HttpPost]
        public IActionResult Login(RegistrationModelClass RMC)
        {
            if(RMC.UserPassword=="Admin" && RMC.UserName == "Admin")
            {
                ViewData["IsAdminLogin"] = true;
                return RedirectToAction("AdminView");
            }
            return RedirectToAction("LoginView");
        }
        public IActionResult AdminView()
        {
            return View();
        }
    }
}
