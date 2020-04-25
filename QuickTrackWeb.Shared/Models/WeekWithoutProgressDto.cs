using System;
using System.Collections.Generic;
using System.Text;

namespace QuickTrackWeb.Shared.Models
{
    public class WeekWithoutProgressDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime MondayDate { get; set; }

        public int DayCount { get; set; }

        public static WeekWithoutProgressDto GetNullWeek()
        {
            WeekWithoutProgressDto nullWeek = new WeekWithoutProgressDto();

            nullWeek.Id = -1;
            nullWeek.Number = 0;
            nullWeek.MondayDate = new DateTime();
            nullWeek.DayCount = 5;

            return nullWeek;
        }
    }
}
