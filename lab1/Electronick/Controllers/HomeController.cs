using Electronick.Data;
using Electronick.Models;
using Electronick.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Electronick.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {

            var devices = _dbContext.Devices
            .Include(b => b.Parameter)
            .Include(b => b.Category)
            .Select(b => new DeviceViewModel(b))
                .ToList();
            var parameters = _dbContext.Parameters
                .Include(a => a.Devices)
                .ToList();
            var categories = _dbContext.Categories
                .Include(a => a.Devices)
                .ToList();
            ViewBag.Parameters = parameters;
            ViewBag.Categories = categories;

            return View(devices);
        }
        // Додавання девайсу
        public IActionResult Create()
        {
            PopulateDropDowns(); // Метод для заповнення списків параметрів і категорій
            return View();
        }

        [HttpPost]
        public IActionResult Create(DeviceViewModel devicesViewModel)
        {
            if (ModelState.IsValid)
            {
                var device = new Device(devicesViewModel);
                _dbContext.Devices.Add(device);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Якщо ModelState.IsValid не пройшов, потрібно знову заповнити дані для відображення
            PopulateDropDowns();
            return View(devicesViewModel);
        }

        private void PopulateDropDowns()
        {
            ViewBag.Parameters = _dbContext.Parameters.ToList();
            ViewBag.Categories = _dbContext.Categories.ToList();
        }

        //Додавання виробника
        public IActionResult CreateParameter(int id)

        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateParameter(ParameterViewModel parameterViewModel)
        {
            var existingParameter = _dbContext.Parameters.FirstOrDefault(p => p.NameCompany == parameterViewModel.NameCompany);

            if (existingParameter != null)
            {
                ModelState.AddModelError("NameParameter", "Компанія з такою назвою вже існує");
                return View(parameterViewModel);
            }

            var parameter = new Parameter(parameterViewModel);

            if (parameter != null)
            {
                _dbContext.Parameters.Add(parameter);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Якщо модель недійсна, поверніть користувача на сторінку створення параметра з повідомленнями про помилки
            return View(parameter);
        }
        // створюємо категорію
        public IActionResult CreateCategory(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(CategoryViewModel categoryViewModel)
        {
            // Перевірка наявності категорії з такою назвою в базі даних
            var existingCategory = _dbContext.Categories.FirstOrDefault(c => c.NameCategory == categoryViewModel.NameCategory);

            if (existingCategory != null)
            {
                ModelState.AddModelError("CategoryName", "Модель з такою назвою вже існує");
                return View(categoryViewModel);
            }

            if (ModelState.IsValid)
            {
                var category = new Category(categoryViewModel);

                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(categoryViewModel);
        }
        [HttpGet]
        //покупка товару
        public ActionResult Buy(int? Id)
        {
            ViewBag.DeviceId = Id ?? 0;
            return View();
        }

        [HttpPost]
        public string Buy(PurchaseViewModel purchaseViewModel)
        {
            var purchase = new Purchase(purchaseViewModel);

            purchase.Date = DateTime.Now;
            _dbContext.Purchases.Add(purchase);
            _dbContext.SaveChanges();
            return "Дякую ," + purchase.Person + "за покупку";
        }
        //Видалення виробника
        public ActionResult DeleteParameter(int id)
        {
            ViewBag.Parameters = _dbContext.Parameters.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult DeleteParameter(ParameterViewModel parameterViewModel)
        {
            var parameter = new Parameter(parameterViewModel);
            _dbContext.Parameters.Remove(parameter);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));


        }
        // видалення категорії 
        public ActionResult DeleteCategory(int id)
        {
            ViewBag.Categories = _dbContext.Categories.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult DeleteCategory(CategoryViewModel categoryViewModel)
        {
            var category = new Category(categoryViewModel);
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //Видалення пристрою
        public ActionResult DeleteDevice(int id)
        {
            ViewBag.Devices = _dbContext.Devices.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult DeleteDevice(DeviceViewModel deviceViewModel)
        {
            var device = new Device(deviceViewModel);
            try
            {
                _dbContext.Devices.Remove(device);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Обробка помилок
                return View(device);
            }
        }
        public IActionResult EditDevice(int id)
        {
            ViewBag.Parameters = new SelectList(_dbContext.Parameters.ToList(), "Id", "NameCompany");
            ViewBag.Categories = new SelectList(_dbContext.Categories.ToList(), "Id", "NameCategory");
            var device = _dbContext.Devices.Find(id);
            var deviceViewModel = new DeviceViewModel(device);
            return View(deviceViewModel);
        }

        [HttpPost]
        public IActionResult EditDevice(DeviceViewModel deviceViewModel)
        {
            var device = new Device(deviceViewModel);
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Entry(device).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Parameters = new SelectList(_dbContext.Parameters.ToList(), "Id", "NameCompany");
                ViewBag.Categories = new SelectList(_dbContext.Categories.ToList(), "Id", "NameCategory");
                return View(deviceViewModel);
            }
            catch (Exception ex)
            {
                // Обробка помилок
                ViewBag.Categories = new SelectList(_dbContext.Categories.ToList(), "Id", "NameCategory");
                ViewBag.Parameters = new SelectList(_dbContext.Parameters.ToList(), "Id", "NameCompany");
                return View(deviceViewModel);
            }
        }


        public IActionResult EditParameter(int id)
        {
            Parameter parameter = _dbContext.Parameters.Find(id);
            var parameterViewModel = new ParameterViewModel(parameter); 
            if (parameter == null)
            {
                return NotFound();
            }

            return View(parameterViewModel);
        }

        [HttpPost]
        public IActionResult EditParameter(ParameterViewModel parameterViewModel)
        {
            var parameter = new Parameter(parameterViewModel);
            if (ModelState.IsValid)
            {
                // Update the parameter in the database
                _dbContext.Parameters.Update(parameter);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(parameterViewModel);
        }

        public IActionResult EditCategory(int id)
        {
            Category category = _dbContext.Categories.Find(id);
            var categoryViewModel = new CategoryViewModel(category);
            if (category == null)
            {
                return NotFound();
            }
            return View(categoryViewModel);
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryViewModel categoryViewModel)
        {
            var category = new Category(categoryViewModel);
            if (ModelState.IsValid)
            {


                _dbContext.Categories.Update(category);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryViewModel);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult IsNameCategoryAvailable(string NameCategory)
        {
            var isNameCategoryAvailable = !_dbContext.Categories.Any(c => c.NameCategory == NameCategory);
            return Json(isNameCategoryAvailable);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult IsNameParameterAvailable(string NameCompany)
        {
            var isNameParameterAvailable = !_dbContext.Parameters.Any(c => c.NameCompany == NameCompany);
            return Json(isNameParameterAvailable);
        }

        public IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
