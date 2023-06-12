using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Dashboard(string name, string operation)
    {
        if (operation == "logout")
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        if (string.IsNullOrEmpty(name))
        {
            return RedirectToAction("Index");
        }

        if (HttpContext.Session.GetString("Name") == null)
        {
            HttpContext.Session.SetString("Name", name);
            HttpContext.Session.SetInt32("Value", 22);
        }

        int value = HttpContext.Session.GetInt32("Value").Value;

        if (operation == "+")
        {
            value += 1;
        }
        else if (operation == "-")
        {
            value -= 1;
        }
        else if (operation == "x")
        {
            value *= 2;
        }
        else if (operation == "random")
        {
            Random random = new Random();
            value += random.Next(1, 11);
        }

        HttpContext.Session.SetInt32("Value", value);

        // Create a new instance of DashboardModel and set the updated value
        var model = new DashboardModel
        {
            Name = name,
            Value = value
        };

        return View("Dashboard", model);
    }


}
