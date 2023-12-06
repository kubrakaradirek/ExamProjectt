namespace ExamProject.DataAccess
{
    public class Exam
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Time { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public ICollection<ExamResult> ExamResults { get; set; }= new HashSet<ExamResult>();
        public ICollection<QnAs> QnAs { get; set; }= new HashSet<QnAs>();
    }
}