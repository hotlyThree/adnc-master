using Adnc.Shared.Rpc.Http;

namespace Adnc.Demo.Shared.Rpc.Http.Services
{
    public interface ISystemRestClient : IRestClient
    {
        /// <summary>
        /// 获取提示信息
        /// </summary>
        /// <returns></returns>
        [Post("/System/Other/FindLanguageAsync")]
        Task<ApiResponse<string>> FindLanguageAsync(string TableName, string TranslateName);

        /// <summary>
        /// 获取某公司的所有服务流程
        /// </summary>
        /// <param name="CompanyID">公司ID</param>
        /// <returns></returns>
        [Post("/System/CodeBase/GetCodeBmServiceProcessAsync")]
        Task<ApiResponse<List<Bm_Service_ProcessRto>>> GetCodeBmServiceProcessAsync(long CompanyID);



        /// <summary>
        /// 获取某公司所有服务规范
        /// </summary>
        /// <param name="CompanyID">公司ID</param>
        /// <returns></returns>
        [Post("/System/CodeBase/GetCodeBmServiceSpecificationsAsync")]
        Task<ApiResponse<List<Bm_Service_SpecificationsRto>>> GetCodeBmServiceSpecificationsAsync(long CompanyID);

        /// <summary>
        /// 获取某公司所有服务流程关系
        /// </summary>
        /// <param name="CompanyID">公司ID</param>
        /// <returns></returns>
        [Post("/System/CodeBase/GetCodeBmRelationComparisonAsync")]
        [Headers("Authorization: Bearer", "Cache: 2000")]
        Task<ApiResponse<List<Bm_Relation_ComparisonRto>>> GetCodeBmRelationComparisonAsync(long CompanyID);


        /// <summary>
        /// 获取某公司所有工作部门
        /// </summary>
        /// <param name="CompanyID">公司ID</param>
        /// <returns></returns>
        [Post("/System/CodeBase/GetCodeBmDepartMentAsync")]
        Task<ApiResponse<List<Bm_DepartMentRto>>> GetCodeBmDepartMentAsync(long CompanyID);

        /// <summary>
        /// 获取某公司所有工作职务
        /// </summary>
        /// <param name="CompanyID">公司ID</param>
        /// <returns></returns>
        [Post("/System/CodeBase/GetCodeBmPostMentAsync")]
        Task<ApiResponse<List<Bm_PostRto>>> GetCodeBmPostMentAsync(long CompanyID);

        /// <summary>
        /// 返回服务器配置信息，服务器地址和产品ID （服务器地址|产品ID）
        /// </summary>
        /// <returns></returns>
        [Post("/System/Language/GetServerInfo")]
        Task<ApiResponse<string>> GetServerInfo();

        /// <summary>
        /// 获取某公司所有人事档案编码
        /// </summary>
        /// <param name="CompanyID">公司ID</param>
        /// <returns></returns>
        [Post("/System/User/GetCodeBmStaffListAsync")]
        Task<ApiResponse<List<Da_StaffListDto>>> GetCodeBmStaffListAsync(long CompanyID);

    }
}
