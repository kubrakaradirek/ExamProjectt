using ExamProject.DataAccess;
using ExamProject.DataAccess.UnitOfWork;
using ExamProject.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.BLL.Services
{
    public class GroupService : IGroupService
    {
        IUnitOfWork _unitOfWork;
        ILogger<GroupService> _iLogger;
        public GroupService(IUnitOfWork unitOfWork,ILogger<GroupService> iLogger)
        {
                _unitOfWork = unitOfWork;
            _iLogger = iLogger;
        }
        public async Task<GroupViewModel> AddGroupAsync(GroupViewModel groupVM)
        {
            try
            {
                Group objGroup = groupVM.ConvertGroupViewModel(groupVM);
                await _unitOfWork.GenericRepository<Group>().AddAsync(objGroup);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return null;
            }
            return groupVM;
        }

        public PagedResult<GroupViewModel> GetAllGroups(int pageNumber, int pageSize)
        {
            var model = new GroupViewModel();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                List<GroupViewModel> detailList=new List<GroupViewModel>();
                var modelList=_unitOfWork.GenericRepository<Group>().GetAll().Skip(ExcludeRecords).Take(pageSize).ToList();
                var totalCount = _unitOfWork.GenericRepository<Group>().GetAll().ToList();
                detailList = GroupListInfo(modelList);
                if (detailList != null)
                {
                    model.GroupList = detailList;
                    model.TotalCount = totalCount.Count();

                }
            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            var result = new PagedResult<GroupViewModel>
            {
                Data = model.GroupList,
                TotalItems = model.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<GroupViewModel> GroupListInfo(List<Group> modelList)
        {
            return modelList.Select(o => new GroupViewModel(o)).ToList();
        }

        public IEnumerable<Group> GetAllGroups()
        {
            try
            {
                var groups = _unitOfWork.GenericRepository<Group>().GetAll();
                return groups;
            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            return Enumerable.Empty<Group>();
        }

        public GroupViewModel GetById(int groupId)
        {
            try
            {
                var group = _unitOfWork.GenericRepository<Group>().GetById(groupId);
                return new GroupViewModel(group);
            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            return null;
        }

        IEnumerable<DataAccess.Group> IGroupService.GetAllGroups()
        {
            throw new NotImplementedException();
        }
    }
}
