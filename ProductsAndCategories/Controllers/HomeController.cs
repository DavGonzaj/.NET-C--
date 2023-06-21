using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductsAndCategories.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        // ViewBag.allProducts = db.Products.ToList();
        return View("Index", allProducts);
    }
    [HttpPost("products")]
    public IActionResult CreateProduct(Product newProduct)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", db.Products.ToList());
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
        Product? oneProduct = db.Products.Include(prod => prod.Associations)
        .ThenInclude(ass => ass.Category).FirstOrDefault(oneProd => oneProd.ProductId == id);

        if (oneProduct == null)
        {
            return RedirectToAction("Index"); // Optionally handle when the product is not found
        }

        return View("OneProduct", oneProduct);
    }
    [HttpPost("products/addcategory")]
    public IActionResult AddCatToProd(int CategoryId, int ProductId)
    {
        Product? oneProd = db.Products
        .Include(prod => prod.Associations)
        .FirstOrDefault(cate => cate.ProductId == ProductId);
        if (oneProd == null)
        {
            return RedirectToAction("OneProduct");
        }
        Category? oneCat = db.Categories.FirstOrDefault(cat => cat.CategoryId == CategoryId);
        if (oneCat == null)
        {
            return RedirectToAction("OneProduct");
        }
        if (oneCat.Associations.Any(a => a.ProductId == ProductId))
        {
            return RedirectToAction("OneProduct", new { id = ProductId });
        }
        Association newAssoc = new Association
        {
            Product = oneProd,
            Category = oneCat
        };
        db.Associations.Add(newAssoc);
        db.SaveChanges();
        return RedirectToAction("ViewProduct", new { id = ProductId });
    }
    [HttpGet("categories")]
    public IActionResult Categories()
    {
        List<Category> allCategories = db.Categories.ToList();
        // ViewBag.allCategories = db.Categories.ToList();
        return View("Categories", allCategories);
    }
    [HttpPost("categories")]
    public IActionResult CreateCategory(Category newCategory)
    {
        if (!ModelState.IsValid)
        {
            List<Category> allCategories = db.Categories.ToList();
            // ViewBag.allCategories = db.Categories.ToList();
            return View("Categories", allCategories);
        }
        db.Categories.Add(newCategory);
        //db doesn't update until we run save changes
        //after SaveChanges, our newPost object now has it's PostID updated from db auto generated id
        db.SaveChanges();
        // List<Category> allCategories = db.Categories.ToList();

        return RedirectToAction("Categories");
    }
    [HttpGet("categories/{id}")]
    public IActionResult ViewCategory(int id)
    {
        Category? oneCategory = db.Categories.Include(cat => cat.Associations)
        .ThenInclude(ass => ass.Product).FirstOrDefault(oneCat => oneCat.CategoryId == id);

        if (oneCategory == null)
        {
            return RedirectToAction("Categories"); // Optionally handle when the Category is not found
        }

        return View("OneCat", oneCategory);
    }
    // [HttpPost("categories/assproduct")]
    // public IActionResult AddProdTocat(int ProductId, int CategoryId)
    // {
    //     Category? oneCat
    // }
    [HttpPost("categories/addproduct")]
    public IActionResult AddProdToCat(int ProductId, int CategoryId)
    {
        Category? oneCat = db.Categories
            .Include(cat => cat.Associations)
            .FirstOrDefault(prod => prod.CategoryId == CategoryId);
        if (oneCat == null)
        {
            return RedirectToAction("Categories");
        }
        Product? oneProd = db.Products.FirstOrDefault(prod => prod.ProductId == ProductId);
        if (oneProd == null)
        {
            return RedirectToAction("Categories");
        }
        if (oneProd.Associations.Any(a => a.CategoryId == CategoryId))
        {
            return RedirectToAction("ViewCategory", new { id = CategoryId });
        }
        Association newAssoc = new Association
        {
            Product = oneProd,
            Category = oneCat
        };
        db.Associations.Add(newAssoc);
        db.SaveChanges();
        return RedirectToAction("ViewCategory", new { id = CategoryId });
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



