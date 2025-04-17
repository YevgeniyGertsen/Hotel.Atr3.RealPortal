using Hotel.Atr3.RealApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Atr3.RealApi.Controllers
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

        [HttpPost("createPosition")]
        public IActionResult CreatePosition(Position position)
        {
            try
            {
                position.CreatedBy = "admin";
                position.CreateAt = DateTime.Now;

                _db.Positions.Add(position);
                _db.SaveChanges();

                return Ok(new { message = "Должность добавлена успешно!" });
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message;

                return BadRequest(new { message = ex.Message + "innerMessage: " + innerMessage });
            }
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

        [HttpPut]
        public IActionResult Put([FromForm]Position position)
        {
            try
            {
                var _position = _db.Positions.Find(position.Id);
                if (_position != null)
                {
                    _position.Name = position.Name;
                    _position.Description = position.Description;
                    _db.SaveChanges();

                    return Ok(new { message = "Должность успешно изменена!" });
                }
                else
                {
                    return NotFound(new { message="Должность не найдена!"});
                }
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message;

                return BadRequest(new { message = ex.Message + "innerMessage: " + innerMessage });
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var position = _db.Positions.Find(id);
                if (position != null)
                {
                    _db.Positions.Remove(position);
                    _db.SaveChanges();
                    return Ok(new { message = "Должность успешно удалена!" });
                }
                else
                {
                    return NotFound(new { message = "Должность не найдена" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
