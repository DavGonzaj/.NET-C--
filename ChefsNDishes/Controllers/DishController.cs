using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ChefsNDishes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Controllers;
public class DishController : Controller
{
    private readonly ILogger<DishController> _logger;
    //conncection to our database "db"
    private MyContext db;

    public DishController(ILogger<DishController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }
    [HttpGet("/dishes")]
    public IActionResult AllDishes()
    {
        List<Dish> allDishes = db.Dishes.ToList();
        return View("AllDishes", allDishes);
    }

    [HttpGet("dishes/new")]
    public IActionResult NewDish()
    {
        return View("New");
    }

    [HttpPost("dishes/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if (!ModelState.IsValid)
        {
            //send user back to form so they can see and fix erros
            return View("New");
        }

        db.Dishes.Add(newDish);
        //db doesn't update until we run save changes
        //after SaveChanges, our newPost object now has it's PostID updated from db auto generated id
        db.SaveChanges();

        return RedirectToAction("AllDishes");
    }

    // view one
    [HttpGet("dishes/{dishId}")]
    public IActionResult ViewDish(int dishId)
    {
        Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
        if (dish == null)
        {
            return RedirectToAction("AllDishes");
        }
        return View("Details", dish);
    }

    [HttpPost("dishes/{dishId}/delete")]
    public IActionResult DeleteDish(int dishId)
    {
        Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
        if (dish != null)
        {
            db.Dishes.Remove(dish);
            db.SaveChanges();
        }
        return RedirectToAction("AllDishes");
    }

    //edit post
    [HttpGet("dishes/{dishId}/edit")]
    public IActionResult Edit(int dishId)
    {
        Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
        if (dish == null)
        {
            return RedirectToAction("AllDishes");
        }

        return View("Edit", dish);

    }

    //update after edit
    [HttpPost("dishes/{dishId}/edit")]
    public IActionResult UpdateDish(int dishId, Dish updatedDish)
    {
        if (!ModelState.IsValid)
        {
            // Post? originalPost = db.Posts.FirstOrDefault(post => post.PostId == postId);
            // return View("Edit", originalPost);
            //we can replace the previous two lines with the following
            //this runs the code within the EditPost function, without creating a new re/res cycle
            //for the edit on the lecture, in order for it to work, the View() function in the Edit method !!!CANNOT!!! default the cshtml file
            return Edit(dishId);//this runs all logic from our edit function but wont create a new response cycle
        }

        Dish? dbDish = db.Dishes.FirstOrDefault(dish => dish.DishId == dishId);

        if (dbDish == null)
        {
            return RedirectToAction("AllDishes");
        }

        dbDish.Name = updatedDish.Name;
        dbDish.Chef = updatedDish.Chef;
        dbDish.Tastiness = updatedDish.Tastiness;
        dbDish.Calories = updatedDish.Calories;
        dbDish.Description = updatedDish.Description;
        dbDish.UpdatedAt = DateTime.Now;

        // db.Posts.Update(dbPost);
        db.SaveChanges();

        return RedirectToAction("ViewDish", new { dishId = dbDish.DishId });
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



