
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

