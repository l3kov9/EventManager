namespace EventManager.Services
{
    using Models;
    using System;
    using System.Collections.Generic;

    public interface IEventService
    {
        IEnumerable<EventServiceModel> GetUpcomingEvents(int totalEvents);

        void CreateEvent(string name, string location, DateTime startDate, DateTime endDate, string currentUserId);

        EventServiceModel ById(int id);

        bool Edit(int id, string name, string location, DateTime startDate, DateTime endDate, string userId);

        bool DeleteById(int id, string userId);

        IEnumerable<EventServiceModel> All();

        IEnumerable<EventServiceModel> AllByUserId(string userId);
    }
}
