using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using EFLecture.Models;
using Microsoft.AspNetCore.Identity;

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
        return View("Index");
    }

    [HttpPost("register")]
    public IActionResult Register(User newUser)
    {
        if (!ModelState.IsValid)
        {
            return Index();
        }

        PasswordHasher<User> hasBrowns = new PasswordHasher<User>();
        newUser.Password = hasBrowns.HashPassword(newUser, newUser.Password);

        db.Users.Add(newUser);
        db.SaveChanges();

        HttpContext.Session.SetInt32("UUID", newUser.UserId);
        return RedirectToAction("AllPosts");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginUser loginUser)
    {
        if (!ModelState.IsValid)
        {
            return Index();
        }

        User? dbUser = db.Users.FirstOrDefault(user => user.Email == loginUser.Email);

        if (dbUser == null)
        {
            ModelState.AddModelError("Email", "nor found");
            return Index();
        }

        PasswordHasher<LoginUser> hashBrowns = new PasswordHasher<LoginUser>();
        PasswordVerificationResult pwCompareResult = hashBrowns.VerifyHashedPassword(loginUser, dbUser.Password, loginUser.LoginPassword);

        if (pwCompareResult == 0)
        {
            //normally we won't be specific with errors but for demo reasons we are 
            //since malicious users can benefit from the specificity
            ModelState.AddModelError("LoginPassword", "invalid password");
        }

        HttpContext.Session.SetInt32("UUID", dbUser.UserId);
        return RedirectToAction("AllPosts");
    }

    [HttpPost("logout")]

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [SessionCheck]
    [HttpGet("/posts")]
    public IActionResult AllPosts()
    {
        List<Post> allPosts = db.Posts.ToList();
        return View("AllPosts", allPosts);
    }

    [SessionCheck]
    [HttpGet("posts/new")]
    public IActionResult NewPost()
    {
        return View("New");
    }

    [SessionCheck]
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

        return RedirectToAction("AllPosts");
    }

    [SessionCheck]
    // view one
    [HttpGet("posts/{postId}")]
    public IActionResult ViewPost(int postId)
    {
        Post? post = db.Posts.FirstOrDefault(post => post.PostId == postId);
        if (post == null)
        {
            return RedirectToAction("AllPosts");
        }
        return View("Details", post);
    }

    [SessionCheck]
    [HttpPost("posts/{postId}/delete")]
    public IActionResult DeletePost(int postId)
    {
        Post? post = db.Posts.FirstOrDefault(post => post.PostId == postId);
        if (post != null)
        {
            db.Posts.Remove(post);
            db.SaveChanges();
        }
        return RedirectToAction("AllPosts");
    }

    [SessionCheck]
    //edit post
    [HttpGet("posts/{postId}/edit")]
    public IActionResult Edit(int postId)
    {
        Post? post = db.Posts.FirstOrDefault(post => post.PostId == postId);
        if (post == null)
        {
            return RedirectToAction("AllPosts");
        }

        return View("Edit", post);

    }

    [SessionCheck]
    //update after edit
    [HttpPost("posts/{postId}/edit")]
    public IActionResult UpdatePost(int postId, Post updatedPost)
    {
        if (!ModelState.IsValid)
        {
            // Post? originalPost = db.Posts.FirstOrDefault(post => post.PostId == postId);
            // return View("Edit", originalPost);
            //we can replace the previous two lines with the following
            //this runs the code within the EditPost function, without creating a new re/res cycle
            //for the edit on the lecture, in order for it to work, the View() function in the Edit method !!!CANNOT!!! default the cshtml file
            return Edit(postId);//this runs all logic from our edit function but wont create a new response cycle
        }

        Post? dbPost = db.Posts.FirstOrDefault(post => post.PostId == postId);

        if (dbPost == null)
        {
            return RedirectToAction("AllPosts");
        }

        dbPost.Title = updatedPost.Title;
        dbPost.Body = updatedPost.Body;
        dbPost.ImgUrl = updatedPost.ImgUrl;
        dbPost.UpdatedAt = DateTime.Now;

        // db.Posts.Update(dbPost);
        db.SaveChanges();

        return RedirectToAction("ViewPost", new { postId = dbPost.PostId });
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

// Name this anything you want with the word "Attribute" at the end
public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Find the session, but remember it may be null so we need int?
        int? userId = context.HttpContext.Session.GetInt32("UUID");
        // Check to see if we got back null
        if (userId == null)
        {
            // Redirect to the Index page if there was nothing in session
            // "Home" here is referring to "HomeController", you can use any controller that is appropriate here
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}

