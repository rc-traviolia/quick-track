using System.Collections.Generic;

namespace QuickTrackWeb.Shared.Models
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProgressRecordDto> ProgressRecords { get; set; } = new List<ProgressRecordDto>();
    }
}
