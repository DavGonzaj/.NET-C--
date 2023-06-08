
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{

    [HttpGet("")]
    public ActionResult Index()
    {
        return View("Index");
    }

    [HttpPost("result")]
    public ActionResult Result(string name, string location, string language, string textarea)
    {
        ViewBag.name = name;
        ViewBag.location = location;
        ViewBag.language = language;
        ViewBag.textarea = textarea;

        return View();
    }

    //Redirect back to Index if trying to access result page through GET
    [HttpGet("result")]
    public RedirectToActionResult Redirect()
    {
        return RedirectToAction("Index");
    }
}