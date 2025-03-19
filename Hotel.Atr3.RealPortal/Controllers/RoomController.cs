using Microsoft.AspNetCore.Mvc;

namespace Hotel.Atr3.RealPortal.Controllers
{
	[Route("hotel-atr-rooms")]
    public class RoomController : Controller
    {
		[HttpGet("")]
		[HttpGet("Index")]
		[HttpGet("all-room")]
        public IActionResult Index()
        {
            return View();
        }

		/// <summary>
		/// details/101 → Показать номер с ID 101
		/// </summary>
		/// <param name="roomId"></param>
		/// <returns></returns>
		[HttpGet("room-details/{roomId:int}")]
		public IActionResult RoomDetails(int roomId)
        {
            return View();
        }

		/// <summary>
		/// rooms?category=luxury → Показать номера категории "люкс"
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
		[HttpGet("room-list/{category}")]
		public IActionResult RoomList(string category)
        {
            return View();
        }

		/// <summary>
		/// rooms/standard/2 → Показать стандартные номера на 2 человек
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
		[HttpGet("room-list/{category}/{capacity:int}")]
		public IActionResult RoomList(string category, int capacity)
        {
            return View();
        }
    }
}
