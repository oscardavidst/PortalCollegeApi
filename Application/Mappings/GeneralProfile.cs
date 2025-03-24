using Application.DTOs;
using Application.Features.Courses.Commands.CreateCourseCommand;
using Application.Features.Professors.Commands.CreateProfessorCommand;
using Application.Features.Students.Commands.CreateStudentCommand;
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
            CreateMap<CreateStudentCommand, Student>();
            CreateMap<CreateProfessorCommand, Professor>();
            CreateMap<CreateCourseCommand, Course>();
            #endregion
        }
    }
}
