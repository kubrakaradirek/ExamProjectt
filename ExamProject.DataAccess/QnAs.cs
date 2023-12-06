namespace ExamProject.DataAccess
{
    public class QnAs
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public string Question { get; set; }
        public int Answer { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public ICollection<ExamResult> ExamResults { get; set; } = new HashSet<ExamResult>();

    }
}