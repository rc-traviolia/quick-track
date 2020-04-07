using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickTrackWeb.Entities
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("ClassEntityId")]
        public ClassEntity ClassEntity { get; set; }
        public int ClassEntityId { get; set; }
    }
}
