using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BeltExam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeltExam.Controllers;

[SessionCheck]
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    //conncection to our database "db"
    private MyContext db;

    public AccountController(ILogger<AccountController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }
    // view one
    [HttpGet("users/{userId}")]
    public IActionResult AccountInfo(int userId)
    {
        int? loggedInUserId = HttpContext.Session.GetInt32("UUID");
        User? loggedInUser = db.Users.Include(user => user.Uses).FirstOrDefault(user => user.UserId == loggedInUserId);

        if (loggedInUser == null)
        {
            return RedirectToAction("AllCoupons");
        }

        int numberOfCouponsPosted = db.Coupons
            .Count(coupon => coupon.UserId == loggedInUserId);

        ViewData["NumberOfCouponsPosted"] = numberOfCouponsPosted;
        ViewData["NumberOfCouponsUsed"] = loggedInUser.Uses.Count;

        return View("Details", loggedInUser);
    }



    //counts
    // [HttpPost("coupons/{couponId}/uses")]
    // public IActionResult Uses(int couponId)
    // {
    //     UserCouponUses? existingUse = db.UserCouponUses.FirstOrDefault(uses => uses.UserId == HttpContext.Session.GetInt32("UUID") && uses.CouponId == couponId);
    //     if (existingUse == null)
    //     {
    //         UserCouponUses newUse = new UserCouponUses()
    //         {
    //             CouponId = couponId,
    //             UserId = (int)HttpContext.Session.GetInt32("UUID")
    //         };

    //         db.UserCouponUses.Add(newUse);
    //     }
    //     else
    //     {
    //         db.UserCouponUses.Remove(existingUse);
    //     }
    //     db.SaveChanges();
    //     return RedirectToAction("AllCoupons");
    // }
}

// Name this anything you want with the word "Attribute" at the end
// public class SessionCheckAttribute : ActionFilterAttribute
// {
//     public override void OnActionExecuting(ActionExecutingContext context)
//     {
//         // Find the session, but remember it may be null so we need int?
//         int? userId = context.HttpContext.Session.GetInt32("UUID");
//         // Check to see if we got back null
//         if (userId == null)
//         {
//             // Redirect to the Index page if there was nothing in session
//             // "Home" here is referring to "HomeController", you can use any controller that is appropriate here
//             context.Result = new RedirectToActionResult("Index", "Home", null);
//         }
//     }
// }
