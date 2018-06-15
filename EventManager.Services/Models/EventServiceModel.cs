namespace EventManager.Services.Models
{
    using System;

    public class EventServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Location { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }
    }
}
