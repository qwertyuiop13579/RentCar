using Labs.Models;
using Labs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private UserContext _context;
        public ClientController(UserContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View(_context.GetAllClients());
        }


        public IActionResult Create()
        {
            if (_context.FindUser(User.Identity.Name).id_client == null)
            {
                return View();
            }
            else return RedirectToAction("Edit", "Client", new { id = _context.FindUser(User.Identity.Name).id_client });

        }

        [HttpPost]
        public IActionResult Create(CreateClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                Passport pass = new Passport()
                {
                    passport1 = model.passport1,
                    passport2 = model.passport2,
                    passport3 = model.passport3,
                    date1 = model.date1,
                    date2 = model.date2,
                    authority = model.authority,
                    sex = model.sex,
                    date3 = model.date3,
                    surname = model.surname,
                    name = model.name,
                    patronymic = model.patronymic,
                };
                int idpass = _context.AddPassport(pass);

                Address address = new Address()
                {
                    country = model.country,
                    type1 = model.type1,
                    city = model.city,
                    type2 = model.type2,
                    street = model.street,
                    numhouse = Convert.ToInt32(model.numhouse),
                    numapartment = Convert.ToInt32(model.numapartment),
                    index = model.index,
                    housephone = model.housephone,
                    mobilephone = model.mobilephone,
                    email = model.email,

                };
                int id_addr = _context.AddAddress(address);


                Client client = new Client()
                {
                    id_passport = idpass,
                    id_address = id_addr,
                };


                int id_cl = _context.AddClient(client);
                if (id_cl != 0)
                {
                    User user = _context.FindUser(User.Identity.Name);
                    user.id_client = id_cl;
                    _context.UpdateUser(user.Id, user);
                    return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError("", "Ошибка");

            }
            return View(model);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            Client client = _context.FindClient(id);
            if (client != null)
            {
                _context.DeleteClient(id);
            }
            return RedirectToAction("Index");
        }



        public ActionResult Details(int id)
        {
            Client client = _context.FindClient(id);
            Passport pass = _context.FindPassport(client.id_passport);
            Address addr = _context.FindAddress(client.id_address);

            ClientDetails cldet = new ClientDetails() { surname = pass.surname, name = pass.name, patronymic = pass.patronymic, country = addr.country, city = addr.city, mobilephone = addr.mobilephone, email = addr.email };

            if (client != null)
            {
                return View(cldet);
            }
            return NotFound();
        }



        public IActionResult Edit(int id)
        {
            Client client = _context.FindClient(id);
            if (client == null)
            {
                return NotFound();
            }
            Passport pass = _context.FindPassport(client.id_passport);
            Address addr = _context.FindAddress(client.id_address);
            EditClientViewModel model = new EditClientViewModel
            {
                Id = client.Id,
                passport1 = pass.passport1,
                passport2 = pass.passport2,
                passport3 = pass.passport3,
                date1 = pass.date1,
                date2 = pass.date2,
                authority = pass.authority,
                sex = pass.sex,
                date3 = pass.date3,
                surname = pass.surname,
                name = pass.name,
                patronymic = pass.patronymic,


                country = addr.country,
                type1 = addr.type1,
                city = addr.city,
                type2 = addr.type2,
                street = addr.street,
                numhouse = addr.numhouse,
                numapartment = addr.numapartment,
                index = addr.index,
                housephone = addr.housephone,
                mobilephone = addr.mobilephone,
                email = addr.email,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                Passport pass = new Passport()
                {
                    passport1 = model.passport1,
                    passport2 = model.passport2,
                    passport3 = model.passport3,
                    date1 = model.date1,
                    date2 = model.date2,
                    authority = model.authority,
                    sex = model.sex,
                    date3 = model.date3,
                    surname = model.surname,
                    name = model.name,
                    patronymic = model.patronymic,
                };
                int idpass = _context.AddPassport(pass);
                Address address = new Address()
                {
                    country = model.country,
                    type1 = model.type1,
                    city = model.city,
                    type2 = model.type2,
                    street = model.street,
                    numhouse = Convert.ToInt32(model.numhouse),
                    numapartment = Convert.ToInt32(model.numapartment),
                    index = model.index,
                    housephone = model.housephone,
                    mobilephone = model.mobilephone,
                    email = model.email,
                };
                int id_addr = _context.AddAddress(address);

                int id_cl = model.Id;
                Client client = new Client()
                {
                    Id = id_cl,
                    id_passport = idpass,
                    id_address = id_addr,
                };

                if (_context.UpdateClient(client))
                {
                    User user = _context.FindUser(User.Identity.Name);
                    user.id_client = id_cl;
                    _context.UpdateUser(user.Id, user);
                    return RedirectToAction("Index");
                }
                else ModelState.AddModelError("", "Ошибка");


            }
            return View(model);
        }
    }
}
