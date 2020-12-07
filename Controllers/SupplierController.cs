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
    public class SupplierController : Controller
    {
        private UserContext _context;
        public SupplierController(UserContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View(_context.GetAllSuppliers());
        }

        public ActionResult Create()
        {
            int? id_cl = _context.FindUser(User.Identity.Name).id_client;

            if (id_cl == null)
            {
                return RedirectToAction("Create", "Client");
            }
            else
            {
                Supplier supp = _context.FindSupplierByClient(id_cl.Value);
                if (supp == null) return View();
                else
                {
                    return RedirectToAction("Edit", "Supplier", new { id = supp.Id });
                }
            }
        }

        [HttpPost]
        public IActionResult Create(CreateSupplierViewModel model)
        {
            if (ModelState.IsValid)
            {
                var address = new Address()
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


                int id_cl = _context.FindUser(User.Identity.Name).id_client.Value;

                Supplier supp = new Supplier() { firmname = model.firmname, id_address = id_addr, unn = model.unn, id_client = id_cl };

                int id_supp = _context.AddSupplier(supp);
                if (id_supp != 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError("", "Ошибка");




            }
            return View(model);
        }


        public IActionResult Edit(int id)
        {
            Supplier supp = _context.FindSupplier(id);
            Address addr = _context.FindAddress(supp.id_address);
            EditSupplierViewModel model = new EditSupplierViewModel
            {
                Id = supp.Id,
                firmname = supp.firmname,
                unn = supp.firmname,

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

                id_client=supp.id_client,
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult Edit(EditSupplierViewModel model)
        {
            if (ModelState.IsValid)
            {
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
                int id_cl = model.id_client;
                Supplier supp = new Supplier()
                {
                    Id=model.Id,
                    firmname = model.firmname,
                    id_address = id_addr,             
                    unn = model.unn,
                    id_client = id_cl,
                };

                if (_context.UpdateSupplier(supp))
                {
                    return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError("", "Ошибка");
            }
            return View(model);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            Supplier supp = _context.FindSupplier(id);
            if (supp != null)
            {
                _context.DeleteSupplier(id);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
