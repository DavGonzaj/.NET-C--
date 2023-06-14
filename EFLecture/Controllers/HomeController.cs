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
    // view one
    [HttpGet("posts/{postId}")]
    public IActionResult ViewPost(int postId)
    {
        Post? post = db.Posts.FirstOrDefault(post => post.PostId == postId);
        if (post == null)
        {
            return RedirectToAction("Index");
        }
        return View("Details", post);
    }

    [HttpPost("posts/{postId}/delete")]
    public IActionResult DeletePost(int postId)
    {
        Post? post = db.Posts.FirstOrDefault(post => post.PostId == postId);
        if (post != null)
        {
            db.Posts.Remove(post);
            db.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    //edit post
    [HttpGet("posts/{postId}/edit")]
    public IActionResult Edit(int postId)
    {
        Post? post = db.Posts.FirstOrDefault(post => post.PostId == postId);
        if (post == null)
        {
            return RedirectToAction("Index");
        }

        return View("Edit", post);

    }

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
            return RedirectToAction("Index");
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
