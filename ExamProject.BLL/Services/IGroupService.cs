﻿using ExamProject.DataAccess;
using ExamProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.BLL.Services
{
    public interface IGroupService
    {
        PagedResult<GroupViewModel> GetAllGroups(int pageNumber, int pageSize);
        Task<GroupViewModel> AddGroupAsync(GroupViewModel groupVM);
        IEnumerable<Group> GetAllGroups();
        GroupViewModel GetById(int groupId);
    }
}
