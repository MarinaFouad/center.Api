using System.ComponentModel.DataAnnotations;

namespace center.Api.Entities
{
    public class Student
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
        
        public int Age { get; set; }
        public int Phone { get; set; }
        [MaxLength(255)]
        public string Address { get; set; } = string.Empty;


        public ICollection<Exam>? Exams { get; set; }

    }
}
