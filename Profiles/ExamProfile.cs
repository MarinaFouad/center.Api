using AutoMapper;

namespace center.Api.Profiles
{
    public class ExamProfile : Profile
    {
        public ExamProfile()
        {
            CreateMap<Entities.Exam, Models.ExamDto>(); 
        }
    }
}
