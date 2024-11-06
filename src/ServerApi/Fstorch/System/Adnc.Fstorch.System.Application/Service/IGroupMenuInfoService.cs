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

    public interface IGroupMenuInfoService : IAppService
    {

        /// <summary>
        /// 插入新的菜单信息
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "插入新的菜单信息")]
        Task<ReturnString> InsertGroupMenuInfoAsync(GroupMenuInfoDto input);

        [OperateLog(LogName = "查询菜单信息")]
        Task<List<GroupMenuInfo>> QueryGroupMenuInfoAsync(long CompanyID,  long GroupID, long MenuID);

        [OperateLog(LogName = "删除菜单信息")]
        Task<ReturnString> DeleteGroupMenuInfoAsync(long CompanyID, long GroupID,long MenuID, long ID);


        

    }
}
