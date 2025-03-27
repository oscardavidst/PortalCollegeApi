namespace Application.DTOs
{
    public class StudentCreditsRequest
    {
        public int StudentId { get; set; }
        public int CreditsCount { get; set; }
        public StudentCreditsRequest(int studentId, int creditsCount)
        {
            StudentId = studentId;
            CreditsCount = creditsCount;
        }
    }
}
