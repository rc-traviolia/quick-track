using System;
using System.Collections.Generic;

namespace QuickTrackWeb.Shared.Models
{
    public class WeekDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime MondayDate { get; set; }
        public int DayCount { get; set; }
        public ICollection<ProgressRecordDto> ProgressRecords { get; set; } = new List<ProgressRecordDto>();

    }
}
