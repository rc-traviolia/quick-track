using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuickTrackWeb.Shared.Models
{
    public class ClassEntityForUpdateDto
    {
        [Required(ErrorMessage = "You still must provide a name for this Class")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide an Owner Identity Name. You cannot remove them from Classes.")]
        public string OwnerIdentityName { get; set; }
    }
}
