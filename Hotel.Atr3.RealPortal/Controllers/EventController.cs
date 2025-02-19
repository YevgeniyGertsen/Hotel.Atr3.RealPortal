using Hotel.Atr3.RealPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Atr3.RealPortal.Controllers
{
    public class EventController : Controller
    {




        public IActionResult Index()
        {
            #region GET data from DataBase

            List<EventViewModel> events = new List<EventViewModel>();
            events.Add(new EventViewModel()
            {
                ImageSize = 2,
                PathImage = "~/img/event/1.jpg",
                EventTitle = "management conferences",
                AuthorName = "By : Jhtyg Hyuir",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt"
            });

            #endregion

            return View(events);
        }
    }
}
