namespace EventManager.Web.Controllers
{
    using Data.Models;
    using EventManager.Web.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.EventViewModels;
    using Services;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IEventService events;

        public HomeController(UserManager<User> userManager, IEventService events)
        {
            this.userManager = userManager;
            this.events = events;
        }

        public IActionResult Index()
        {
            var latestEvents = this.events.GetUpcomingEvents(6);

            return View(new EventListingViewModel
            {
                Events = latestEvents,
                CurrentUserId = this.userManager.GetUserId(User)
            });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
