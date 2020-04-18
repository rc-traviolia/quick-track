using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickTrackWeb.EmbeddedApi.Entities
{
    public class ProgressRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Progress { get; set; }

        //[Required]
        [ForeignKey("ClassEntityId")]
        public ClassEntity ClassEntity { get; set; }
        public int ClassEntityId { get; set; }

        //[Required]
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        public int StudentId { get; set; }

        //[Required]
        [ForeignKey("TrackedItemId")]
        public TrackedItem TrackedItem { get; set; }
        public int TrackedItemId { get; set; }

        //[Required]
        [ForeignKey("WeekId")]
        public Week Week { get; set; }
        public int WeekId { get; set; }

        public static ProgressRecord GetNullObject()
        {
            ProgressRecord nullProgressRecord = new ProgressRecord();
            nullProgressRecord.Id = -1;
            nullProgressRecord.Progress = 0;
            nullProgressRecord.ClassEntityId = -1;
            nullProgressRecord.StudentId = -1;
            nullProgressRecord.TrackedItemId = -1;
            nullProgressRecord.WeekId = -1;
            return nullProgressRecord;
        }

    }
}
