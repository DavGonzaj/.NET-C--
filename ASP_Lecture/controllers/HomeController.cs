using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    //attribute, http type & route URL 
    //[HttpGet]
    //[Route("")]
    [HttpGet("")]//this combinrd both lines 5 & 6
    [HttpGet("/index")]// you can stack routes by adding ,ultiple http get attributes
    //handle the incoming request
    public ViewResult Index()
    {
        //how we respond to the request
        return View("Index");
    }

    [HttpGet("videos")]
    public ViewResult Videos()
    {
        ViewBag.Videos = new List<string>()
        {
            "yT3_vLQ3jbM", "fbqHK8i-HdA", "CUe2ymg1RHs", "-rEIOkGCbo8", "KYgZPphIKQY", "GPdGeLAprdg", "eg9_ymCEAF8", "nHkUMkUFuBc", "QTwcvNdMFMI", "j6YK-qgt_TI", "Wvjsgb2nB4o", "GcKkiRl9_qE", "6avJHaC3C2U", "_mZBa3sqTrI"
        };
        ViewBag.VideoCount = "5";

        /*
        Each controller method / 'action' has it's own ViewBag that is
        SEPARATE, the data is not shared between them.

        The ViewBag properties are automatically available in the View
        that is returned from this method.
        */
        return View("Videos");
    }
}