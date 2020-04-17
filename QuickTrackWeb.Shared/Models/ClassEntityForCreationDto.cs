
using System.ComponentModel.DataAnnotations;

namespace QuickTrackWeb.Shared.Models
{
    public class ClassEntityForCreationDto
    {

        [Required(ErrorMessage = "You must provide a name for this Class")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Every class must be assigned an OwnerIdentityName (Account e-mail address)")]
        public string OwnerIdentityName { get; set; }
        
    }
}
