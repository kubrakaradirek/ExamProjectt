﻿using ExamProject.DataAccess;
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
    public class QnAService : IQnAService
    {
        IUnitOfWork _unitOfWork;
        ILogger<IQnAService> _iLogger;
        public QnAService(IUnitOfWork unitOfWork,ILogger<IQnAService> iLogger)
        {
                _unitOfWork = unitOfWork;
            _iLogger = iLogger;
        }

        public async Task<QnAsViewModel> AddAsync(QnAsViewModel QnAVM)
        {
            try
            {
                QnAs objGroup = QnAVM.ConvertViewModel(QnAVM);
                await _unitOfWork.GenericRepository<QnAs>().AddAsync(objGroup);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return null;
            }
            return QnAVM;
        }

        public PagedResult<QnAsViewModel> GetAll(int pageNumber, int pageSize)
        {
            var model = new QnAsViewModel();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                List<QnAsViewModel> detailList = new List<QnAsViewModel>();
                var modelList = _unitOfWork.GenericRepository<QnAs>().GetAll().Skip(ExcludeRecords).Take(pageSize).ToList();
                var totalCount = _unitOfWork.GenericRepository<QnAs>().GetAll().ToList();
                detailList = ListInfo(modelList);
                if (detailList != null)
                {
                    model.QnAsList = detailList;
                    model.TotalCount = totalCount.Count();

                }
            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            var result = new PagedResult<QnAsViewModel>
            {
                Data = model.QnAsList,
                TotalItems = model.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<QnAsViewModel> ListInfo(List<QnAs> modelList)
        {
            return modelList.Select(o => new QnAsViewModel(o)).ToList();
        }

        public IEnumerable<QnAsViewModel> GetAllQnAByExam(int examId)
        {
            try
            {
                var qnaList = _unitOfWork.GenericRepository<QnAs>().GetAll().Where(x=>x.ExamId==examId);
                return ListInfo(qnaList.ToList());
            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            return Enumerable.Empty<QnAsViewModel>();
        }

        public bool IsExamAttended(int examId, int studentId)
        {
            try
            {
                var qnaRecord = _unitOfWork.GenericRepository<ExamResult>().GetAll().FirstOrDefault(x => x.ExamId == examId && x.StudentId == studentId);
                return qnaRecord == null ? false : true;
            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            return false;
        }
    }
}
