using Microsoft.AspNetCore.Mvc;

namespace GroupWork.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Employees()
        {
            return RedirectToAction("ManageEmployee");
        }
        public IActionResult ManageEmployee() {
            return View();
        }
        public IActionResult Users() {
            return RedirectToAction("ManageUser");
        }
        public IActionResult ManageUser() {
            return View();  
        }

        public IActionResult Company()
        {
            return RedirectToAction("ManageCompany");
        }
        public IActionResult ManageCompany()
        {
            return View();
        }

        public IActionResult CompanyBranch()
        {
            return RedirectToAction("ManageCompanyBranch");
        }
        public IActionResult ManageCompanyBranch()
        {
            return View();
        }

    }
}
