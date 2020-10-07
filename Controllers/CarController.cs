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
    public class CarController : Controller
    {
        private UserContext _context;
        public CarController(UserContext context)
        {
            _context = context;
        }


        public IActionResult Index(string mark,string color,int? year)
        {
            return View(_context.GetAllCars(mark,color,year));
            //return View(_context.GetAllCars());
        }

        public IActionResult Create()
        {
            ViewBag.Suppliers = new SelectList(_context.GetAllSuppliers(), "Id", "firmname");
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCarViewModel modelvm)
        {
            if (ModelState.IsValid)
            {               
                Car car = new Car() {Mark= modelvm.Mark,Model= modelvm.Model,Color= modelvm.Color,Goverment_number= modelvm.Goverment_number,Year= modelvm.Year,id_supplier= modelvm.id_supplier,Price=modelvm.Price};

                if (_context.AddCar(car)) return RedirectToAction("Index");
                else ModelState.AddModelError("", "Ошибка");

                
            }
            return View(modelvm);
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


        public IActionResult Rent(int id)
        {
            if (_context.FindUser(User.Identity.Name).id_client != 0)
            {
                ViewBag.Banks = new SelectList(_context.GetAllBanks(), "bank_name", "bank_name");
                var model = new RentCarViewModel() { id_car = id };
                return View(model);
            }
            else
            {
                return RedirectToAction("Create","Client");
            }
        }

        [HttpPost]
        public IActionResult Rent(RentCarViewModel model)
        {
            if (ModelState.IsValid)
            {
                Payments pay = new Payments() {date=model.datepay,price=model.price,account_number=model.account_number,payer_number=model.payer_number };
                int idpay = _context.AddPayment(pay);

                Sales sale = new Sales() { date1 = DateTime.Now, id_client = _context.FindUser(User.Identity.Name).id_client, id_car = model.id_car, id_payment = idpay, date2 = model.date2, date3 = model.date3, price = model.price };

                if (_context.AddSale(sale)!=0) return RedirectToAction("Index");
                else ModelState.AddModelError("", "Ошибка");

            }
            return View(model);
        }


        public IActionResult Edit(int id)
        {
            ViewBag.Suppliers = new SelectList(_context.GetAllSuppliers(), "Id", "firmname");
            return View();
        }

        [HttpPost]
        public IActionResult Edit(EditCarViewModel modelvm)
        {
            if (ModelState.IsValid)
            {
                Car car = new Car() { Mark = modelvm.Mark, Model = modelvm.Model, Color = modelvm.Color, Goverment_number = modelvm.Goverment_number, Year = modelvm.Year, id_supplier = modelvm.id_supplier, Price = modelvm.Price };

                if (_context.UpdateCar(car)) return RedirectToAction("Index");
                else ModelState.AddModelError("", "Ошибка");

            }
            return View(modelvm);
        }


    }
}
