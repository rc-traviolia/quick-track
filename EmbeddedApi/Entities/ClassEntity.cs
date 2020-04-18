using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickTrackWeb.EmbeddedApi.Entities
{
    public class ClassEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string OwnerIdentityName { get; set; }

        public List<Week> Weeks {get; set;}
        public List<Student> Students { get; set; }
        public List<TrackedItem> TrackedItems { get; set; }
        public List<ProgressRecord> ProgressRecords { get; set; }

        public static ClassEntity GetNullObject()
        {
            ClassEntity nullClassEntity = new ClassEntity();
            nullClassEntity.Id = -1;
            nullClassEntity.Name = "You must create a class to use this Application!";
            nullClassEntity.OwnerIdentityName = "no owner, this is the null class";
            nullClassEntity.Weeks = new List<Week>();
            nullClassEntity.Students = new List<Student>();
            nullClassEntity.TrackedItems = new List<TrackedItem>();
            nullClassEntity.ProgressRecords = new List<ProgressRecord>();
            return nullClassEntity;
        }


    }
}
