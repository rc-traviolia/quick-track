using System.Collections.Generic;

namespace QuickTrackWeb.Shared.Models
{
    public class ClassEntityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerIdentityName { get; set; }
        public ICollection<WeekWithoutProgressDto> Weeks { get; set; } = new List<WeekWithoutProgressDto>();
        public ICollection<StudentWithoutProgressDto> Students { get; set; } = new List<StudentWithoutProgressDto>();
        public ICollection<TrackedItemWithoutProgressDto> TrackedItems { get; set; } = new List<TrackedItemWithoutProgressDto>();
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


    }
}
