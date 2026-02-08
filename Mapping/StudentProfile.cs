using AutoMapper;
using StudentAPI.Entity;
using StudentAPI.DTO ;
namespace StudentAPI.Mapping;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentDto>()
            .ForMember(dest => dest.CourseTitle,
                opt => opt.MapFrom(src => src.Course.Title));

        CreateMap<CreateStudentDto, Student>().ReverseMap(); 
        CreateMap<UpdateStudentDto, Student>().ReverseMap();
        CreateMap<Student, StudentResponseDto>().ReverseMap();
    }
}