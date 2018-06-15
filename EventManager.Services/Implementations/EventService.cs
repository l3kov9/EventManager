namespace EventManager.Services.Implementations
{
    using Data;
    using Data.Models;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EventService : IEventService
    {
        private readonly EventManagerDbContext db;

        public EventService(EventManagerDbContext db)
        {
            this.db = db;
        }

        public EventServiceModel ById(int id)
            => this.db
                .Events
                .Where(e => e.Id == id)
                .Select(e => new EventServiceModel
                {
                    Name = e.Name,
                    Location = e.Location,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate
                })
                .FirstOrDefault();

        public void CreateEvent(string name, string location, DateTime startDate, DateTime endDate, string currentUserId)
        {
            var @event = new Event()
            {
                Name = name,
                Location = location,
                StartDate = startDate,
                EndDate = endDate,
                UserId = currentUserId
            };

            this.db.Add(@event);

            this.db.SaveChanges();
        }

        public bool DeleteById(int id, string userId)
        {
            var @event = this.db
                .Events
                .Where(e => (e.Id == id && e.UserId == userId))
                .FirstOrDefault();

            if (@event == null)
            {
                return false;
            }

            this.db
                .Events
                .Remove(@event);

            this.db.SaveChanges();

            return true;
        }

        public bool Edit(int id, string name, string location, DateTime startDate, DateTime endDate, string userId)
        {
            var @event = this.db
                .Events
                .Where(e => (e.Id == id && e.UserId == userId))
                .FirstOrDefault();

            if (@event == null)
            {
                return false;
            }

            @event.Name = name;
            @event.Location = location;
            @event.StartDate = startDate;
            @event.EndDate = endDate;

            this.db.SaveChanges();

            return true;
        }

        public IEnumerable<EventServiceModel> All()
            => this.db
                .Events
                .OrderBy(e => e.StartDate)
                .Select(e => new EventServiceModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Location = e.Location,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Username = e.User.UserName,
                    UserId = e.User.Id
                });

        public IEnumerable<EventServiceModel> GetUpcomingEvents(int totalEvents)
            => this.db
                .Events
                .OrderBy(e => e.StartDate)
                .Where(e => e.StartDate >= DateTime.Now)
                .Select(e => new EventServiceModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Location = e.Location,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Username = e.User.UserName,
                    UserId = e.User.Id
                })
                .Take(totalEvents);

        public IEnumerable<EventServiceModel> AllByUserId(string userId)
            => this.db
                .Events
                .OrderBy(e => e.StartDate)
                .Select(e => new EventServiceModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Location = e.Location,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Username = e.User.UserName,
                    UserId = e.User.Id
                })
                .Where(e => e.UserId == userId);
    }
}
