using AutoMapper;
using center.Api.Entities;
using center.Api.Models;
using center.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace center.Api.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IStudentRepo _studentRepo;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepo studentRepo , IMapper mapper)
        {  _studentRepo = studentRepo; 
        _mapper = mapper;
        }
        [HttpGet("AllStudents")]

        public async Task<ActionResult<IEnumerable<StudentWithoutExam>>>
            GetAllStudent()
        {
            var Sudents = await _studentRepo.GetStudentsAsync();
            var studentWithoutExam = _mapper.Map<IEnumerable<StudentWithoutExam>>(Sudents);
            return Ok(studentWithoutExam);
        }
        /// <response code="201">Returns the Student by ID</response>
        /// <response code="404">If the item Not Found</response>
        [HttpGet("student")]
        
        public async Task<ActionResult<IEnumerable<StudentWithoutExam>>> 
            GetStudent( int id)
        {
            if(!await _studentRepo.StudentExistsAsync(id))
            {
                return NotFound();
            }
            var currentStudent =await _studentRepo.GetStudentByIdAsync(id);
            var studentWithoutExam = _mapper.Map<StudentWithoutExam>(currentStudent);
            return Ok(studentWithoutExam);
        }
        /// <summary>
        /// Add a student
        /// </summary>
        /// 
        [HttpPost("add")]
        public async Task<ActionResult<IEnumerable<StudentWithoutExam>>> AddStudent(StudentWithoutExam student)
        {
        
 // return Ok(student);
            var studentAdded = _mapper.Map<Entities.Student>(student);
            await _studentRepo.AddStudentAsync(studentAdded);
            await _studentRepo.SaveChangesAsync();
          
            var studentDto = _mapper.Map<Models.StudentWithoutExam>(studentAdded);

            return CreatedAtAction(nameof(GetStudent), new { id = studentDto.Id }, "successfully added");
        }

        /// <summary>
        /// Deletes a specific Student.
        /// </summary>
        /// 
        [Authorize]
        [HttpDelete("Delete")]

        public async Task<ActionResult> DeleteStudent(int id)
        {
            if(!await _studentRepo.StudentExistsAsync(id))
            {
                return NotFound();
            }
            var student = await _studentRepo.GetStudentByIdAsync(id);
            _studentRepo.DeleteStudent(student);
            await _studentRepo.SaveChangesAsync();
            return Ok("Student deleted");
        }



    }
}
