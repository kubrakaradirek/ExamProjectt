using ExamProject.DataAccess;
using ExamProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.BLL.Services
{
    public interface IExamService
    {
        PagedResult<ExamViewModel> GetAll(int pageNumber,int pageSize);
        Task<ExamViewModel> AddAsync(ExamViewModel examVM);
        IEnumerable<Exam> GetAllExams();
    }
}
