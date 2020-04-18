using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickTrackWeb.EmbeddedApi.Entities
{
    public class TrackedItem
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string UnitOfMeasure { get; set; }

        public int DailyTarget { get; set; }

        [ForeignKey("ClassEntityId")]
        public ClassEntity ClassEntity { get; set; }
        public int ClassEntityId { get; set; }

        public List<ProgressRecord> ProgressRecords { get; set; }

        public static TrackedItem GetNullObject()
        {
            TrackedItem nullTrackedItem = new TrackedItem();
            nullTrackedItem.Id = -1;
            nullTrackedItem.Name = "No Tracked Item found";
            nullTrackedItem.UnitOfMeasure = "minutes";
            nullTrackedItem.DailyTarget = 15;
            nullTrackedItem.ClassEntityId = -1;
            nullTrackedItem.ProgressRecords = new List<ProgressRecord>();
            return nullTrackedItem;
        }

    }
}
