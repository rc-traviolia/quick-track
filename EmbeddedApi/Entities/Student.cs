﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickTrackWeb.EmbeddedApi.Entities
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [ForeignKey("ClassEntityId")]
        public ClassEntity ClassEntity { get; set; }
        public int ClassEntityId { get; set; }

        public List<ProgressRecord> ProgressRecords { get; set; }
    }
}
