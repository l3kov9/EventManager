namespace EventManager.Web.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.EventViewModels;
    using Services;
    using System;

    [Authorize]
    public class EventsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IEventService events;

        public EventsController(UserManager<User> userManager, IEventService events)
        {
            this.userManager = userManager;
            this.events = events;
        }

        [HttpGet]
        public ActionResult Create()
             => base.View(new EventViewModel()
             {
                 StartDate = DateTime.Now,
                 EndDate = DateTime.Now.AddDays(1)
             });

        [HttpPost]
        public ActionResult Create(EventViewModel @event)
        {
            if (!ModelState.IsValid)
            {
                return View(@event);
            }

            var currentUserId = this.userManager.GetUserId(User);
            this.events.CreateEvent(@event.Name, @event.Location, @event.StartDate, @event.EndDate, currentUserId);
            this.TempData.AddSuccessMessage("You've successfully created an event");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var @event = this.events.ById(id);

            if (@event == null)
            {
                return NotFound();
            }

            return View(new EventViewModel()
            {
                Id = id,
                Name = @event.Name,
                Location = @event.Location,
                StartDate = @event.StartDate,
                EndDate = @event.EndDate
            });
        }

        [HttpPost]
        public ActionResult Edit(EventViewModel @event)
        {
            if (!ModelState.IsValid)
            {
                return View(@event);
            }

            @event.UserId = this.userManager.GetUserId(User);

            var success = this.events.Edit(@event.Id, @event.Name, @event.Location, @event.StartDate, @event.EndDate, @event.UserId);

            if (success)
            {
                this.TempData.AddSuccessMessage("You've successfully edited an event");

                return RedirectToAction("Index", "Home");
            }
            else
            {
                this.TempData.AddErrorMessage("You can't edit this event");

                return View(@event);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var success = this.events.DeleteById(id, userId);

            if (!success)
            {
                return NotFound();
            }

            if (success)
            {
                this.TempData.AddSuccessMessage("You've successfully deleted an event");
            }
            else
            {
                this.TempData.AddErrorMessage("You can't delete this event");
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult All()
        {
            var userId = this.userManager.GetUserId(User);

            var allEvents = this.events.All();

            return View(new EventListingViewModel()
            {
                Events = allEvents,
                CurrentUserId = userId
            });
        }

        public ActionResult MyEvents()
        {
            var userId = this.userManager.GetUserId(User);

            var myEvents = this.events.AllByUserId(userId);

            return View(new EventListingViewModel()
            {
                Events = myEvents,
                CurrentUserId = userId
            });
        }
    }
}
