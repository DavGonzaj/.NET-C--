
using System.Diagnostics;
using Dojo_Survey_Validation.Models;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("formsubmit")]
    public IActionResult FormSubmit(User user)
    {

        if (ModelState.IsValid)
        {
            return RedirectToAction("Result", user);
        }
        else
        {
            return View("Index");
        }

    }

    [HttpGet("result")]

    public IActionResult Result(User user)
    {
        return View("Result", user);
    }
}

//     public IActionResult Generator()
//     {
//         return Generator(HttpContext);
//     }

//     [HttpGet("generator")]
//     public IActionResult Generator(HttpContext httpContext)
//     {

//         string? newPassword = HttpContext.Session.GetString("password");
//         int? Count = HttpContext.Session.GetInt32("Count");

//         ViewBag.newPassword = newPassword;
//         ViewBag.Count = Count;

//         return View();
//     }

//     public HttpContext GetHttpContext()
//     {
//         return HttpContext;
//     }

//     [HttpPost("randompass")]
//     public IActionResult RandomPass(HttpContext httpContext)
//     {
//         int len = 10;
//         string password = "";
//         Random rand = new Random();
//         for (int i = 0; i < len; i++)
//         {
//             int num = rand.Next(0, 122);
//             if (num >= 97 && num <= 122)
//             {
//                 password += (char)num;
//             }
//             else if (num >= 65 && num <= 90)
//             {
//                 password += (char)num;
//             }
//             else if (num >= 48 && num <= 57)
//             {
//                 password += (char)num;
//             }
//             else
//             {
//                 i--;
//             }
//         }
//         HttpContext.Session.SetString("password", password);
//         if (HttpContext.Session.GetInt32("Count") == null)
//         {
//             HttpContext.Session.SetInt32("Count", 1);
//         }
//         else
//         {
//             HttpContext.Session.SetInt32("Count", (int)(+1));
//             // HttpContext.Session.GetInt32("Count")
//         }


//         string? newPassword = httpContext.Session.GetString("password");
//         int? Count = HttpContext.Session.GetInt32("Count");


//         return Json(new { newPassword = newPassword, Count = Count });

//         // return RedirectToAction("Generator");
//     }

//     public IActionResult Privacy()
//     {
//         return View();
//     }

//     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//     public IActionResult Error()
//     {
//         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//     }
// }