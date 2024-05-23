
using BlogerMVC.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogerMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventService _eventService;

        public HomeController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public IActionResult Index()
        {
            var events= _eventService.GetAllEvents();
            return View(events);
        }

        
    }
}
