using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace center.Api.Entities
{
    public class Exam
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        [Length(10,100)]
        public string Description { get; set; } = string.Empty;
        [Range(0,100)]
        public int Degree { get; set; }
        [ForeignKey("StudentId")]
        public int StudentId { get; set; }
        public Student? Student { get; set; }


    }
}
