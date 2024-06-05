using AutoMapper;
using center.Api.Models;

namespace center.Api.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile() {
            CreateMap<Entities.Student, Models.StudentWithoutExam>();
            CreateMap<Entities.Student,Models.StudentDto>();
            CreateMap<Models.StudentWithoutExam, Entities.Student>();

        }
    }
}
