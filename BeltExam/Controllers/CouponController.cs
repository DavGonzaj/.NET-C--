using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BeltExam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace BeltExam.Controllers;

[SessionCheck]
public class CouponController : Controller
{
    private readonly ILogger<CouponController> _logger;
    //conncection to our database "db"
    private MyContext db;

    public CouponController(ILogger<CouponController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }
    [HttpGet("coupons")]
    public IActionResult AllCoupons()
    {
        List<Coupon> allCoupons = db.Coupons.Include(coupon => coupon.Creator).Include(coupon => coupon.Uses).ToList();
        return View("AllCoupons", allCoupons);
    }

    [HttpGet("coupons/new")]
    public IActionResult NewCoupon()
    {
        return View("New");
    }

    [HttpPost("coupons/create")]
    public IActionResult CreateCoupon(Coupon newCoupon)
    {
        if (!ModelState.IsValid)
        {
            //send user back to form so they can see and fix erros
            return View("New");
        }

        newCoupon.UserId = (int)HttpContext.Session.GetInt32("UUID");

        db.Coupons.Add(newCoupon);
        //db doesn't update until we run save changes
        //after SaveChanges, our newPost object now has it's PostID updated from db auto generated id
        db.SaveChanges();

        return RedirectToAction("AllCoupons");
    }

    [HttpPost("coupons/{couponId}/expired")]
    public IActionResult Expired(int couponId)
    {
        Expired? expCoupon = db.Expired.FirstOrDefault(uses => uses.UserId == HttpContext.Session.GetInt32("UUID") && uses.CouponId == couponId);
        if (expCoupon == null)
        {
            Expired newExp = new Expired()
            {
                CouponId = couponId,
                UserId = (int)HttpContext.Session.GetInt32("UUID")
            };

            db.Expired.Add(newExp);
        }
        db.SaveChanges();
        return RedirectToAction("AllCoupons");
    }




    //counts
    [HttpPost("coupons/{couponId}/uses")]
    public IActionResult Uses(int couponId)
    {
        UserCouponUses? existingUse = db.UserCouponUses.FirstOrDefault(uses => uses.UserId == HttpContext.Session.GetInt32("UUID") && uses.CouponId == couponId);
        if (existingUse == null)
        {
            UserCouponUses newUse = new UserCouponUses()
            {
                CouponId = couponId,
                UserId = (int)HttpContext.Session.GetInt32("UUID")
            };

            db.UserCouponUses.Add(newUse);
        }
        db.SaveChanges();
        return RedirectToAction("AllCoupons");
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
