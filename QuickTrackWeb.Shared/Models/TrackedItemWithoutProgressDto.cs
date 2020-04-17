using System;
using System.Collections.Generic;
using System.Text;

namespace QuickTrackWeb.Shared.Models
{
    public class TrackedItemWithoutProgressDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UnitOfMeasure { get; set; }
        public int DailyTarget { get; set; }
    }
}
