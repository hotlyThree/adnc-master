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
    public class Bm_Service_SpecificationsService : AbstractAppService, IBm_Service_SpecificationsService
    {
        /// <summary>
        /// 初始化数据 MsgCode=BM_GROUP_INSERT_1成功,_0失败，_2找不到对象 _3条件不满足MsgValue=1成功，0失败
        /// </summary>
        private readonly IEfRepository<Bm_Service_Specifications> _Bm_Service_SpecificationsRepository;
        private readonly CacheService _cacheService;
        private readonly IUnitOfWork _unitOfWork;
        public Bm_Service_SpecificationsService(IEfRepository<Bm_Service_Specifications> bm_Service_SpecificationsRepository,
            CacheService cacheService, IUnitOfWork unitOfWork)
        {
            _Bm_Service_SpecificationsRepository = bm_Service_SpecificationsRepository;
            _cacheService = cacheService;
            _unitOfWork = unitOfWork;
            
        }

        #region 工作组信息
        public async Task<ReturnString> DeleteBmServiceSpecificationsAsync(long CompanyID,long ID)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            Bm_Service_Specifications cinfo = await _Bm_Service_SpecificationsRepository.FindAsync(x =>x.CompanyID.Equals(CompanyID) && x.Id.Equals(ID));
            if (cinfo != null)          
            {                
                //首先检测当前工作部门编码是否已使用,已使用则不能删除
                //
                _unitOfWork.BeginTransaction();
                try
                {
                    var whereExpression = ExpressionCreator
                     .New<Bm_Service_Specifications>()
                     .AndIf(CompanyID > 0, x => x.CompanyID.Equals(CompanyID))
                     .AndIf(ID>0, x => x.Id.Equals(ID));
                    int i = await _Bm_Service_SpecificationsRepository.DeleteRangeAsync(whereExpression);
                    await _unitOfWork.CommitAsync();        
                    rn.MsgValue = "1";              
                }
                catch
                {
                    await _unitOfWork.RollbackAsync();
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

        public async Task<ReturnString> InsertBmServiceSpecificationsAsync(Bm_Service_SpecificationsDto inputDto)
        {
            var input = Mapper.Map<Bm_Service_Specifications>(inputDto);
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            
            Bm_Service_Specifications cinfo = await _Bm_Service_SpecificationsRepository.FindAsync(x => x.CompanyID.Equals(input.CompanyID) &&
             x.ServiceProcessID.Equals(input.ServiceProcessID) &&
             x.ServiceSpecificationName.Equals(input.ServiceSpecificationName)==false);
            if (cinfo == null)
            {
                //查找当前编码相同的数据，找不到则新增
                _unitOfWork.BeginTransaction();
                try
                {
                    int irn = await _Bm_Service_SpecificationsRepository.InsertAsync(input);
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

        public async Task<ReturnString> ModiBmServiceSpecificationsAsync(long CompanyID, long ID, Bm_Service_SpecificationsDto input)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            Bm_Service_Specifications cinfo = await _Bm_Service_SpecificationsRepository.FindAsync(x => x.CompanyID.Equals(CompanyID) && x.Id.Equals(ID), noTracking: false);
            if (cinfo != null)
            {
                cinfo.ImageTemplate = input.ImageTemplate;
                cinfo.IsEndSpecification= input.IsEndSpecification;
                cinfo.MustCompleted= input.MustCompleted;
                cinfo.OtherOperations= input.OtherOperations;
                cinfo.ReviewObject= input.ReviewObject;
                cinfo.ReviewResult = input.ReviewResult;
                cinfo.ServiceProcessID = input.ServiceProcessID;
                cinfo.ServiceSpecificationDescribe=input.ServiceSpecificationDescribe;
                cinfo.ServiceSpecificationID= input.ServiceSpecificationID;
                cinfo.ServiceSpecificationName= input.ServiceSpecificationName;
                cinfo.SpecificationEndVerification = input.SpecificationEndVerification;
                cinfo.ToReview= input.ToReview;
                cinfo.UsingObject= input.UsingObject;
                cinfo.AttachmentTemplate= input.AttachmentTemplate;
                cinfo.CheckDuration= input.CheckDuration;
                cinfo.Displaynumber= input.Displaynumber;
                _unitOfWork.BeginTransaction();
                try
                {
                    int i = await _Bm_Service_SpecificationsRepository.UpdateAsync(cinfo);
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

      
        public async Task<List<Bm_Service_Specifications>> QueryBmServiceSpecificationsAsync(long CompanyID,  string ServiceProcessID, long ID, string ServiceSpecificationName)
        {
            //根据条件组合查询条件
            var whereExpression = ExpressionCreator
             .New<Bm_Service_Specifications>()
             .AndIf(CompanyID > 0, x => x.CompanyID.Equals(CompanyID))
             .AndIf(ServiceProcessID.IsNotNullOrEmpty(), x => x.ServiceProcessID.Equals(ServiceProcessID))
             .AndIf(ID > 0, x => x.Id.Equals(ID))
             .AndIf(ServiceSpecificationName.IsNotNullOrEmpty(),x=> x.ServiceSpecificationName.Equals(ServiceSpecificationName));

            return _Bm_Service_SpecificationsRepository.Where(whereExpression).OrderBy(x=>x.Displaynumber).ToList();
        }


        #endregion

    }
}
