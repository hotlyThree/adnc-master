﻿using Adnc.Fstorch.System.Repository.Entities;
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
    public class Bm_Public_BaseSerevice : AbstractAppService, IBm_Public_BaseSerevice
    {
        /// <summary>
        /// 初始化数据 MsgCode=BM_GROUP_INSERT_1成功,_0失败，_2找不到对象 _3条件不满足MsgValue=1成功，0失败
        /// </summary>
        private readonly IEfRepository<Bm_Public_Base> _Bm_Public_BaseRepository;
        private readonly CacheService _cacheService;
        private readonly IUnitOfWork _unitOfWork;
        public Bm_Public_BaseSerevice(IEfRepository<Bm_Public_Base> bm_Public_BaseRepository,
            CacheService cacheService, IUnitOfWork unitOfWork)
        {
            _Bm_Public_BaseRepository = bm_Public_BaseRepository;
            _cacheService = cacheService;
            _unitOfWork = unitOfWork;
            
        }

        #region 工作组信息
        public async Task<ReturnString> DeleteBmPublicBaseAsync(long CompanyID,string ModuleID)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            Bm_Public_Base cinfo = await _Bm_Public_BaseRepository.FindAsync(x =>x.CompanyID.Equals(CompanyID) && x.ModuleID.Equals(ModuleID));
            if (cinfo != null)          
            {                
                //首先检测当前工作部门编码是否已使用,已使用则不能删除
                //
                _unitOfWork.BeginTransaction();
                try
                {
                    var whereExpression = ExpressionCreator
                     .New<Bm_Public_Base>()
                     .AndIf(CompanyID > 0, x => x.CompanyID.Equals(CompanyID))
                     .AndIf(ModuleID.IsNotNullOrEmpty(), x => x.ModuleID.Equals(ModuleID));
                    int i = await _Bm_Public_BaseRepository.DeleteRangeAsync(whereExpression);
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

        public async Task<ReturnString> InsertBmPublicBaseAsync(Bm_Public_BaseDto inputDto)
        {
            var input = Mapper.Map<Bm_Public_Base>(inputDto);
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;

            Bm_Public_Base cinfo = await _Bm_Public_BaseRepository.FindAsync(x => x.CompanyID.Equals(input.CompanyID) && 
             x.ModuleID.Equals(input.ModuleID) );
            if (cinfo == null)
            {
                //查找当前编码相同的数据，找不到则新增
                _unitOfWork.BeginTransaction();
                try
                {
                    int irn = await _Bm_Public_BaseRepository.InsertAsync(input);
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

        public async Task<ReturnString> ModiBmPublicBaseAsync(long CompanyID, string ModuleID, Bm_Public_BaseDto input)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            Bm_Public_Base cinfo = await _Bm_Public_BaseRepository.FindAsync(x => x.CompanyID.Equals(CompanyID) &&
                x.ModuleID.Equals(ModuleID), noTracking: false);
            if (cinfo != null)
            {                
                cinfo.ModuleDescribe = input.ModuleDescribe;                
                cinfo.ModuleName = input.ModuleName;
                _unitOfWork.BeginTransaction();
                try
                {
                    int i = await _Bm_Public_BaseRepository.UpdateAsync(cinfo);
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

      
        public async Task<List<Bm_Public_Base>> QueryBmPublicBaseAsync(long CompanyID, string ModuleID, string ModuleName)
        {
            //根据条件组合查询条件
            var whereExpression = ExpressionCreator
             .New<Bm_Public_Base>()
             .AndIf(CompanyID > 0, x => x.CompanyID.Equals(CompanyID))
             .AndIf(ModuleID.IsNotNullOrEmpty(), x => x.ModuleID.Equals(ModuleID))
             .AndIf(ModuleName.IsNotNullOrEmpty(),x=> x.ModuleName.Equals(ModuleName));

            return _Bm_Public_BaseRepository.Where(whereExpression).OrderBy(x=>x.ModuleID).ToList();
        }


        #endregion

    }
}