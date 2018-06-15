namespace EventManager.Web.Models.EventViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class EventViewModel : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Event name can't be empty.")]
        [MinLength(MinInputLength)]
        [MaxLength(MaxInputLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location can't be empty.")]
        [MinLength(MinInputLength)]
        [MaxLength(MaxInputLength)]
        public string Location { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string UserId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartDate < DateTime.UtcNow)
            {
                yield return new ValidationResult("Start date should be in the future.");
            }

            if (this.StartDate > this.EndDate)
            {
                yield return new ValidationResult("Start date should be before end date.");
            }
        }
    }
}
