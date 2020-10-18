using Labs.Models;
using Labs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.IO;
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


        public IActionResult Index(string mark, string color, int? year)
        {
            return View(_context.GetAllCars(mark, color, year));
        }

        public IActionResult IndexBySupplier()
        {
            int id_cl = _context.FindUser(User.Identity.Name).id_client.Value;
            int id_supp = _context.FindSupplierByClient(id_cl).Id;
            return View(_context.GetAllCars(id_supp));
        }

        [Authorize]
        public IActionResult Create()
        {
            int id_cl = _context.FindUser(User.Identity.Name).id_client.Value;
            int id_supp = _context.FindSupplierByClient(id_cl).Id;
            if (id_cl == 0)
            {
                return RedirectToAction("Create", "Client");
            }
            else if (id_supp == 0)
            {
                return RedirectToAction("Create", "Supplier");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCarViewModel modelvm)
        {
            if (ModelState.IsValid)
            {
                int id_cl = _context.FindUser(User.Identity.Name).id_client.Value;
                int id_supp = _context.FindSupplierByClient(id_cl).Id;
                Car car = new Car()
                {
                    Mark = modelvm.Mark,
                    Model = modelvm.Model,
                    Color = modelvm.Color,
                    Goverment_number = modelvm.Goverment_number,
                    Year = modelvm.Year,
                    id_supplier = id_supp,
                    Price = modelvm.Price,
                    status = "Свободен",
                    country = modelvm.country,
                    city = modelvm.city
                };

                if (modelvm.Image != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(modelvm.Image.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)modelvm.Image.Length);

                    }
                    car.Image = Convert.ToBase64String(imageData); ;
                    car.ImageMimeType = modelvm.Image.ContentType;
                }
                if (_context.AddCar(car)) return RedirectToAction("Index", "Home");
                else ModelState.AddModelError("", "Ошибка");
            }
            return View(modelvm);
        }



        public FileContentResult GetImage(int id)
        {
            Car car = _context.FindCar(id);
            if (car != null && car.Image != null && car.ImageMimeType != "")
            {
                byte[] bytes = Convert.FromBase64String(car.Image);
                return File(bytes, car.ImageMimeType);
            }
            else
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images");
                byte[] mas = System.IO.File.ReadAllBytes(path + @"\NoCar.png");
                string file_type = "image/png";
                string file_name = "NoCar.png";
                return File(mas, file_type, file_name);
            }
        }

        public FileContentResult GetImageFromString(string im,string type)
        {
            if (im != null && type != null)
            {
                byte[] bytes = Convert.FromBase64String(im);
                return File(bytes, type);
            }
            else
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images");
                byte[] mas = System.IO.File.ReadAllBytes(path + @"\NoCar.png");
                string file_type = "image/png";
                string file_name = "NoCar.png";
                return File(mas, file_type, file_name);
            }
        }


        public IActionResult Delete(int id)
        {
            Car car = _context.FindCar(id);

            if (car != null)
            {
                _context.DeleteCar(id);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Details(int id)
        {
            Car car = _context.FindCar(id);
            if (car != null)
            {
                return View(car);
            }
            return NotFound();
        }

        /*
        [Authorize]
        public IActionResult Rent(int id)
        {
            if (_context.FindUser(User.Identity.Name).id_client != 0)
            {
                var model = new RentCarViewModel() { id_car = id };
                return View(model);
            }
            else
            {
                return RedirectToAction("Create", "Client");
            }
        }

        [HttpPost]
        public IActionResult Rent(RentCarViewModel model)
        {
            if (ModelState.IsValid)
            {
                Payment pay = new Payment() { date = model.datepay, price = model.price, account_number = model.account_number, payer_number = model.payer_number };
                int idpay = _context.AddPayment(pay);

                Sale sale = new Sale() { date1 = DateTime.Now, id_client = _context.FindUser(User.Identity.Name).id_client.Value, id_car = model.id_car, id_payment = idpay, date2 = model.date2, date3 = model.date3, price = model.price, status = "Обрабатывается" };

                if (_context.AddSale(sale) != 0)
                {
                    _context.CreateEventStatusCar(sale);
                    return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError("", "Ошибка");

            }
            return View(model);
        }
        */


        [Authorize]
        public IActionResult Edit(int id_car)
        {
            Car car = _context.FindCar(id_car);


            byte[] bytes = Convert.FromBase64String(car.Image);
            var stream = new MemoryStream(bytes);
            IFormFile file = new FormFile(stream, 0, bytes.Length, "name", "fileName");

            EditCarViewModel model = new EditCarViewModel
            {
                Id = car.Id,
                Mark = car.Mark,
                Model = car.Model,
                Color = car.Color,
                Goverment_number = car.Goverment_number,
                Year = car.Year,
                id_supplier = car.id_supplier,
                Price = car.Price,
                status = car.status,
                country = car.country,
                city = car.city,
                Image = file,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditCarViewModel modelvm)
        {
            if (ModelState.IsValid)
            {
                Car car = new Car()
                {
                    Id = modelvm.Id,
                    Mark = modelvm.Mark,
                    Model = modelvm.Model,
                    Color = modelvm.Color,
                    Goverment_number = modelvm.Goverment_number,
                    Year = modelvm.Year,
                    id_supplier = modelvm.id_supplier,
                    Price = modelvm.Price,
                    status = modelvm.status,
                    country = modelvm.country,
                    city = modelvm.city
                };

                if (modelvm.Image != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(modelvm.Image.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)modelvm.Image.Length);

                    }
                    car.Image = Convert.ToBase64String(imageData); ;
                    car.ImageMimeType = modelvm.Image.ContentType;
                    if (_context.UpdateCar(car)) return RedirectToAction("IndexBySupplier", "Car");
                    else ModelState.AddModelError("", "Ошибка");
                }
                else
                {
                    if (_context.UpdateCarWithoutImage(car)) return RedirectToAction("IndexBySupplier", "Car");
                    else ModelState.AddModelError("", "Ошибка");
                }
            }
            return View(modelvm);
        }


    }
}
