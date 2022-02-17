using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        // private readonly ILogger<HomeController> _logger;

        private CRUDeliciousContext db;
        public HomeController(CRUDeliciousContext context)
        {
            db = context;
        }

        [HttpGet("")]
        public ViewResult Index()
        {
            List<Dish> allDishes = db.Dishes
                .OrderByDescending(d => d.CreatedAt)
                .ToList();

            return View("Index", allDishes);
        }

        [HttpGet("/dish/new")]
        public ViewResult New()
        {
            return View("New");
        }

        [HttpPost("/dish/create")]
        public IActionResult Create(Dish newDish)
        {
            if(ModelState.IsValid)
            {
                db.Add(newDish);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("New");
        }

        [HttpGet("/dish/{dishId}")]
        public IActionResult GetDish(int dishId)
        {
            Dish RetrievedDish = db.Dishes
                .FirstOrDefault(d => d.DishId == dishId);

            if (RetrievedDish == null)
            {
                return RedirectToAction("Index");
            }

            return View("Dish", RetrievedDish);
        }

        [HttpGet("/dish/{dishId}/update")]
        public IActionResult UpdateDish(int dishId)
        {
            Dish RetrievedDish = db.Dishes
                .FirstOrDefault(d => d.DishId == dishId);

            if (RetrievedDish == null)
            {
                return RedirectToAction("Index");
            }
            return View("Edit", RetrievedDish);
        }

        [HttpPost("/dish/{dishId}/edit")]
        public IActionResult Edit(Dish editedDish, int dishId)
        {
            Dish RetrievedDish = db.Dishes
                .FirstOrDefault(d => d.DishId == dishId);

            if (RetrievedDish != null)
            {
                if (ModelState.IsValid)
                {
                    RetrievedDish.Name = editedDish.Name;
                    RetrievedDish.Chef = editedDish.Chef;
                    RetrievedDish.Calories = editedDish.Calories;
                    RetrievedDish.Tastiness = editedDish.Tastiness;
                    RetrievedDish.Description = editedDish.Description;
                    RetrievedDish.UpdatedAt = DateTime.Now;
                    db.SaveChanges();
                    return View("Dish", editedDish);
                }
                return View("Edit", RetrievedDish);
            }
            return View("Index");

        }

        [HttpPost("/dish/{dishId}/delete")]
        public IActionResult Delete(int dishId)
        {
            Dish RetrievedDish = db.Dishes
                .FirstOrDefault(d => d.DishId == dishId);
            
            if(RetrievedDish != null)
            {
                db.Dishes.Remove(RetrievedDish);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");

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
