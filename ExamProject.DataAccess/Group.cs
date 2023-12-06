using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.DataAccess
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId  { get; set; }
        public User User { get; set; }
        public ICollection<Student> Students{ get; set; }= new HashSet<Student>();
        public ICollection<Exam> Exams { get; set; }= new HashSet<Exam>();
    }
}
