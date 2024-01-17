using Microsoft.AspNetCore.Mvc;

namespace BookMarketWeb.Controllers;

public class ManageController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}