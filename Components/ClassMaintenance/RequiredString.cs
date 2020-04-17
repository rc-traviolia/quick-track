using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
     
namespace QuickTrackWeb.Components.ClassMaintenance
{
    public class RequiredString
    {
        [Required( ErrorMessage = "You must enter a name to Add  ")]
        public string Value { get; set; }
    }
}
