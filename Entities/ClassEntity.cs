using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickTrackWeb.Entities
{
    public class ClassEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string OwnerIdentityName { get; set; }

        public List<Week> Weeks {get; set;}
        public List<Student> Students { get; set; }
        public List<TrackedItem> TrackedItems { get; set; }
        public List<ProgressRecord> ProgressRecords { get; set; }


    }
}
