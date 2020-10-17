using Labs.Models;
using Labs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Labs.Controllers
{

    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View(_context.GetAllUsers());

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CreateUserViewModel model)
        {

            if (ModelState.IsValid)
            {
                User user = _context.FindUser(model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    user = new User { Email = model.Email, Password = UserContext.HashPassword(model.Password) };
                    Role userRole = _context.FindRole("user");
                    if (userRole != null)
                    {
                        user.id_role = userRole.Id;
                    }
                    user.dateofregistration = DateTime.Now;
                    _context.AddNewUser(user);

                }
                else ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            User user = _context.FindUser(Convert.ToInt32(id));
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _context.FindUser(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    bool result = _context.UpdateUser(user.Id, user);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {

                        ModelState.AddModelError(string.Empty, "Ошибка");

                    }
                }
            }
            return View(model);
        }

        public IActionResult ChangeRole(int id)
        {
            User user = _context.FindUser(Convert.ToInt32(id));
            if (user == null)
            {
                return NotFound();
            }
            var userrole = _context.FindRole(user.id_role);
            var allroles = _context.GetAllRoles();
            ViewBag.AllRoles = allroles;
            ChangeRoleViewModel model = new ChangeRoleViewModel { Id = user.Id, Email = user.Email, RoleName = userrole.Name };
            return View(model);
        }

        [HttpPost]
        public IActionResult ChangeRole(ChangeRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _context.FindUser(model.Id);
                if (user != null)
                {
                    Role role = _context.FindRole(model.RoleName);

                    bool result = _context.UpdateRole(user.Id, role);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {

                        ModelState.AddModelError(string.Empty, "Ошибка");

                    }
                }
            }
            return View(model);
        }


        public IActionResult ChangeStatus(int id)
        {
            User user = _context.FindUser(Convert.ToInt32(id));
            if (user == null)
            {
                return NotFound();
            }
            var userstatus = _context.FindStatus(user.id_status);
            var allstatuses = _context.GetAllStatuses();
            ViewBag.AllStatuses = allstatuses;
            ChangeStatusViewModel model = new ChangeStatusViewModel { Id = user.Id, Email = user.Email, StatusName = userstatus.Name, Dateofendblock = DateTime.Now };
            return View(model);
        }

        [HttpPost]
        public IActionResult ChangeStatus(ChangeStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _context.FindUser(model.Id);

                if (user != null)
                {
                    bool result;
                    if (model.StatusName == "block")  
                    {
                        if(model.IsForever==true)        //заблокировать навсегда
                        {
                            user.id_status = _context.FindStatus(model.StatusName).Id;
                            result = _context.BlockUserForever(user.Id, user);
                        }
                        else                     //заблокировать до времени
                        {
                            user.id_status = _context.FindStatus(model.StatusName).Id;
                            user.dateofbeginblock = DateTime.Now;
                            user.dateofendbock = model.Dateofendblock;
                            result = _context.BlockUser(user.Id,user);
                        }                 
                    }
                    else          //notblock
                    {
                        user.id_status = _context.FindStatus(model.StatusName).Id;
                        result = _context.UnblockUser(user.Id,user);
                    }

                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ошибка");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Ошибка");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            User user = _context.FindUser(id);
            if (user != null)
            {
                bool result = _context.DeleteUser(id);
            }
            return RedirectToAction("Index");
        }
    }
}

