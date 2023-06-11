using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Petstagram.Models;

namespace Petstagram.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("addPet")]
    public IActionResult Create(Pet newPet)
    {
        // checking to see if the Model passed in as an argument has valid data in it
        if(!ModelState.IsValid)
        {
            //if invalid data, we NEED to return a view so that the error messages don't go away
            //(since error messages only exist for one req/res cycle)
            return View("Index");
        }
        
        Console.WriteLine($"{newPet.Name} is a {newPet.Age} year old {newPet.Type}");
        if(newPet.Type == "tortoise")
        {
            ViewBag.SecretMessage = "You picked the secret pet type!!!!";
            return View("Secret", newPet);
        }
        return Redirect("/");
    }

    [HttpPost("addPetSession")]
    public IActionResult CreateWithSession(Pet newPet)
    {
        if(!ModelState.IsValid)
        {
            return View("Index");
        }
        // session stores data as dict: key          value
        HttpContext.Session.SetString("PetName", newPet.Name);
        HttpContext.Session.SetString("PetType", newPet.Type);
        HttpContext.Session.SetInt32("PetAge", newPet.Age);

        string? sessionName = HttpContext.Session.GetString("PetName");
        int? sessionAge = HttpContext.Session.GetInt32("PetAge");

        Console.WriteLine($"Pet Name is {sessionName}");
        Console.WriteLine($"Pet Age is {sessionAge}");

        return RedirectToAction("Success");
    }

    [HttpGet("success")]
    public IActionResult Success()
    {
        return View("Success");
    }

    [HttpPost("clearSession")]
    public IActionResult ClearSession()
    {
        HttpContext.Session.Clear();
        //redirecttoaction still redirects the user, but you need to pass in a string of the function name
        //rather than the url that you want to redirect to
        return RedirectToAction("Index");
    }

    [HttpGet("privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet("{**path}")]
    public RedirectResult Unknown()
    {
        return Redirect("/");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
