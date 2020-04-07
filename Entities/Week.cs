using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickTrackWeb.Entities
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

        [ForeignKey("ClassEntityId")]
        public ClassEntity ClassEntity { get; set; }
        public int ClassEntityId { get; set; }
    }
}
