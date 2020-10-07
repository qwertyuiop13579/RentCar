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
    public class SaleController : Controller
    {
        private UserContext _context;
        public SaleController(UserContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View(_context.GetAllSales());
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Car car = _context.FindCar(id);
            if (car != null)
            {
                _context.DeleteCar(id);
            }
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            if (_context.FindUser(User.Identity.Name).id_client != 0)
            {
                ViewBag.Banks = new SelectList(_context.GetAllBanks(), "bank_name", "bank_name");
                var model = new EditRentViewModel() { id_car = id };
                return View(model);
            }
            else
            {
                return RedirectToAction("Create", "Client");
            }
        }

        [HttpPost]
        public IActionResult Edit(EditRentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Payments pay = new Payments() { date = model.datepay, price = model.price, account_number = model.account_number, payer_number = model.payer_number };
                int idpay = _context.AddPayment(pay);

                Sales sale = new Sales() { date1 = DateTime.Now, id_client = _context.FindUser(User.Identity.Name).id_client, id_car = model.id_car, id_payment = idpay, date2 = model.date2, date3 = model.date3, price = model.price };

                if (_context.UpdateSale(sale)) return RedirectToAction("Index");
                else ModelState.AddModelError("", "Ошибка");

            }
            return View(model);
        }

    }
}
