using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductsAndCategories.Models;
using Microsoft.AspNetCore.Identity;

namespace ProductsAndCategories.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    //conncection to our database "db"
    private MyContext db;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }
    [HttpGet("")]
    public IActionResult Index()
    {
        List<Product> allProducts = db.Products.ToList();
        ViewBag.allProducts = db.Products.ToList();
        return View("Index");
    }
    [HttpPost("products")]
    public IActionResult CreateProduct(Product newProduct)
    {
        if (!ModelState.IsValid)
        {
            return View("NewProduct");
        }
        db.Products.Add(newProduct);
        //db doesn't update until we run save changes
        //after SaveChanges, our newPost object now has it's PostID updated from db auto generated id
        db.SaveChanges();
        List<Product> allProducts = db.Products.ToList();

        return RedirectToAction("Index", allProducts);
    }

    [HttpGet("products/{id}")]
    public IActionResult ViewProduct(int id)
    {
        Product? oneProduct = db.Products.FirstOrDefault(oneProd => oneProd.ProductId == id);

        if (oneProduct == null)
        {
            return RedirectToAction("Index"); // Optionally handle when the product is not found
        }

        return View("OneProduct", oneProduct);
    }
    [HttpGet("categories")]
    public IActionResult Categories()
    {
        List<Category> allCategories = db.Categories.ToList();
        ViewBag.allCategories = db.Categories.ToList();
        return View("Categories");
    }
    [HttpPost("categories")]
    public IActionResult CreateCategory(Category newCategory)
    {
        if (!ModelState.IsValid)
        {
            return View("NewCategory");
        }
        db.Categories.Add(newCategory);
        //db doesn't update until we run save changes
        //after SaveChanges, our newPost object now has it's PostID updated from db auto generated id
        db.SaveChanges();
        List<Category> allCategories = db.Categories.ToList();

        return RedirectToAction("Index", allCategories);
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



