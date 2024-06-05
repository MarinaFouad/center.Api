using center.Api.Dbcontexts;
using center.Api.Entities;
using center.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace center.Api.Services
{
    public class StudentRepo : IStudentRepo
    {
        private readonly CenterDbContext _context;
        public StudentRepo(CenterDbContext context)
        { 
            _context = context;
        }
        public async Task<bool> StudentExistsAsync(int studentId)
        {
            return await _context.Students.AnyAsync(student => student.Id == studentId);
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _context.Students
                 .Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }
        public async Task AddExamForStudentAsync(int studentId, Exam exam)
         {
            var student = await GetStudentByIdAsync(studentId);
            if (student != null)
            {
                student.Exams.Add(exam);
            }
        }

        public async Task AddStudentAsync(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            await _context.Students.AddAsync(student);
        }

        public void DeleteStudent(Student student)
        {
             
            _context.Students.Remove(student);
            

        }

        public void DeleteExam(Exam exam)
        {
            var currentExam = _context.Exams.FirstOrDefault(e=>e.Id == exam.Id);
            if (currentExam != null)
            {
                _context.Exams.Remove(exam);
            }

        }

        public async Task<IEnumerable<Exam>> GetAllExamsOfStudentAsync(int studentId)
        {
            return await _context.Exams
                           .Where(p => p.StudentId== studentId).ToListAsync();
        }

        public async Task<Exam?> GetExamForStudentAsync(int studentId, int ExamId)
        {
            return await _context.Exams
               .Where(p => p.StudentId == studentId && p.Id == ExamId)
               .FirstOrDefaultAsync();
        }

        

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        
    }
}