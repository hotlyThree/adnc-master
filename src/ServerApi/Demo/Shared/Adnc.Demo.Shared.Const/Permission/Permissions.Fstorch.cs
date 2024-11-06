namespace Adnc.Demo.Shared.Const.Permissions.Fstorch;

public static class PermissionConsts
{

    public static class Server
    {
        public const string CodeSet = "ServerCodeSet";
        public const string CodeQuery = "ServerCodeQuery";

    }

    public static class Public
    {
        /// <summary>
        /// 服务器信息查询权限
        /// </summary>
        public const string ServerQuery = "PublicServerQuery";
        /// <summary>
        /// 数据查询权限
        /// </summary>
        public const string DataQuery = "PublicDataQuery";
        /// <summary>
        /// 数据设置权限
        /// </summary>
        public const string DataSet = "PublicDataSet";
        /// <summary>
        /// 文件设置权限
        /// </summary>
        public const string FileSet = "PublicFileSet";
        /// <summary>
        /// 文件查询权限
        /// </summary>
        public const string FileQuery = "PublicFileQuery";
    }

    /// <summary>
    /// 系统设置相关的接口权限
    /// </summary>
    public static class System
    {
        
        /// <summary>
        /// 编码设置权限
        /// </summary>
        public const string CodeSet = "SystemCodeSet";
        /// <summary>
        /// 编码查询权限
        /// </summary>
        public const string CodeQuery = "SystemCodeQuery";
        /// <summary>
        /// 工作组设置权限
        /// </summary>
        public const string GroupSet = "SystemGroupSet";
        /// <summary>
        /// 工作组查询权限
        /// </summary>
        public const string GroupQuery = "SystemGroupQuery";
        /// <summary>
        /// 公司编码设置权限
        /// </summary>
        public const string CompanySet = "SystemCompanySet";
        /// <summary>
        /// 公司编码查询权限
        /// </summary>
        public const string CompanyQuery = "SystemCompanyQuery";
    }

    /// <summary>
    /// 财务设置相关接口权限
    /// </summary>
    public static class Financ
    {
        /// <summary>
        /// 编码设置权限（编码的增、删、改）
        /// </summary>
        public const string CodeSet = "FinancCodeSet";
        /// <summary>
        /// 编码查询权限（编码的查询和使用）
        /// </summary>
        public const string CodeQuery = "FinancCodeQuery";
        /// <summary>
        /// 数据设置权限（信息的增、删、改）
        /// </summary>
        public const string DataSet = "FinancDataSet";
        /// <summary>
        /// 数据查询权限（信息的查询）
        /// </summary>
        public const string DataQuery = "FinancDataQuery";

    }

    /// <summary>
    /// 服务设置相关接口权限
    /// </summary>
    public static class AfterService
    {
        /// <summary>
        /// 编码设置权限（编码的增、删、改）
        /// </summary>
        public const string CodeSet = "ServiceCodeSet";
        /// <summary>
        /// 编码查询权限（编码的查询和使用）
        /// </summary>
        public const string CodeQuery = "ServiceCodeQuery";
        /// <summary>
        /// 数据设置权限（信息的增、删、改）
        /// </summary>
        public const string DataSet = "ServiceDataSet";
        /// <summary>
        /// 数据查询权限（信息的查询）
        /// </summary>
        public const string DataQuery = "ServiceDataQuery";

    }
}