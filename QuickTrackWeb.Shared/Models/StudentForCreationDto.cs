using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuickTrackWeb.Shared.Models
{
    public class StudentForCreationDto
    {
        [Required(ErrorMessage="You must name a new student.")]
        public string Name { get; set; }
    }
}
