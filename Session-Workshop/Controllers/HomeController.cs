using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Dashboard(DashboardModel model)
    {
        if (model.Operation == "logout")
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        if (string.IsNullOrEmpty(model.Name))
        {
            return RedirectToAction("Index");
        }

        if (HttpContext.Session.GetString("Name") == null)
        {
            HttpContext.Session.SetString("Name", model.Name);
            HttpContext.Session.SetInt32("Value", 22);
        }

        int value = HttpContext.Session.GetInt32("Value").Value;

        switch (model.Operation)
        {
            case "+":
                value += 1;
                break;
            case "-":
                value -= 1;
                break;
            case "x":
                value *= 2;
                break;
            case "random":
                Random random = new Random();
                value += random.Next(1, 11);
                break;
        }

        HttpContext.Session.SetInt32("Value", value);
        model.Value = value; // Set the updated value in the model

        return View(model);
    }

}
