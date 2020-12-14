using Labs.Models;
using Labs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        [Authorize(Roles = "manager,admin")]
        public IActionResult Index()
        {
            return View(_context.GetAllSales());
        }

        [Authorize(Roles = "supplier,admin")]
        public IActionResult IndexBySupplier()
        {
            int? id_cl = _context.FindUser(User.Identity.Name).id_client;

            if (id_cl == null) return RedirectToAction("Create", "Client");

            Supplier supp = _context.FindSupplierByClient(id_cl.Value);
            if (supp == null) return RedirectToAction("Create", "Supplier");
            int id_supp = supp.Id;
            return View(_context.GetSalesBySupplier(id_supp));



        }

        [Authorize(Roles = "supplier,admin")]
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
            int? id_cl = _context.FindUser(User.Identity.Name).id_client;
            
            if (id_cl != null)
            {
                Car car = _context.FindCar(id_c);
                var model = new CreateSaleViewModel() { id_car = id_c, rentprice = car.Price };
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
                Sale sale = new Sale() {date1 = DateTime.Now, id_client = _context.FindUser(User.Identity.Name).id_client.Value, id_car = model.id_car, id_payment = null, date2 = model.date2, date3 = model.date3, summ = model.summ, status = "Обрабатывается" };

                int canadd = _context.CanAddSale(sale);
                if (canadd == 1)     //проверка на корректность
                {
                    ModelState.AddModelError("", "Неверное время аренды.");
                    return View(model);
                }
                else if (canadd == 2)    //проверка на занятость
                {
                    ModelState.AddModelError("", "В это время автомобиль забронирован.");
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


        public ActionResult Payment(int id)
        {
            Sale sale = _context.FindSale(id);
            Car car = _context.FindCar(sale.id_car);
            if (sale.id_payment != null)
            {
                Payment pay = _context.FindPayment(sale.id_payment.Value);
                var model = new PaymentViewModel()
                {
                    id_sale = sale.Id,
                    date = pay.date,
                    amount = pay.amount,
                    withdrawAmount = pay.withdrawAmount,
                    sender = pay.sender,
                    operation_Id = pay.operation_Id
                };
                return View(model);
            }
            else return NotFound();


            ;
        }


        public IActionResult Cancel(int id_sale)
        {
            Sale sale = _context.FindSale(id_sale);
            if (sale != null)
            {
                _context.UpdateSaleStatus(id_sale, "Отменён");
                return RedirectToAction("IndexByClient", "Sale");
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

        public IActionResult Restore(int id_sale)
        {
            Sale sale = _context.FindSale(id_sale);
            if (sale != null && sale.status=="Отменён")
            {
                _context.UpdateSaleStatus(id_sale, "Обрабатывается");
                return RedirectToAction("IndexByClient", "Sale");
            }
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Pay(int id_sale)
        {
            Sale sale = _context.FindSale(id_sale);
            Car car = _context.FindCar(sale.id_car);
            if (sale != null)
            {
                PayViewModel payModel = new PayViewModel { SaleId = sale.Id, Sum = (decimal)sale.summ,Model=car.Model,Mark=car.Mark,date2=sale.date2,date3=sale.date3 };
                return View(payModel);
            }
            return NotFound();
        }


        [HttpGet]
        public IActionResult Paid()
        {
            //key = DfJ/9D0wTHyVOrL+B0sUQBGF           
            return View();
        }

        [HttpPost]
        public void Paid(string notification_type, string operation_id, string label, string datetime, decimal amount, decimal withdraw_amount, string sender, string sha1_hash, string currency, bool codepro)
        {
            string key = "DfJ/9D0wTHyVOrL+B0sUQBGF"; // секретный код
            string paramString = String.Format("{0}&{1}&{2}&{3}&{4}&{5}&{6}&{7}&{8}", notification_type, operation_id, amount, currency, datetime, sender, codepro.ToString().ToLower(), key, label);
            string paramStringHash1 = GetHash(paramString);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase; // создаем класс для сравнения строк

            if (0 == comparer.Compare(paramStringHash1, sha1_hash)) // если хэши идентичны, добавляем данные о заказе в бд
            {
                Payment pay = new Payment() { date = Convert.ToDateTime(datetime), operation_Id = operation_id, sender = sender, amount = amount, withdrawAmount = withdraw_amount };
                int id_pay = _context.AddPayment(pay);
                Sale sale = _context.FindSale(Convert.ToInt32(label));
                sale.id_payment = id_pay;
                sale.status = "Оплачено";
                _context.UpdateSale(sale);
            }
        }

        public string GetHash(string val)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] data = sha.ComputeHash(Encoding.Default.GetBytes(val));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }



        public IActionResult Edit(int id_sale)
        {
            Sale sale = _context.FindSale(id_sale);
            Car car = _context.FindCar(sale.id_car);
            var model = new EditSaleViewModel()
            {
                Id = sale.Id,
                id_client = _context.FindUser(User.Identity.Name).id_client.Value,
                id_car = sale.id_car,
                date2 = sale.date2,
                date3 = sale.date3,
                rentprice = car.Price,
                status = sale.status,
                summ=sale.summ,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditSaleViewModel model)
        {
            if (ModelState.IsValid)
            {
                Sale sale = new Sale() { Id=model.Id,date1 = DateTime.Now, id_client = _context.FindUser(User.Identity.Name).id_client.Value, id_car = model.id_car, id_payment = null, date2 = model.date2, date3 = model.date3, summ = model.summ, status = model.status };

                int canadd = _context.CanEditSale(sale);
                if (canadd == 1)     //проверка на корректность
                {
                    ModelState.AddModelError("", "Неверное время аренды.");
                    return View(model);
                }
                else if (canadd == 2)    //проверка на занятость
                {
                    ModelState.AddModelError("", "В это время автомобиль забронирован.");
                    return View(model);
                }



                if (_context.UpdateSale(sale)) return RedirectToAction("Index","Home");
                else ModelState.AddModelError("", "Ошибка");

            }
            return View(model);
        }

    }
}
