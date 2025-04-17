using Hotel.Atr3.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hotel.Atr3.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HttpClient client = new HttpClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Position()
        {
            List<Position> positions = new List<Position>();

            using (var request = await client.GetAsync("http://localhost:5258/api/Position"))
            {
                string result = await request.Content.ReadAsStringAsync();
                positions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Position>>(result);
            }

            return View(positions);
        }

        public async Task<IActionResult> EditPosition(int id)
        {
            Position position = new Position();

            if (id != 0)
            {
                using (var request = await client.GetAsync("http://localhost:5258/api/Position/" + id))
                {
                    string result = await request.Content.ReadAsStringAsync();
                    position = Newtonsoft.Json.JsonConvert.DeserializeObject<Position>(result);
                }

            }
            return View(position);
        }

        [HttpPost]
        public async Task<IActionResult> EditPosition(Position position)
        {
            if (position.Id != 0)
            {
                var formData = new Dictionary<string, string>
                    {
                        { "Name", position.Name },
                        { "Description", position.Description },
                    };

                var content = new FormUrlEncodedContent(formData);

                using (var request = await client.PutAsync("http://localhost:5258/api/Position/", content))
                {
                    string result = await request.Content.ReadAsStringAsync();
                }
            }
            else
            {
                //position.CreatedBy = "admin";
                //position.CreateAt = DateTime.Now;

                //_db.Positions.Add(position);
            }
          
            return RedirectToAction("Position");
        }

        public IActionResult DeletePosition(int id)
        {
            //var _position = _db.Positions.Find(id);
            //if (_position != null)
            //{
            //    _db.Positions.Remove(_position);
            //    _db.SaveChanges();
            //}
            return RedirectToAction("Position");
        }



        public IActionResult Team()
        {
            //var teams = _db.Teams.ToList();

            return View(teams);
        }

        public IActionResult EditTeam(int id)
        {
            var tvm = new TeamViewModel();
            tvm.Positions = _db.Positions.ToList();
            tvm.Team = new Team();

            if (id != 0)
            {
                var team = _db.Teams.Find(id);
                if (team != null)
                    tvm.Team = team;
            }

            return View(tvm);
        }

        [HttpPost]
        public async Task<IActionResult> EditTeam(IFormFile ImagePath, Team team)
        {
            var memoryStream = new MemoryStream();
            ImagePath.CopyTo(memoryStream);

            if (team.Id != 0)
            {
                var _team = _db.Teams.Find(team.Id);
                if (_team != null)
                {
                    _team.FirstName = team.FirstName;
                    _team.SecondName = team.SecondName;
                    _team.MiddleName = team.MiddleName;
                    _team.AboutTeam = team.AboutTeam;
                    _team.PositionId = team.PositionId;

                    if (ImagePath != null)
                        team.ImagePath = memoryStream.ToArray();

                    _db.SaveChanges();
                }
            }
            else
            {
                using (var client = new HttpClient())
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        //прикрепляем к закголовку картинку
                        var fileBites = memoryStream.ToArray();
                        var fileContent = new ByteArrayContent(fileBites);

                        content.Add(fileContent, "file", ImagePath.FileName);

                        //прикрепляем к заголовку свойства класса
                        content.Add(new StringContent(team.FirstName), "FirstName");
                        content.Add(new StringContent(team.SecondName), "SecondName");
                        content.Add(new StringContent(team.MiddleName), "MiddleName");
                        content.Add(new StringContent(team.AboutTeam), "AboutTeam");
                        content.Add(new StringContent(team.PositionId.ToString()), "PositionId");

                        using (var request = await client.PostAsync("http://localhost:5193/api/Team",
                            content))
                        {
                            var result = await request.Content.ReadAsStringAsync();

                        }
                    }
                }
            }
            return RedirectToAction("Team");
        }
        public IActionResult DeleteTeam(int id)
        {
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
