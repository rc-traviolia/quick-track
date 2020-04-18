using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickTrackWeb.EmbeddedApi.Entities
{
    public class Week
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public int Number { get; set; }

        [Required]
        public DateTime MondayDate { get; set; }

        public int DayCount { get; set; }

        [ForeignKey("ClassEntityId")]
        public ClassEntity ClassEntity { get; set; }
        public int ClassEntityId { get; set; }

        public List<ProgressRecord> ProgressRecords { get; set; }

        public static Week GetNullObject()
        {
            Week nullWeek = new Week();
            nullWeek.Id = -1;
            nullWeek.Number = -1;
            nullWeek.MondayDate = new DateTime();
            nullWeek.DayCount = -1;
            nullWeek.ClassEntityId = -1;
            nullWeek.ProgressRecords = new List<ProgressRecord>();
            return nullWeek;
        }
    }
}
