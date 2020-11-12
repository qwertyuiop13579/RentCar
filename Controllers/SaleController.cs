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


        public IActionResult IndexBySupplier()
        {
            int? id_cl = _context.FindUser(User.Identity.Name).id_client;

            if (id_cl == null) return RedirectToAction("Create", "Client");

            Supplier supp = _context.FindSupplierByClient(id_cl.Value);
            if(supp==null) return RedirectToAction("Create", "Supplier");
            int id_supp =supp.Id;
            return View(_context.GetSalesBySupplier(id_supp));
            

            
        }

        public IActionResult IndexByCar(int id_car)
        {
            return View(_context.GetSalesByCar(id_car));
        }

        public IActionResult IndexByClient()
        {
            int? id_cl = _context.FindUser(User.Identity.Name).id_client;
            if (id_cl != null)
            {
                return View(_context.GetSalesByClient(id_cl.Value));
            }
            else
            {
                return RedirectToAction("Create", "Client");
            }
        }



        public IActionResult Create(int id_c)
        {
            //if(User.Identity.Name==null) return RedirectToAction("Create", "Client");
            int? id_cl = _context.FindUser(User.Identity.Name).id_client;
            if (id_cl != null)
            {
                var model = new CreateSaleViewModel() { id_car = id_c };
                return View(model);
            }
            else
            {
                return RedirectToAction("Create", "Client");
            }
        }

        [HttpPost]
        public IActionResult Create(CreateSaleViewModel model)
        {
            if (ModelState.IsValid)
            {
                Payment pay = new Payment() { date = model.datepay, price = model.price, account_number = model.account_number, payer_number = model.payer_number };
                int idpay = _context.AddPayment(pay);

                Sale sale = new Sale() { date1 = DateTime.Now, id_client = _context.FindUser(User.Identity.Name).id_client.Value, id_car = model.id_car, id_payment = idpay, date2 = model.date2, date3 = model.date3, price = model.price, status = "Обрабатывается" };

                int canadd = _context.CanAddSale(sale);
                if (canadd == 1)     //проверка на корректность
                {
                    ModelState.AddModelError("", "Неверное время.");
                    return View(model);
                }
                else if (canadd == 2)    //проверка на занятость
                {
                    ModelState.AddModelError("", "Автомобиль забронирован.");
                    return View(model);
                }

                int id_sale = _context.AddSale(sale); ;
                if (id_sale != 0)
                {
                    sale = _context.FindSale(id_sale);
                    _context.CreateEventStatusCar(sale);
                    return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError("", "Ошибка");

            }
            return View(model);
        }


        public IActionResult Delete(int id_sale)
        {
            Sale sale = _context.FindSale(id_sale);
            if (sale != null)
            {
                _context.DeleteSale(id_sale);
                _context.DropEventStatusCar(sale);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Cancel(int id_sale)
        {
            Sale sale = _context.FindSale(id_sale);
            if (sale != null)
            {
                _context.UpdateSaleStatus(id_sale, "Отменён");
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Confirm(int id_sale)
        {
            Sale sale = _context.FindSale(id_sale);
            if (sale != null)
            {
                _context.UpdateSaleStatus(id_sale, "Подтверждён");
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }



        public IActionResult Edit(int id_sale)
        {
            Sale sale = _context.FindSale(id_sale);
            Payment pay = _context.FindPayment(sale.id_payment);
            if (_context.FindUser(User.Identity.Name).id_client != 0)
            {
                var model = new EditSaleViewModel()
                {
                    Id = sale.Id,
                    date1 = sale.date1,
                    id_client = _context.FindUser(User.Identity.Name).id_client.Value,
                    id_car = sale.id_car,
                    date2 = sale.date2,
                    date3 = sale.date3,
                    price = sale.price,
                    datepay = pay.date,
                    account_number = pay.account_number,
                    payer_number = pay.account_number,
                    status = sale.status,
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("Create", "Client");
            }
        }

        [HttpPost]
        public IActionResult Edit(EditSaleViewModel model)
        {
            if (ModelState.IsValid)
            {
                Payment pay = new Payment() { date = model.datepay, price = model.price, account_number = model.account_number, payer_number = model.payer_number };
                int idpay = _context.AddPayment(pay);

                Sale sale = new Sale() { date1 = DateTime.Now, id_client = _context.FindUser(User.Identity.Name).id_client.Value, id_car = model.id_car, id_payment = idpay, date2 = model.date2, date3 = model.date3, price = model.price, status = model.status };

                if (_context.UpdateSale(sale)) return RedirectToAction("Index");
                else ModelState.AddModelError("", "Ошибка");

            }
            return View(model);
        }

    }
}
