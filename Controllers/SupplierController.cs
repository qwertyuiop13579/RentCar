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
    public class SupplierController : Controller
    {
        private UserContext _context;
        public SupplierController(UserContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View(_context.GetAllSuppliers());
        }

        public IActionResult Create()
        {
            ViewBag.Banks= new SelectList(_context.GetAllBanks(), "bank_name", "bank_name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateSupplierViewModel model)
        {
            if (ModelState.IsValid)
            {
                Bank bank = _context.FindBank(model.bank_name);
                if (bank != null)
                {
                    _context.AddCountry(new Country() { Name = model.country });
                    _context.AddRegion(new Region() { Name = model.region });
                    _context.AddDistrict(new District() { Name = model.district });
                    _context.AddCity(new City() { Name = model.city });
                    _context.AddStreet(new Street() { Name = model.street });
                    var address = new Address()
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

                    Supplier supp = new Supplier() {firmname=model.firmname,id_address=_context.FindAddress(address).Id,unn=model.unn,id_bank_details=bank.Id };

                    if (_context.AddSupplier(supp)) return RedirectToAction("Index");
                    else ModelState.AddModelError("", "Ошибка");

                }
                else ModelState.AddModelError("", "Такого банка не существует");
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
            return RedirectToAction("Index");
        }

    }
}
