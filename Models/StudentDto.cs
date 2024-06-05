using center.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace center.Api.Models
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }= string.Empty;
        public int Age { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; } = string.Empty;



        public ICollection<ExamDto> Exams { get; set; } = new List<ExamDto>();

        public int ExamCount { get { return Exams.Count; } }

    }
}
