using Adnc.Infra.Entities;
using Adnc.Shared.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.System.Application.Dtos.Group
{
    /// <summary>
    /// 用户工作组设置
    /// </summary>
    public class GroupInfoDto : InputDto
    {
        /// <summary> 
        /// 工作组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary> 
        /// 工作组描述
        /// </summary>
        public string GroupDescribe { get; set; }

        /// <summary> 
        /// 只查自己的单据
        /// </summary>
        public string OnlyOneSelf { get; set; }

        /// <summary> 
        /// 只查自己经销商的单据
        /// </summary>
        public string OnlyOneAgent { get; set; }

        /// <summary> 
        /// 只查自己分公司的单据
        /// </summary>
        public string OnlyOnebranch { get; set; }

        /// <summary> 
        /// 只查自己服务商的单据
        /// </summary>
        public string OnlyOneProvider { get; set; }

        /// <summary> 
        /// 只查自己门店的单据
        /// </summary>
        public string OnlyOneStore { get; set; }

        /// <summary> 
        /// 所属公司
        /// </summary>
        public long CompanyID { get; set; }

        /// <summary> 
        /// 商家ID
        /// </summary>
        public string ProviderID { get; set; }

        /// <summary> 
        /// APP角色
        /// </summary>
        public string AppRole { get; set; }
    }
    /// <summary>
    /// 工作组用户设置
    /// </summary>
    public class GroupUserInfoDto : InputDto
    {
        /// <summary>
        /// 工作组ID
        /// </summary>
        public long GroupID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }
        
        /// <summary>
        /// 所属公司
        /// </summary>
        public long CompanyID { get; set; }
    }
    /// <summary>
    /// 工作组菜单设置
    /// </summary>
    public class GroupMenuInfoDto : InputDto
    {
        /// <summary>
        /// 工作组ID
        /// </summary>
        public long GroupID { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public long MenuID { get; set; }
        /// <summary>
        /// 子权限字符串(保存:1|打印:0)
        /// </summary>
        public string? SubPermissions { get; set; }
        /// <summary>
        /// 所属公司
        /// </summary>
        public long CompanyID { get; set; }
    }
    /// <summary>
    /// 工作组仓库设置
    /// </summary>
    public class GroupWareHouseInfoDto : InputDto
    {
        /// <summary>
        /// 工作组ID
        /// </summary>
        public long GroupID { get; set; }
        /// <summary>
        /// 仓库ID
        /// </summary>
        public long WarehouseCode { get; set; }
        /// <summary>
        /// 查询权限
        /// </summary>
        public string QueryPermissions { get; set; }
        /// <summary>
        /// 出库权限
        /// </summary>
        public string OutboundPermission {get;set;}
        /// <summary>
        /// 入库权限
        /// </summary>
        public string EntryPermission {get;set;}
        /// <summary>
        /// 所属公司
        /// </summary>
        public long CompanyID { get; set; }
    }
}
