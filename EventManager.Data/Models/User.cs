namespace EventManager.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public IEnumerable<Event> Events { get; set; } = new List<Event>();
    }
}
