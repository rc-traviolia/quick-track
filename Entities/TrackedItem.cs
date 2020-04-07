using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickTrackWeb.Entities
{
    public class TrackedItem
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [ForeignKey("ClassEntityId")]
        public ClassEntity ClassEntity { get; set; }
        public int ClassEntityId { get; set; }

    }
}
