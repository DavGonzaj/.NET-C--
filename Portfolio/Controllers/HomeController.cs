using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    [HttpGet("")]//this combinrd both lines 5 & 6
    [HttpGet("/index")]// you can stack routes by adding ,ultiple http get attributes
    //handle the incoming request
    public ViewResult Index()
    {
        //how we respond to the request
        return View("Index");
    }
    [HttpGet("/projects")]
    public ViewResult Projects()
    {
        //how we respond to the request
        return View("Projects");
    }
    [HttpGet("/contact")]
    public ViewResult Contact()
    {
        //how we respond to the request
        return View("Contact");
    }

}