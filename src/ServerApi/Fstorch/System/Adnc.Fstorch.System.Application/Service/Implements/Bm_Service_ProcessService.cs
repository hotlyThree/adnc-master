using Adnc.Fstorch.System.Repository.Entities;
using Adnc.Infra.IRepositories;
using Adnc.Shared.Application.Services;
using Adnc.Fstorch.System.Application.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adnc.Infra.IdGenerater.Yitter;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Adnc.Fstorch.System.Repository;
using Adnc.Shared.Application.Contracts.ResultModels;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.ComponentModel.Design;
using Adnc.Fstorch.System.Application.Dtos.Group;
using Adnc.Fstorch.System.Application.Dtos.CodeBase;
using System.Reflection;

namespace Adnc.Fstorch.System.Application.Service.Implements
{
    public class Bm_Service_ProcessService : AbstractAppService, IBm_Service_ProcessService
    {
        /// <summary>
        /// 初始化数据 MsgCode=BM_GROUP_INSERT_1成功,_0失败，_2找不到对象 _3条件不满足MsgValue=1成功，0失败
        /// </summary>
        private readonly IEfRepository<Bm_Service_Process> _Bm_Service_ProcessRepository;
        private readonly CacheService _cacheService;
        private readonly IUnitOfWork _unitOfWork;
        public Bm_Service_ProcessService(IEfRepository<Bm_Service_Process> bm_Service_ProcessRepository,
            CacheService cacheService, IUnitOfWork unitOfWork)
        {
            _Bm_Service_ProcessRepository = bm_Service_ProcessRepository;
            _cacheService = cacheService;
            _unitOfWork = unitOfWork;
            
        }

        #region 工作组信息
        public async Task<ReturnString> DeleteBmServiceProcessAsync(long CompanyID,string ServiceProcessID)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            Bm_Service_Process cinfo = await _Bm_Service_ProcessRepository.FindAsync(x =>x.CompanyID.Equals(CompanyID) &&
            x.ServiceProcessID.Equals(ServiceProcessID));
            if (cinfo != null)          
            {                
                //首先检测当前工作部门编码是否已使用,已使用则不能删除
                //
                _unitOfWork.BeginTransaction();
                try
                {
                    var whereExpression = ExpressionCreator
                     .New<Bm_Service_Process>()
                     .AndIf(CompanyID > 0, x => x.CompanyID.Equals(CompanyID))
                     .AndIf(ServiceProcessID.IsNotNullOrEmpty(), x => x.ServiceProcessID.Equals(ServiceProcessID));
                    int i = await _Bm_Service_ProcessRepository.DeleteRangeAsync(whereExpression);
                    _unitOfWork.Commit();          
                    rn.MsgValue = "1";              
                }
                catch
                {
                    _unitOfWork.Rollback();
                    rn.MsgValue = "0";
                }                
            }
            else
            {
                rn.MsgValue = "2";
            }
            //返回当前Msgcode的缓存值
            return rn;
        }

        public async Task<ReturnString> InsertBmServiceProcessAsync(Bm_Service_ProcessDto inputDto)
        {
            var input = Mapper.Map<Bm_Service_Process>(inputDto);
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
           
            Bm_Service_Process cinfo = await _Bm_Service_ProcessRepository.FindAsync(x => x.CompanyID.Equals(input.CompanyID) && 
             x.ServiceProcessID.Equals(input.ServiceProcessID) );
            if (cinfo == null)
            {
                //查找当前编码相同的数据，找不到则新增
                _unitOfWork.BeginTransaction();
                try
                {
                    int irn = await _Bm_Service_ProcessRepository.InsertAsync(input);
                    if (irn >= 0)
                    {
                        rn.MsgValue = "1";
                        await _unitOfWork.CommitAsync();
                    }
                    else
                    {
                        rn.MsgValue = "0";
                        await _unitOfWork.RollbackAsync();
                    }
                }
                catch
                {
                    rn.MsgValue = "0";
                    await _unitOfWork.RollbackAsync();
                }
            }
            else
            {
                rn.MsgValue = "2";
            }
            return rn; 
        }

        public async Task<ReturnString> ModiBmServiceProcessAsync(long CompanyID, string ServiceProcessID, Bm_Service_ProcessDto input)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            Bm_Service_Process cinfo = await _Bm_Service_ProcessRepository.FindAsync(x => x.CompanyID.Equals(CompanyID) &&
                x.ServiceProcessID.Equals(ServiceProcessID), noTracking: false);
            if (cinfo != null)
            {

                //复制
                cinfo.ServiceProcessName = input.ServiceProcessName;
                cinfo.IsEndProcess = input.IsEndProcess;
                cinfo.ProcessEndVerification = input.ProcessEndVerification;
                cinfo.OrderVerification = input.OrderVerification;
                cinfo.AttachmentTemplate = input.AttachmentTemplate;
                cinfo.ImageTemplate = input.ImageTemplate;
                cinfo.MustCompleted = input.MustCompleted;  
                cinfo.OtherOperations= input.OtherOperations;
                cinfo.ReviewObject= input.ReviewObject;
                cinfo.ReviewResult= input.ReviewResult;
                cinfo.ToReview= input.ToReview;
                cinfo.UsingObject= input.UsingObject;
                
                _unitOfWork.BeginTransaction();
                try
                {
                    int i = await _Bm_Service_ProcessRepository.UpdateAsync(cinfo);
                    if (i > 0)
                    {
                        rn.MsgValue = "1";
                        await _unitOfWork.CommitAsync();
                    }
                    else
                    {
                        rn.MsgValue = "0";
                        await _unitOfWork.RollbackAsync();
                    }
                }
                catch {
                    rn.MsgValue = "0";
                    await _unitOfWork.RollbackAsync();
                }
            }
            else
            {
                rn.MsgValue = "2";
            }
            return rn;
        }

      
        public async Task<List<Bm_Service_Process>> QueryBmServiceProcessAsync(long CompanyID,  string ServiceProcessID)
        {
            //根据条件组合查询条件
            var whereExpression = ExpressionCreator
             .New<Bm_Service_Process>()
             .AndIf(CompanyID > 0, x => x.CompanyID.Equals(CompanyID))
             .AndIf(ServiceProcessID.IsNotNullOrEmpty(), x => x.ServiceProcessID.Equals(ServiceProcessID));

            return _Bm_Service_ProcessRepository.Where(whereExpression).OrderBy(x=>x.ServiceProcessID).ToList();
        }


        #endregion

    }
}
