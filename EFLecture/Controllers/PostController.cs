using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using EFLecture.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EFLecture.Controllers;
[SessionCheck]
public class PostController : Controller
{
    private readonly ILogger<PostController> _logger;
    //conncection to our database "db"
    private MyContext db;

    public PostController(ILogger<PostController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }

    [HttpGet("/posts")]
    public IActionResult AllPosts()
    {
        List<Post> allPosts = db.Posts.Include(post => post.Author).Include(post => post.UserLikes).ToList();
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

        newPost.UserId = (int)HttpContext.Session.GetInt32("UUID");

        db.Posts.Add(newPost);
        //db doesn't update until we run save changes
        //after SaveChanges, our newPost object now has it's PostID updated from db auto generated id
        db.SaveChanges();

        return RedirectToAction("AllPosts");
    }

    // view one
    [HttpGet("posts/{postId}")]
    public IActionResult ViewPost(int postId)
    {
        Post? post = db.Posts.Include(post => post.Author).Include(post => post.UserLikes).ThenInclude(like => like.User).FirstOrDefault(post => post.PostId == postId);
        if (post == null)
        {
            return RedirectToAction("AllPosts");
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
        return RedirectToAction("AllPosts");
    }

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
    [HttpPost("post/{postId}/like")]
    public IActionResult LikePost(int postId)
    {
        UserPostLike? existingLike = db.UserPostLikes.FirstOrDefault(like => like.UserId == HttpContext.Session.GetInt32("UUID") && like.PostId == postId);
        if (existingLike == null)
        {
            UserPostLike newLike = new UserPostLike()
            {
                PostId = postId,
                UserId = (int)HttpContext.Session.GetInt32("UUID")
            };

            db.UserPostLikes.Add(newLike);
        }
        else
        {
            db.UserPostLikes.Remove(existingLike);
        }
        db.SaveChanges();
        return RedirectToAction("AllPosts");
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



