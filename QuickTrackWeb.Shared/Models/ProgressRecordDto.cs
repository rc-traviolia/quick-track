
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
        
    }
}
