using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EFLecture.Models;

namespace EFLecture.Controllers;

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
        List<Post> allPosts = db.Posts.ToList();
        return View("AllPosts", allPosts);
    }

    [HttpGet("posts/new")]
    public IActionResult NewPost()
    {
        return View("New");
    }

    [HttpPost("posts/create")]
    public IActionResult CreatePost(Post newPost)
    {
        if (!ModelState.IsValid)
        {
            //send user back to form so they can see and fix erros
            return View("New");
        }

        db.Posts.Add(newPost);
        //db doesn't update until we run save changes
        //after SaveChanges, our newPost object now has it's PostID updated from db auto generated id
        db.SaveChanges();

        return RedirectToAction("Index");
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
