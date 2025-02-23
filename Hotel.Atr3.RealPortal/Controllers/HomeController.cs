﻿using FluentValidation;
using Hotel.Atr3.RealPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hotel.Atr3.RealPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger,
            UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        //[HttpGet]
        public async Task<IActionResult> Index()
        {
            //AppUser user = new AppUser();
            //user.UserName = "secondAdmin";
            //user.Email = "gersen.e.a@gmail.com";
            //var result = await _userManager.CreateAsync(user, "Gg110188@");

            //if (result.Succeeded)
            //{

            //}

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
    }
}
