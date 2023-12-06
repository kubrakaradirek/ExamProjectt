namespace ExamProject.DataAccess
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public ICollection<Group> Groups { get; set; }= new HashSet<Group>();

    }
}