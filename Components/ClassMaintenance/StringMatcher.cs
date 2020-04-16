using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuickTrackWeb.Components.ClassMaintenance
{
    public class StringMatcher
    {
        [Compare("MatchTo", ErrorMessage = "You must enter the exact Class Name to delete it!")]
        public string Input { get; set; }

        public string MatchTo { get; set; }

        public StringMatcher(string matchInput)
        {
            MatchTo = matchInput;
        }
    }
}
