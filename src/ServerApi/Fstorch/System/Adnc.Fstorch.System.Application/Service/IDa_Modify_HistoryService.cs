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
using Adnc.Fstorch.System.Application.Dtos.CodeBase;
using Adnc.Fstorch.System.Application.Dtos.Other;

namespace Adnc.Fstorch.System.Application.Service
{

    public interface IDa_Modify_HistoryService : IAppService
    {

        /// <summary>
        /// 插入修改历史数据
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "插入修改历史数据")]
        Task<ReturnString> InsertDaModifyHistoryAsync(Da_Modify_HistoryDto input);
       
        [OperateLog(LogName = "查询修改历史数据")]
        Task<List<Da_Modify_History>> QueryDaModifyHistoryAsync(long CompanyID, string TableName, long InfoID);

        [OperateLog(LogName = "删除修改历史数据")]
        Task<ReturnString> DeleteDaModifyHistoryAsync(long CompanyID, long ID);


        

    }
}
