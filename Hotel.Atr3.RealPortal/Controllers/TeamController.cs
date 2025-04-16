using Hotel.Atr3.RealPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hotel.Atr3.RealPortal.Controllers
{
    public class TeamController : Controller
    {
        private readonly AppDbContext _db;
        public TeamController(AppDbContext db)
        {
            _db = db;
		}

        public IActionResult Index()
        {
            var teams = _db.Teams
                .Include(i => i.Position)
                .ToList();

            return View(teams);

			//#region GetTeamsFromDataBase
			//List<TeamViewModel> teams = new List<TeamViewModel>();

   //         teams.Add(new TeamViewModel()
   //             {
   //                 ImagePath = "img/team/1.jpg",
   //                 FullName = "Kathy Luis",
   //                 Position= "( Officer )",
   //                 Description = "Lorem ipsupm dolor sit amet, conse ctetur adipisicing elit, sed do eiumthgtipsupm dolor sit amet conse",
   //                 TeamLinks = new List<TeamLinks>()
   //                 {
   //                     new TeamLinks(){ URL="https://www.facebook.com/", LinkType="zmdi-facebook" }
   //                 }
   //             });
   //         #endregion

            //return View(teams);
        }
    }
}
