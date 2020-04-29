
namespace QuickTrackWeb.Shared.Models
{
    public class ProgressRecordDto
    {
    
        public int Id { get; set; }
        public int ClassEntityId { get; set; }
        public int StudentId { get; set; }
        public int TrackedItemId { get; set; }
        public int WeekId { get; set; }
        public int Progress { get; set; }

        public static ProgressRecordDto GetNullProgressRecord(int ClassEntityId, int StudentId, int TrackedItemId, int WeekId)
        {
            ProgressRecordDto nullProgressRecord = new ProgressRecordDto();
            
            nullProgressRecord.ClassEntityId = ClassEntityId;
            nullProgressRecord.StudentId = StudentId;
            nullProgressRecord.WeekId = WeekId;
            nullProgressRecord.TrackedItemId = TrackedItemId;
            nullProgressRecord.Progress = 0;

            return nullProgressRecord;
        }

    }
}
