using System.ComponentModel.DataAnnotations;

namespace QuickTrackWeb.Shared.Models
{
    public class TrackedItemForCreationDto
    {
        [Required(ErrorMessage ="You must provide a name for a new Tracked Item.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must provide a unit like: min, hrs, minutes, or hours for a new Tracked Item.")]
        public string UnitOfMeasure { get; set; }

        [Required(ErrorMessage ="You must provide a daily goal for a new Tracked Item.")]
        public int DailyTarget { get; set; }
    }
}
