using Labs.Models;
using Labs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        private List<string> AllMarks;
        public CarController(UserContext context)
        {
            _context = context;
            AllMarks = "Acura Alfa Romeo Audi BMW Cadillac Chevrolet Chrysler Citroen Daewoo Dodge Fiat Ford Geely Great Wall Honda Hyundai Infiniti Jaguar Jeep Kia Lada Lancia Land Rover Lexus Mazda Mercedes-Benz Mitsubishi Nissan Opel Peugeot Porsche Renault Rover Saab SEAT Skoda SsangYong Subaru Suzuki Toyota Volkswagen Volvo".Split(" ").ToList();
            AllMarks.Insert(0, "Все");
        }


        public IActionResult Index(string mark, int? yearmin, int? yearmax)
        {
            if (AllMarks[0] != "Все") AllMarks.Insert(0, "Все");
            ViewBag.Marks = new SelectList(AllMarks);
            
            if(string.IsNullOrEmpty(mark)) return View(_context.GetAllCars());
            return View(_context.GetAllCars(mark, yearmin, yearmax));
        }

        public IActionResult IndexBySupplier(string mark, int? yearmin, int? yearmax)
        {
            int? id_cl = _context.FindUser(User.Identity.Name).id_client;
            if (id_cl == null)
            {
                return RedirectToAction("Create", "Client");
            }
            else
            {
                Supplier supp = _context.FindSupplierByClient(id_cl.Value);
                if (supp == null) return RedirectToAction("Create", "Supplier");
                else
                {
                    int id_supp = supp.Id;
                    ViewBag.Marks = new SelectList(AllMarks);
                    return View(_context.GetAllCars(id_supp));
                }
            }
        }

        [Authorize]
        public IActionResult Create()
        {
            int? id_cl = _context.FindUser(User.Identity.Name).id_client;
            
            if (id_cl == null)
            {
                return RedirectToAction("Create", "Client");
            }
            else 
            {
                Supplier supp = _context.FindSupplierByClient(id_cl.Value);
                if (supp == null) return RedirectToAction("Create", "Supplier");
                else
                {
                    if (AllMarks[0] == "Все") AllMarks.RemoveAt(0);
                    ViewBag.Marks = new SelectList(AllMarks);
                    return View();
                }
            }
            
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
                    city = modelvm.city,
                    Image = null,
                    ImageMimeType = null,
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
            else
            {
                ModelState.AddModelError("", "Ошибка");
            }
            if (AllMarks[0] == "Все") AllMarks.RemoveAt(0);
            ViewBag.Marks = new SelectList(AllMarks);
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
            if (AllMarks[0] == "Все") AllMarks.RemoveAt(0);
            ViewBag.Marks = new SelectList(AllMarks);
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
            if (AllMarks[0] == "Все") AllMarks.RemoveAt(0);
            ViewBag.Marks = new SelectList(AllMarks);
            return View(modelvm);
        }


    }
}
