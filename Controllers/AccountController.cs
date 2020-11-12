using Labs.Models;
using Labs.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Labs.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserContext _context;
        public string ConnectionString { get; set; }
        public AccountController(UserContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _context.FindUser(model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    user = new User { Email = model.Email, Password = UserContext.HashPassword(model.Password) };
                    if (model.issupplier)
                    {
                        Role userRole = _context.FindRole("supplier");
                        if (userRole != null)
                        {
                            user.id_role = userRole.Id;
                        }
                    }
                    else
                    {
                        Role userRole = _context.FindRole("user");
                        if (userRole != null)
                        {
                            user.id_role = userRole.Id;
                        }
                    }

                    Status status = _context.FindStatus("notblock");
                    if (status != null)
                    {
                        user.id_status = status.Id;
                    }

                    user.dateofregistration = DateTime.Now;
                    user.id_client = null;
                    user.dateofbeginblock = new DateTime();
                    user.dateofendbock = new DateTime();
                    _context.AddNewUser(user);

                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Login");
                }
            }
            else ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                (string status, string hash) = _context.Login(model.Email);
                if (hash != null)
                {
                    bool result = UserContext.VerifyHashedPassword(hash, model.Password);
                    if (result == true)
                    {
                        if (status != null)
                        {
                            if (status == "block")
                            {
                                ModelState.AddModelError("", "Пользователь заблокирован");
                            }
                            else
                            {
                                await Authenticate(_context.FindUser(model.Email)); // аутентификация

                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                    }
                    else ModelState.AddModelError("", "Неверный пароль");
                }
                else ModelState.AddModelError("", "Нет такого пользователя");
            }
            return View(model);
        }
        private async Task Authenticate(User user)
        {
            // создаем claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, _context.FindRole(user.id_role).Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
