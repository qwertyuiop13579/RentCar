using Labs.Models;
using Labs.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.Controllers
{
    public class BankController : Controller
    {
        private UserContext _context;
        public BankController(UserContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.GetAllBanks()) ;
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CreateBankViewModel model)
        {

            if (ModelState.IsValid)
            {
                Bank bank = _context.FindBank(model.bank_name);
                if (bank == null)
                {
                    _context.AddCountry(new Country() { Name = model.country });
                    _context.AddRegion(new Region() { Name = model.region });
                    _context.AddDistrict(new District() { Name = model.district });
                    _context.AddCity(new City() { Name = model.city });
                    _context.AddStreet(new Street() { Name = model.street });
                    var address = new Address() { id_country = _context.FindCountry(model.country).Id, id_region = _context.FindRegion(model.region).Id, id_district = _context.FindDistrict(model.district).Id,
                        type1 = model.type1, id_city = _context.FindCity(model.city).Id, type2 = model.type2, id_street = _context.FindStreet(model.street).Id, num1 = model.num1, num2 = model.num2, num3 = model.num3, index = model.index,
                        num4 = model.num4, code = model.code, mobile = model.mobile, email = model.email
                    };
                    _context.AddAddress(address);

                    bank = new Bank { bank_name=model.bank_name,innclient=model.innclient,bank_account=model.bank_account,id_address=_context.FindAddress(address).Id,bank_bik=model.bank_bik };
                    if(_context.AddBank(bank)) return RedirectToAction("Index");
                    else ModelState.AddModelError("", "Ошибка");

                }
                else ModelState.AddModelError("", "Такой банк уже существует");
            }
            return View(model);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            Bank bank= _context.FindBank(id);
            if (bank != null)
            {
                bool result = _context.DeleteBank(id);
            }
            return RedirectToAction("Index");
        }


    }
}
