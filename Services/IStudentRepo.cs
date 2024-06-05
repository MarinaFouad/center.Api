using center.Api.Entities;
using center.Api.Models;

namespace center.Api.Services
{
    public interface IStudentRepo
    {
         Task<IEnumerable<Student>> GetStudentsAsync();
         Task<Student> GetStudentByIdAsync(int id);
         Task<bool> StudentExistsAsync(int studentId);
        Task<IEnumerable<Exam>> GetAllExamsOfStudentAsync(int studentId);
        Task<Exam?> GetExamForStudentAsync(int studentId,
            int ExamId);
        Task AddExamForStudentAsync(int studentId, Exam exam);

        void DeleteStudent(Student student);
        void DeleteExam(Exam exam);
        Task AddStudentAsync(Student student);
        Task<bool> SaveChangesAsync();
    }
}
