using ExamProject.DataAccess;
using ExamProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.BLL.Services
{
    public interface IStudentService
    {
        PagedResult<StudentViewModel> GetAll(int pageNumber,int pageSize);  
        Task<StudentViewModel> AddAsync(StudentViewModel vm);
        IEnumerable<Student> GetAllStudents();
        bool SetGroupIdToStudents(GroupViewModel vm);
        bool SetExamResult(AttendExamViewModel vm);
        IEnumerable<ResultViewModel> GetExamResults(int studentId);
        StudentViewModel GetStudentDetails(int studentId);  
        Task<StudentViewModel> UpdateAsync(StudentViewModel vm);
    }
}
