using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdsAndCats.Models;

namespace ProdsAndCats.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    //conncection to our database "db"
    private MyContext db;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        db = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {


        return View();
    }

    [HttpGet("product/new")]
    public IActionResult AddProduct()
    {
        ViewBag.AllProducts = db.Products.ToList();
        return View();
    }
    [HttpPost("create")]
    public IActionResult Create(Product newProduct)
    {
        if (ModelState.IsValid)
        {
            db.Products.Add(newProduct);
            db.SaveChanges();
            return Redirect("product/new");
        }
        else
        {
            ViewBag.AllProducts = db.Products.ToList();
            return View("AddProduct");
        }
    }

    [HttpGet("categories")]
    public IActionResult Categories()
    {
        ViewBag.AllCategories = db.Categories.ToList();
        return View();
    }
    [HttpPost("process")]
    public IActionResult Process(Category newCategory)
    {
        if (ModelState.IsValid)
        {
            db.Categories.Add(newCategory);
            db.SaveChanges();
            return Redirect("categories");
        }
        else
        {
            ViewBag.AllCategories = db.Categories.ToList();
            return View("Categories");
        }
    }
    [HttpGet("product/{productId}")]
    public IActionResult DisplayProduct(int productId)
    {
        List<Product> AllProducts = db.Products.Include(c => c.Categories).ThenInclude(c => c.NavProduct).ToList();

        ViewBag.AllCategories = db.Categories.ToList();
        ViewBag.NotOnProduct = db.Categories.Include(c => c.Products).Where(c => c.Products.All(a => a.ProductId != productId)).ToList();
        Product? PrId = db.Products.Include(p => p.Categories).ThenInclude(a => a.NavCategory).FirstOrDefault(p => p.ProductId == productId);

        ViewBag.productId = productId;
        return View(PrId);
    }
    [HttpPost("product/{productId}")]
    public IActionResult AddCategory(int productId, int categoryId)
    {
        Association categorized = new Association();
        categorized.ProductId = productId;
        categorized.CategoryId = categoryId;
        db.Associations.Add(categorized);
        db.SaveChanges();
        return Redirect($"/product/{productId}");

    }
    [HttpPost("category/{categoryId}")]
    public IActionResult AddProduct(int productId, int categoryId)
    {
        Association LoadProduct = new Association();
        LoadProduct.ProductId = productId;
        LoadProduct.CategoryId = categoryId;
        db.Associations.Add(LoadProduct);
        db.SaveChanges();
        return Redirect($"/category/{categoryId}");

    }
    [HttpGet("category/{categoryId}")]
    public IActionResult DisplayCategory(int categoryId)
    {
        List<Category> AllCategories = db.Categories.Include(c => c.Products).ThenInclude(c => c.NavCategory).ToList();

        ViewBag.AllProducts = db.Products.ToList();
        ViewBag.NotOnCategory = db.Products.Include(c => c.Categories).Where(c => c.Categories.All(a => a.CategoryId != categoryId)).ToList();
        Category CtrId = db.Categories.Include(p => p.Products).ThenInclude(a => a.NavProduct).FirstOrDefault(p => p.CategoryId == categoryId);

        ViewBag.categoryId = categoryId;
        return View(CtrId);
    }
    [HttpPost("remove/{associationId}")]
    public IActionResult Remove(int associationId)
    {
        Association ToBeRemoved = db.Associations.FirstOrDefault(t => t.AssociationId == associationId);
        db.Associations.Remove(ToBeRemoved);
        db.SaveChanges();
        return Redirect($"/category/{ToBeRemoved.CategoryId}");
    }
    [HttpPost("delete/{associationId}")]
    public IActionResult Delete(int associationId)
    {
        Association ToBeRemoved = db.Associations.FirstOrDefault(t => t.AssociationId == associationId);
        db.Associations.Remove(ToBeRemoved);
        db.SaveChanges();
        return Redirect($"/product/{ToBeRemoved.ProductId}");
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
