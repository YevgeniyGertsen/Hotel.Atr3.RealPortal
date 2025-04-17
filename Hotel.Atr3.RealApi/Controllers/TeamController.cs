using Hotel.Atr3.RealApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Atr3.RealApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly AppDbContext _db;
        public TeamController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public List<Team> Get()
        {
            return _db.Teams.ToList();
        }

        [HttpPost]
        public IActionResult Post([FromForm] Team team, IFormFile file)
        {
            try
            {
                if(file!=null)
                {
                    var memory = new MemoryStream();
                    file.CopyTo(memory);
                    team.ImagePath = memory.ToArray();
                }

                team.CreatedBy = "admin";
                team.CreateAt = DateTime.Now;

                _db.Teams.Add(team);
                _db.SaveChanges();

                return Ok(new { message = "Сотрудник успешно добавлен" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
