using Hotel.Atr3.RealPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hotel.Atr3.RealPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        public IActionResult Index()
        {
            ViewBag.Title = "Home";

            return View();
        }

        public IActionResult Contact(Message message)
        {
            message = new Message();
            return View(message);
        }

        [HttpPost]
        //public IActionResult AddMessage(string name, string email, string message)
        public IActionResult AddMessage(Message userMessage)
        {
            //write to DB
            //sent email
            TempData["ResultAddMessage"] = "Данные успешно добавлены";
            //ViewBag.ResultAddMessage = "Данные успешно добавлены";

            return RedirectToAction("Contact", userMessage);
        }
    }
}
