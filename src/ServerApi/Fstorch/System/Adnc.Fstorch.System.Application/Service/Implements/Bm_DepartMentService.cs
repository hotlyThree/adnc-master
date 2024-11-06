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
using System.Collections;
using System.Diagnostics;

namespace Adnc.Fstorch.System.Application.Service.Implements
{
    public class Bm_DepartMentService : AbstractAppService, IBm_DepartMentService
    {
        /// <summary>
        /// 初始化数据 MsgCode=BM_GROUP_INSERT_1成功,_0失败，_2找不到对象 _3条件不满足MsgValue=1成功，0失败
        /// </summary>
        private readonly IEfRepository<Bm_DepartMent> _BmDepartMentRepository;
        private readonly CacheService _cacheService;
        private readonly IUnitOfWork _unitOfWork;
        public Bm_DepartMentService(IEfRepository<Bm_DepartMent> bmDepartMentRepository,
            CacheService cacheService, IUnitOfWork unitOfWork)
        {
            _BmDepartMentRepository = bmDepartMentRepository;
            _cacheService = cacheService;
            _unitOfWork = unitOfWork;            
        }

        #region 工作组信息
        public async Task<ReturnString> DeleteBmDepartMentAsync(long CompanyID,string DepartMentName)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name; 
            Bm_DepartMent cinfo = await _BmDepartMentRepository.FindAsync(x =>x.CompanyID.Equals(CompanyID) && x.DepartMentName.Equals(DepartMentName));
            if (cinfo != null)          
            {                
                //首先检测当前工作部门编码是否已使用,已使用则不能删除
                //
                _unitOfWork.BeginTransaction();
                try
                {
                    var whereExpression = ExpressionCreator
                     .New<Bm_DepartMent>()
                     .AndIf(CompanyID > 0, x => x.CompanyID.Equals(CompanyID))
                     .AndIf(DepartMentName!="", x => x.DepartMentName.Equals(DepartMentName));
                    int i = await _BmDepartMentRepository.DeleteRangeAsync(whereExpression);
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

        public async Task<ReturnString> InsertBmDepartMentAsync(Bm_DepartMentDto inputDto)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = "InsertBmDepartMentAsync";
            rn.MsgDescribe = await _cacheService.FindLanguageFromCacheAsync("B", rn.MsgCode, "");

            var input = Mapper.Map<Bm_DepartMent>(inputDto);      
            Bm_DepartMent cinfo = await _BmDepartMentRepository.FindAsync(x => x.CompanyID.Equals(input.CompanyID) && 
             x.DepartMentName.Equals(input.DepartMentName) );
            if (cinfo == null)
            {
                //查找当前编码相同的数据，找不到则新增
                _unitOfWork.BeginTransaction();
                try
                {
                    
                    int irn = await _BmDepartMentRepository.InsertAsync(input);
                    if (irn >= 0)
                    {
                        rn.MsgValue = "1";
                        await _unitOfWork.CommitAsync();
                        rn.MsgDescribe = await _cacheService.FindLanguageFromCacheAsync("B", rn.MsgCode, "01");
                        

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

        public async Task<ReturnString> ModiBmDepartMentAsync(long CompanyID, string DepartMentName, Bm_DepartMentDto input)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            Bm_DepartMent cinfo = await _BmDepartMentRepository.FindAsync(x => x.CompanyID.Equals(CompanyID) && x.DepartMentName.Equals(DepartMentName), noTracking: false);
            if (cinfo != null)
            {
                if (cinfo.DepartMentName != input.DepartMentName)
                {
                    //检测工作组名称相同企业是否重复
                    int n= await _BmDepartMentRepository.CountAsync(x => x.CompanyID.Equals(CompanyID)  && x.DepartMentName.Equals(input.DepartMentName));
                    if (n > 0)
                    {
                        //存在重复名称,不能更新
                        rn.MsgValue = "3";
                        return rn;
                    }
                    cinfo.DepartMentName = input.DepartMentName;//可以更新名称
                }
                //复制
                cinfo.IsValid = input.IsValid;                
                cinfo.Displaynumber = input.Displaynumber;
                _unitOfWork.BeginTransaction();
                try
                {
                    int i = await _BmDepartMentRepository.UpdateAsync(cinfo);
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

        public async Task<List<Bm_DepartMent>> QueryBmDepartMentAsync(long CompanyID,  string DepartMentName, string IsValid)
        {
            //根据条件组合查询条件
            var whereExpression = ExpressionCreator
             .New<Bm_DepartMent>()
             .AndIf(CompanyID > 0, x => x.CompanyID.Equals(CompanyID))
             .AndIf(DepartMentName.IsNotNullOrEmpty(), x => x.DepartMentName.Equals(DepartMentName))
             .AndIf(IsValid.IsNotNullOrEmpty(),x=> x.IsValid.Equals(IsValid));

            return _BmDepartMentRepository.Where(whereExpression).OrderBy(x=>x.Displaynumber).ToList();
        }


        #endregion

    }
}
