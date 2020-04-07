using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickTrackWeb.Entities
{
    public class ProgressRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        public int StudentId { get; set; }

        [ForeignKey("TrackedItemId")]
        public TrackedItem TrackedItem { get; set; }
        public int TrackedItemId { get; set; }

        [ForeignKey("WeekId")]
        public Week Week { get; set; }
        public int WeekId { get; set; }

    }
}
