using System.Security.Claims;
using JIWGrandAlpha.Domain.ViewModels.User;
using JIWGrandAlpha.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace JIWGrandAlpha.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService=accountService;
        }
        [HttpGet]
        public IActionResult Login() => View();
        [HttpGet]
        public IActionResult LoginT() => View();

        [HttpPost]
        public async Task<IActionResult> Login(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.LoginStudent(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));
                    return RedirectToAction("Student","Journal");
                }
                ModelState.AddModelError("",response.Description);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> LoginT(TeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.LoginTeacher(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));
                    return RedirectToAction("ChooseGroup","Journal");
                }
                ModelState.AddModelError("",response.Description);
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult GetGroups()
        {
            var groups = _accountService.GetGroups();
            return Json(groups.Data);
        }

        [HttpPost]
        public JsonResult GetTeachers()
        {
            var teachers = _accountService.GetTeachers();
            return Json(teachers.Data);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Account");
        }
    }
}
