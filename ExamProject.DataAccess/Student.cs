namespace ExamProject.DataAccess
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Contact { get; set; }
        public string CvFileName { get; set; }
        public string PictureFileName { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public ICollection<ExamResult> ExamResults { get; set; }= new HashSet<ExamResult>();
    }
}