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
            nullWeek.MondayDate = DateTime.Now;
            if (nullWeek.MondayDate.DayOfWeek != DayOfWeek.Monday)
            {

                var offset = (int)DayOfWeek.Monday - (int)nullWeek.MondayDate.DayOfWeek;

                //following two lines could be combined.
                var monday = nullWeek.MondayDate + TimeSpan.FromDays(offset);
                nullWeek.MondayDate = monday;

            }
            nullWeek.DayCount = 5;

            return nullWeek;
        }
    }
}
