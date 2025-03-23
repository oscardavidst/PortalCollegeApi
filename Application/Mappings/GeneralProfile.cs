using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region DTOs
            CreateMap<Student, StudentDto>();
            CreateMap<Professor, ProfessorDto>();
            CreateMap<Course, CourseDto>();
            #endregion

            #region Commands
            //CreateMap<CreateStudentCommand, Student>();
            //CreateMap<CreateTeacherCommand, Professor>();
            #endregion
        }
    }
}
