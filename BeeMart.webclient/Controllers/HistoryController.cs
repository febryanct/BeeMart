using Microsoft.AspNetCore.Mvc;

namespace BeeMart.webclient.Controllers
{
    public class HistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}