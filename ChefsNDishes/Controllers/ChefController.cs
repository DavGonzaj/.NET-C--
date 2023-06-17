using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ChefsNDishes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Controllers;
public class ChefController : Controller
{
    private readonly ILogger<ChefController> _logger;
    //conncection to our database "db"
    private MyContext db;

    public ChefController(ILogger<ChefController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }
    [HttpGet("/chefs")]
    public IActionResult AllChefs()
    {
        List<Chef> allChefs = db.Chefs.Include(chef => chef.CreatedDishes).ToList();
        return View("AllChefs", allChefs);
    }

    [HttpGet("chefs/new")]
    public IActionResult NewChef()
    {
        return View("NewChef");
    }

    [HttpPost("chefs/create")]
    public IActionResult CreateChef(Chef newChef)
    {
        if (!ModelState.IsValid)
        {
            //send user back to form so they can see and fix erros
            return View("NewChef");
        }

        db.Chefs.Add(newChef);
        //db doesn't update until we run save changes
        //after SaveChanges, our newPost object now has it's PostID updated from db auto generated id
        db.SaveChanges();

        return RedirectToAction("AllChefs");
    }

    // view one
    [HttpGet("chefs/{chefId}")]
    public IActionResult ViewChef(int chefId)
    {
        Chef? chef = db.Chefs.FirstOrDefault(chef => chef.ChefId == chefId);
        if (chef == null)
        {
            return RedirectToAction("AllChefs");
        }
        return View("ChefDetails", chef);
    }

    [HttpPost("chefs/{chefId}/delete")]
    public IActionResult DeleteChef(int chefId)
    {
        Chef? chef = db.Chefs.FirstOrDefault(chef => chef.ChefId == chefId);
        if (chef != null)
        {
            db.Chefs.Remove(chef);
            db.SaveChanges();
        }
        return RedirectToAction("AllChefs");
    }

    //edit post
    [HttpGet("chefs/{chefId}/edit")]
    public IActionResult Edit(int chefId)
    {
        Chef? chef = db.Chefs.FirstOrDefault(chef => chef.ChefId == chefId);
        if (chef == null)
        {
            return RedirectToAction("AllChefs");
        }

        return View("EditChef", chef);

    }

    //update after edit
    [HttpPost("chefs/{chefId}/edit")]
    public IActionResult UpdateChef(int chefId, Chef updatedChef)
    {
        if (!ModelState.IsValid)
        {
            // Post? originalPost = db.Posts.FirstOrDefault(post => post.PostId == postId);
            // return View("Edit", originalPost);
            //we can replace the previous two lines with the following
            //this runs the code within the EditPost function, without creating a new re/res cycle
            //for the edit on the lecture, in order for it to work, the View() function in the Edit method !!!CANNOT!!! default the cshtml file
            return Edit(chefId);//this runs all logic from our edit function but wont create a new response cycle
        }

        Chef? dbChef = db.Chefs.FirstOrDefault(chef => chef.ChefId == chefId);

        if (dbChef == null)
        {
            return RedirectToAction("AllChefs");
        }

        dbChef.FirstName = updatedChef.FirstName;
        dbChef.LastName = updatedChef.LastName;
        dbChef.Birthday = updatedChef.Birthday;
        dbChef.UpdatedAt = DateTime.Now;

        // db.Posts.Update(dbPost);
        db.SaveChanges();

        return RedirectToAction("ViewChef", new { chefId = dbChef.ChefId });
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



