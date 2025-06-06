﻿using FluentValidation;
using Hotel.Atr3.RealPortal.AppFilter;
using Hotel.Atr3.RealPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hotel.Atr3.RealPortal.Controllers
{
    //[TypeFilter(typeof(TimeElapsedFilter))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<AppUser> _userManager;
        private readonly IMessage _sender;

        public HomeController(ILogger<HomeController> logger,
            UserManager<AppUser> userManager,
            IMessage sender)
        {
            _logger = logger;
            _userManager = userManager;
            _sender = sender;
        }


        public async Task<IActionResult> Index()
        {
            throw new Exception("Test Exception");

            _logger.LogCritical("Test logger - LogCritical");
            _logger.LogError("Test logger - LogError");
            _logger.LogInformation("Test logger - LogInformation");
            _logger.LogDebug("Test logger - LogDebug");

            //AppUser user = new AppUser();
            //user.UserName = "secondAdmin";
            //user.Email = "gersen.e.a@gmail.com";
            //var result = await _userManager.CreateAsync(user, "Gg110188@");

            //if (result.Succeeded)
            //{

            //}

            return View();

        }


        //[IEFilter]
        //->START [ResourceFilter]:OnResourceExecuting
        //->START [ActionFilter]:TimeElapsedFilter
        public IActionResult Contact()
        {
            Thread.Sleep(1000);
            var test = "";
            //-> START [ResourceFilter]:OnResourceExecuted
            return View();
        }
        //->START [ActionFilter]:TimeElapsedFilter


        [HttpPost]
        public IActionResult Contact(Message userMessage)
        {
            //EmailSender sender = new EmailSender();
            //SmsSender sender = new SmsSender();


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
            //rules.ValidateAndThrow(userMessage);

            //throw new Exception();

            if(result.IsValid)
            //if (ModelState.IsValid)
            {
               _sender.SendMessage(userMessage.email, userMessage.message, "");

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


        public JsonResult SetCity(string city)
        {
            try
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddMinutes(1);

                Response.Cookies.Append("city", city, option);
                return Json(city);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }


        [HttpPost]
        public JsonResult ChangeCulture(string culture)
        {
            Response.Cookies.Append(                
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions {Expires = DateTime.Now.AddMonths(1) });

            return Json(culture);
        }

        public IActionResult Error(string message)
        {
            return View("Error", message);
        }
    }
}
