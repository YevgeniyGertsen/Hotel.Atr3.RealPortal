using Hotel.Atr3.WebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Atr3.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly AppDbContext _db;
        public PositionController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public List<Position> Get()
        {
            return _db.Positions.ToList();
        }

        [HttpGet("[action]/{id:int}")]
        public Position Get(int id)
        {
            return _db.Positions.Find(id);
        }
    }
}
