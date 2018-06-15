namespace EventManager.Web.Models.EventViewModels
{
    using Services.Models;
    using System.Collections.Generic;

    public class EventListingViewModel
    {
        public IEnumerable<EventServiceModel> Events { get; set; }

        public string CurrentUserId { get; set; }
    }
}
