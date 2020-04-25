using System;
using System.Collections.Generic;
using System.Text;

namespace QuickTrackWeb.Shared.Models
{
    class ClassEntityCompleteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerIdentityName { get; set; }
        public ICollection<WeekDto> Weeks { get; set; } = new List<WeekDto>();
        public ICollection<StudentDto> Students { get; set; } = new List<StudentDto>();
        public ICollection<TrackedItemDto> TrackedItems { get; set; } = new List<TrackedItemDto>();
        public ICollection<ProgressRecordDto> ProgressRecords { get; set; } = new List<ProgressRecordDto>();
        public int NumberOfWeeks
        {
            get
            {
                return Weeks.Count;
            }
        }
        public int NumberOfStudents
        {
            get
            {
                return Students.Count;
            }
        }
        public int NumberOfTrackedItems
        {
            get
            {
                return TrackedItems.Count;
            }
        }
        public int NumberOfProgressRecords
        {
            get
            {
                return ProgressRecords.Count;
            }
        }
    }
}
