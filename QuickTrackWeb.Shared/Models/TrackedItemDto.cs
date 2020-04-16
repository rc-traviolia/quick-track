using System.Collections.Generic;

namespace QuickTrackWeb.Shared.Models
{
    public class TrackedItemDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string UnitOfMeasure { get; set; }

        public int DailyTarget { get; set; }
        public ICollection<ProgressRecordDto> ProgressRecords { get; set; } = new List<ProgressRecordDto>();

    }
}
