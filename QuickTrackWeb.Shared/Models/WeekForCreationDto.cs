﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuickTrackWeb.Shared.Models
{
    public class WeekForCreationDto
    {
        [Required(ErrorMessage = "A week must have a number assigned to it")]
        public int Number { get; set; }

        [Required(ErrorMessage = "A week must have a Monday assigned to it")]
        public DateTime MondayDate { get; set; }

        [Required(ErrorMessage = "A week must have some number of days")]
        [Range(0, 5, ErrorMessage ="A week can be from 0 (an entire week off) to 5 (full M-F week) days long")]
        public int DayCount { get; set; }

        public WeekForCreationDto()
        {

        }
        public WeekForCreationDto(WeekWithoutProgressDto previousWeek)
        {
            Number = previousWeek.Number + 1;
            MondayDate = previousWeek.MondayDate.AddDays(7);
            DayCount = 5; //TODO: method to get day count based on recognized holidays. upload school calendar? Hah!
        }
    }
}
