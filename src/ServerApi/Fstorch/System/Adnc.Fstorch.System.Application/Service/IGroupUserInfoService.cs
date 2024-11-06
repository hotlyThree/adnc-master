using Adnc.Shared.Application.Contracts.Attributes;
using Adnc.Shared.Application.Contracts.Interfaces;
using Adnc.Fstorch.System.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adnc.Fstorch.System.Repository;
using Adnc.Shared.Application.Contracts.ResultModels;
using Adnc.Fstorch.System.Application.Dtos.Group;

namespace Adnc.Fstorch.System.Application.Service
{

    public interface IGroupUserInfoService : IAppService
    {

        /// <summary>
        /// 插入新的分组信息
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "插入新的分组信息")]
        Task<ReturnString> InsertGroupUserInfoAsync(GroupUserInfoDto input);

        [OperateLog(LogName = "查询分组信息")]
        Task<List<GroupUserInfo>> QueryGroupUserInfoAsync(long CompanyID, long GroupID, long UserID);

        [OperateLog(LogName = "删除分组信息")]
        Task<ReturnString> DeleteGroupUserInfoAsync(long CompanyID,long GroupID,long UserID,long ID);


        

    }
}
