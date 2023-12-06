using ExamProject.DataAccess;
using ExamProject.DataAccess.UnitOfWork;
using ExamProject.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.BLL.Services
{
    public class StudentService : IStudentService
    {
        IUnitOfWork _unitOfWork;
        ILogger<StudentService> _iLogger;
        public StudentService(IUnitOfWork unitOfWork,ILogger<StudentService> iLogger)
        {
            _unitOfWork = unitOfWork;
            _iLogger = iLogger;
                
        }
        public async Task<StudentViewModel> AddAsync(StudentViewModel vm)
        {
            try
            {
                Student obj = vm.ConvertViewModel(vm);
                await _unitOfWork.GenericRepository<Student>().AddAsync(obj);
            }
            catch (Exception ex)
            {
                return null;
            }
            return vm;
        }

        public PagedResult<StudentViewModel> GetAll(int pageNumber, int pageSize)
        {
            var model = new StudentViewModel();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                List<StudentViewModel> detailList=new List<StudentViewModel>();
                var modelList=_unitOfWork.GenericRepository<Student>().GetAll().Skip(ExcludeRecords).Take(pageSize).ToList();
                var totalCount = _unitOfWork.GenericRepository<Student>().GetAll().ToList();
                detailList = GroupListInfo(modelList);
                if (detailList != null)
                {
                    model.StudentList = detailList;
                    model.TotalCount = totalCount.Count();
                }

            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            var result = new PagedResult<StudentViewModel>
            {
                Data = model.StudentList,
                TotalItems = model.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
            return result;
        }

        private List<StudentViewModel> GroupListInfo(List<Student> modelList)
        {
            return modelList.Select(o=>new StudentViewModel(o)).ToList();
        }

        public IEnumerable<Student> GetAllStudents()
        {
            try
            {
                var students = _unitOfWork.GenericRepository<Student>().GetAll();
                return students;
            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            return Enumerable.Empty<Student>();
        }

        public IEnumerable<ResultViewModel> GetExamResults(int studentId)
        {
            try
            {
                var examResults = _unitOfWork.GenericRepository<ExamResult>().GetAll().Where(a=>a.StudentId == studentId);
                var students=_unitOfWork.GenericRepository<Student>().GetAll();
                var exams = _unitOfWork.GenericRepository<ExamResult>().GetAll();
                var qnas=_unitOfWork.GenericRepository<QnAs>().GetAll();

                var requiredData = examResults.Join(students, er => er.StudentId, s => s.Id, (er, st) => new { er, st }).Join(exams, erj => erj.er.ExamId, ex => ex.Id, (erj, ex) => new { erj, ex }).Join(qnas, exj => exj.erj.er.QnAsId, q => q.Id, (exj, q) => new ResultViewModel()
                {
                    StudentId = studentId,
                    ExamName = exj.ex.Exam.Title,
                    TotalQuestion = examResults.Count(a => a.StudentId == studentId && a.ExamId == exj.ex.Id),
                    CorrectAnswer = examResults.Count(a => a.StudentId == studentId && a.ExamId == exj.ex.Id && a.Answer == q.Answer),
                    WrongAnswer = examResults.Count(a => a.StudentId == studentId && a.ExamId == exj.ex.Id && a.Answer != q.Answer)

                }); ;
                return requiredData;

            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            return Enumerable.Empty<ResultViewModel>();
        }

        public StudentViewModel GetStudentDetails(int studentId)
        {
            try
            {
                var student = _unitOfWork.GenericRepository<Student>().GetById(studentId);
                return student != null ? new StudentViewModel(student) : null;
            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            return null;
        }

        public bool SetExamResult(AttendExamViewModel vm)
        {
            try
            {
                foreach(var item in vm.QnAs)
                {
                    ExamResult examResult=new ExamResult();
                    examResult.StudentId = vm.StudentId;
                    examResult.QnAsId = item.Id;
                    examResult.ExamId = item.ExamId;
                    examResult.Answer = item.SelectedAnswer;
                    _unitOfWork.GenericRepository<ExamResult>().Add(examResult);
                }
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            return false;
        }

        public bool SetGroupIdToStudents(GroupViewModel vm)
        {
            try
            {
                foreach(var item in vm.StudentCheckList)
                {
                    var student =_unitOfWork.GenericRepository<Student>().GetById(item.Id);
                    if(item.Selected)
                    {
                        student.GroupId = vm.Id;
                        _unitOfWork.GenericRepository<Student>().Update(student);
                    }
                    else
                    {
                        //if(student.GroupId == vm.Id)
                        //{
                        //    student.GroupId = null;
                        //}
                    }
                    _unitOfWork.Save();
                    return true;
                }
            }
            catch (Exception ex)
            {

                _iLogger.LogError(ex.Message);
            }
            return false;
        }

        public async Task<StudentViewModel> UpdateAsync(StudentViewModel vm)
        {
            try
            {
                Student obj = _unitOfWork.GenericRepository<Student>().GetById(vm.Id);
                obj.FullName=vm.FullName;
                obj.UserName=vm.UserName;
                obj.PictureFileName=vm.PictureFileName !=null?
                    vm.PictureFileName:obj.PictureFileName; 
                obj.Contact=vm.Contact;
                await _unitOfWork.GenericRepository<Student>().UpdateAsync(obj);
                _unitOfWork.Save();
            }
            catch (Exception)
            {

                throw;
            }
            return vm;
        }
    }
}
