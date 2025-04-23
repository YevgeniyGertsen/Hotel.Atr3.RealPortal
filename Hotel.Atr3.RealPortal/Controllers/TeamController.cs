using Hotel.Atr3.RealPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;

namespace Hotel.Atr3.RealPortal.Controllers
{
    public class TeamController : Controller
    {
        private readonly AppDbContext _db;
        public TeamController(AppDbContext db)
        {
            _db = db;
		}

        public async Task<IActionResult> Index()
        {
            List<Team> teams = new List<Team>();
            using (var client = new HttpClient())
            {
                var token = Request.Cookies["token"];

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                using (var responce = await client.GetAsync("http://localhost:5258/api/Team"))
                {
                    var result = await responce.Content.ReadAsStringAsync();
                    teams = JsonConvert.DeserializeObject<List<Team>>(result);
                }
            }

            return View(teams);


            //var teams = _db.Teams
            //    .Include(i => i.Position)
            //    .ToList();

            //return View(teams);

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
