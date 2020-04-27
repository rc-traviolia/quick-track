using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuickTrackWeb.Shared.Models
{
    public class ProgressRecordForCreationDto
    {
        [Required]
        public int ClassEntityId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int TrackedItemId { get; set; }

        [Required]
        public int WeekId { get; set; }

        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage = "This must be a positive integer")]
        public int Progress { get; set; }
    }
}
