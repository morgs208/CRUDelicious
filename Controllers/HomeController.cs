using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private CrudContext dbContext;
        public HomeController(CrudContext context)
        {
            dbContext = context;
        }

        [HttpGet("Dishes")]
        public IActionResult Index()
        {
            List<Dish> allDishes = dbContext.Dishes.ToList();
            return View(allDishes);
        }

      [HttpGet("Dishes/NewDish")]
      public IActionResult NewDish()
      {
    
        return View();
      }

      [HttpPost("Dishes/Create")]
      public IActionResult Create(Dish newDish)
      {
          if(ModelState.IsValid == false)
          {
              return View("newDish");
          }

          dbContext.Add(newDish);
          dbContext.SaveChanges();
          return RedirectToAction("Index", newDish);
      }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("/Dishes/{dishId}")]
        public IActionResult Details(int dishId)
        {
            Dish selectedDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            //in case user manually types an invalid ID into the URL
            if (selectedDish == null)
            {
                RedirectToAction("Index");
            }
            return View(selectedDish);
        }

        [HttpGet("/Dishes/Edit/{dishId}")]
        public IActionResult Edit(int dishId)
        {
            Dish dishToEdit = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == dishId);

                if (dishToEdit == null)
        {
            return RedirectToAction("All");
        }
      

        return View(dishToEdit);
        }

        [HttpPost("/Dishes/Update")]
        public IActionResult Update(Dish editedDish, int dishId)
        {
            if (ModelState.IsValid == false)
            {
                return View ("Edit", editedDish);
            }
            
            Dish dbDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == dishId);

            dbDish.ChefName = editedDish.ChefName;
            dbDish.DishName = editedDish.DishName;
            dbDish.Calories = editedDish.Calories;
            dbDish.Toastiness = editedDish.Toastiness;
            dbDish.Description = editedDish.Description;

            dbContext.Dishes.Update(dbDish);
            dbContext.SaveChanges();

            return RedirectToAction("Details", new {dishId = dbDish.DishId});
        }

        [HttpGet("/dishes/{dishId}/delete")]
        public IActionResult Delete(int dishId)
        {
            Dish dishToDelete = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            dbContext.Dishes.Remove(dishToDelete);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        }
    }

