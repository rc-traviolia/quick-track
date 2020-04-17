using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuickTrackWeb.Shared.Models
{
    public class TrackedItemForUpdateDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must still have a name for this Tracked Item.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must still provide a unit like: min, hrs, minutes, or hours for this Tracked Item.")]
        public string UnitOfMeasure { get; set; }

        [Required(ErrorMessage = "You must still provide a daily goal for this Tracked Item.")]
        public int DailyTarget { get; set; }
    }
}
