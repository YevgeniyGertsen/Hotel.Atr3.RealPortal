using FluentValidation;
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

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(Message userMessage)
        {
            //1
            //if (string.IsNullOrWhiteSpace(userMessage.name))
            //{
            //    ModelState.AddModelError("name", "Поле Name обязательно к заполнению");
            //}

            MessageValidator rules = new MessageValidator();
            var result = rules.Validate(userMessage);

            //список ошиббок при проверки
            var errosrs = result.Errors;

            //генерирует ошибку, выдвет сиключение
            rules.ValidateAndThrow(userMessage);

            //throw new Exception();

            if(result.IsValid)
            //if (ModelState.IsValid)
            {
                return View();
            }
            else
            {
                return View(userMessage);
            }

            
        }


        //NewsLetterSignUp
        [HttpPost]
        public IActionResult NewsLetterSignUp(string email, string phone)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("email","Поле обязательно должно быть заполненым");
            }

            if(ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Contact");
            }            
        }
    }
}
