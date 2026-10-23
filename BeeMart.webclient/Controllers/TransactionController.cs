using Microsoft.AspNetCore.Mvc;

namespace BeeMart.webclient.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}