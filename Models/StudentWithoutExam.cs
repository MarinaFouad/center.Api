using System.ComponentModel.DataAnnotations;

namespace center.Api.Models
{
    public class StudentWithoutExam
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public int Phone { get; set; } 
        public string Address { get; set; } = string.Empty;

    }
}
