namespace Application.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public int CreditsCount { get; set; }
        public ICollection<CourseDto> CoursesEnrollments { get; set; } = new List<CourseDto>();

        public StudentDto()
        {
            CoursesEnrollments = new List<CourseDto>();
        }
    }
}