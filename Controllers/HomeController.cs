using Microsoft.AspNetCore.Mvc;

namespace avviewer.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet]
    public IActionResult Index()
    {
      return View("~/Pages/Shared/Index.html");
    }
  }
}