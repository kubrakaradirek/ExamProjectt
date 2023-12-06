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
    public class AccountService : IAccountService
    {
        IUnitOfWork _unitOfWork;
        ILogger<StudentService> _iLogger;
        public AccountService(IUnitOfWork unitOfWork,ILogger<StudentService> iLogger)
        {
                _unitOfWork = unitOfWork;
            _iLogger = iLogger;
        }
        public bool AddTeacher(UserViewModel vm)
        {
            try
            {
                User obj = new User()
                {
                    FullName = vm.FullName,
                    UserName = vm.UserName,
                    Password = vm.Password,
                    Role = (int)Roles.Teacher
                };
                _unitOfWork.GenericRepository<User>().AddAsync(obj);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message);
                return false;
            }
            return true;
        }

        public PagedResult<UserViewModel> GetAllTeachers(int pageNumber, int pageSize)
        {
            var model = new UserViewModel();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                List<UserViewModel> detailList=new List<UserViewModel>();
                var modelList=_unitOfWork.GenericRepository<User>().GetAll().Where(x=>x.Role==(int)Roles.Teacher).Skip(ExcludeRecords).Take(pageSize).ToList();
                detailList = ListInfo(modelList);
                if(detailList !=null)
                {
                    model.UserList=detailList;
                    model.TotalCount=_unitOfWork.GenericRepository<User>().GetAll().Count(x=>x.Role==(int)Roles.Teacher);
                }
            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            var results = new PagedResult<UserViewModel>
            {
                Data = model.UserList,
                TotalItems = model.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return results;
        }

        private List<UserViewModel> ListInfo(List<User> modelList)
        {
            return modelList.Select(o => new UserViewModel(o)).ToList();
        }

        public LoginViewModel Login(LoginViewModel vm)
        {
            if (vm.Role==(int)Roles.Admin||vm.Role==(int)Roles.Teacher)
            {
                var user=_unitOfWork.GenericRepository<User>().GetAll().FirstOrDefault(a=>a.UserName==vm.UserName.Trim() && a.Password==vm.Password.Trim() && a.Role==vm.Role);
                if(user !=null)
                {
                    vm.Id = user.Id;
                    return vm;
                }
            }
            else
            {
                var student=_unitOfWork.GenericRepository<Student>().GetAll().FirstOrDefault(a=>a.UserName==vm.UserName.Trim()&&a.Password==vm.Password.Trim());
                if(student!=null)
                {
                    vm.Id = student.Id;
                }
                return vm;
            }
            return null;
        }
    }
}
