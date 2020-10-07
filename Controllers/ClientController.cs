using Labs.Models;
using Labs.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.Controllers
{
    public class ClientController : Controller
    {
        private UserContext _context;
        public ClientController(UserContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View(_context.GetAllClients());
        }


        public IActionResult Create()
        {
            if (_context.FindUser(User.Identity.Name).id_client == 0)
            {
                ViewBag.Banks = new SelectList(_context.GetAllBanks(), "bank_name", "bank_name");
                return View();
            }
            else return RedirectToAction("Edit", "Client",   new { id = _context.FindUser(User.Identity.Name).id_client } );

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
                    date3 = model.date3
                };
                if (_context.FindName1(model.Name1) == null)
                {
                    _context.AddName1(model.Name1);
                }
                pass.id_name1 = _context.FindName1(model.Name1).Id;
                if (_context.FindName2(model.Name2) == null)
                {
                    _context.AddName2(model.Name2);
                }
                pass.id_name2 = _context.FindName2(model.Name2).Id;
                if (!string.IsNullOrEmpty(model.Name3))
                {
                    if (_context.FindName3(model.Name3) == null)
                    {
                        _context.AddName3(model.Name3);
                    }
                    pass.id_name3 = _context.FindName3(model.Name3).Id;
                }
                int idpass = _context.AddPassport(pass);

                _context.AddCountry(new Country() { Name = model.country });
                _context.AddRegion(new Region() { Name = model.region });
                _context.AddDistrict(new District() { Name = model.district });
                _context.AddCity(new City() { Name = model.city });
                _context.AddStreet(new Street() { Name = model.street });
                Address address = new Address()
                {
                    id_country = _context.FindCountry(model.country).Id,
                    id_region = _context.FindRegion(model.region).Id,
                    id_district = _context.FindDistrict(model.district).Id,
                    type1 = model.type1,
                    id_city = _context.FindCity(model.city).Id,
                    type2 = model.type2,
                    id_street = _context.FindStreet(model.street).Id,
                    num1 = model.num1,
                    num2 = model.num2,
                    num3 = model.num3,
                    index = model.index,
                    num4 = model.num4,
                    code = model.code,
                    mobile = model.mobile,
                    email = model.email
                };
                _context.AddAddress(address);


                Clients client = new Clients()
                {
                    //id_passport = _context.FindPassport(pass).Id,
                    id_passport = idpass,
                    id_address = _context.FindAddress(address).Id,
                    id_bank_details = _context.FindBank(model.bank_name).Id,
                };



                if (_context.AddClient(client) != 0)
                {
                    User user = _context.FindUser(User.Identity.Name);
                    user.id_client = _context.FindClient(client).Id;
                    _context.UpdateUser(user.Id, user);
                    return RedirectToAction("Index");
                }
                else ModelState.AddModelError("", "Ошибка");

            }
            return View(model);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            Clients client = _context.FindClient(id);
            if (client != null)
            {
                _context.DeleteClient(id);
            }
            return RedirectToAction("Index");
        }



        public IActionResult Edit(int id)
        {
            Clients client = _context.FindClient(id);
            if (client == null)
            {
                return NotFound();
            }
            Passport pass = _context.FindPassport(client.id_passport);
            Address addr = _context.FindAddress(client.id_address);
            Bank bank = _context.FindBank(client.id_bank_details);
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
                Name1 = _context.FindName1(pass.id_name1).Name,
                Name2 = _context.FindName2(pass.id_name2).Name,
                Name3 = _context.FindName1(pass.id_name3).Name,


                country = _context.FindCountry(addr.id_country).Name,
                region = _context.FindRegion(addr.id_region).Name,
                district = _context.FindDistrict(addr.id_district).Name,
                type1 = addr.type1,
                city = _context.FindCity(addr.id_city).Name,
                type2 = addr.type2,
                street = _context.FindStreet(addr.id_street).Name,
                num1 = addr.num1,
                num2 = addr.num2,
                num3 = addr.num3,
                index = addr.index,
                num4 = addr.num4,
                code = addr.code,
                mobile = addr.mobile,
                email = addr.email,

                bank_name = bank.bank_name,
            };
            ViewBag.Banks = new SelectList(_context.GetAllBanks(), "bank_name", "bank_name");
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
                    date3 = model.date3
                };
                if (_context.FindName1(model.Name1) == null)
                {
                    _context.AddName1(model.Name1);
                }
                pass.id_name1 = _context.FindName1(model.Name1).Id;
                if (_context.FindName2(model.Name2) == null)
                {
                    _context.AddName2(model.Name2);
                }
                pass.id_name2 = _context.FindName2(model.Name2).Id;
                if (!string.IsNullOrEmpty(model.Name3))
                {
                    if (_context.FindName3(model.Name3) == null)
                    {
                        _context.AddName3(model.Name3);
                    }
                    pass.id_name3 = _context.FindName3(model.Name3).Id;
                }
                int idpass = _context.AddPassport(pass);
                Address address = new Address()
                {
                    id_country = _context.FindCountry(model.country).Id,
                    id_region = _context.FindRegion(model.region).Id,
                    id_district = _context.FindDistrict(model.district).Id,
                    type1 = model.type1,
                    id_city = _context.FindCity(model.city).Id,
                    type2 = model.type2,
                    id_street = _context.FindStreet(model.street).Id,
                    num1 = model.num1,
                    num2 = model.num2,
                    num3 = model.num3,
                    index = model.index,
                    num4 = model.num4,
                    code = model.code,
                    mobile = model.mobile,
                    email = model.email
                };
                _context.AddAddress(address);


                Clients client = new Clients()
                {
                    id_passport = idpass,
                    id_address = _context.FindAddress(address).Id,
                    id_bank_details = _context.FindBank(model.bank_name).Id,
                };
                /*
                _context.UpdateClient(client);
                User user = _context.FindUser(User.Identity.Name);
                user.id_client = _context.FindClient(client).Id;
                _context.UpdateUser(user.Id, user);
                return RedirectToAction("Index");
                */
                
                if (_context.UpdateClient(client))
                {
                    User user = _context.FindUser(User.Identity.Name);
                    user.id_client = _context.FindClient(client).Id;
                    _context.UpdateUser(user.Id, user);
                    return RedirectToAction("Index");
                }
                else ModelState.AddModelError("", "Ошибка");
                

            }
            return View(model);
        }

    }

}
