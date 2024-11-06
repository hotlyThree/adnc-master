using Adnc.Shared.Rpc.Http;

namespace Adnc.Demo.Shared.Rpc.Http.Services
{
    public interface IStaffRestClient: IRestClient
    {
        /// <summary>
        /// 获取人事信息
        /// </summary>
        /// <param name="CompanyID">公司ID</param>
        /// <param name="ID">人事ID</param>
        /// <returns></returns>
        [Post("/System/User/QueryDaStaffIDAsync")]
        Task<ApiResponse<List<Da_StaffRto>>> QueryDaStaffIDAsync(long CompanyID, long ID);


        /// <summary>
        /// 获取人事信息（缓存）
        /// </summary>
        /// <param name="CompanyID">接口名称</param>
        /// <returns></returns>
        [Post("/System/User/GetCodeBmStaffListAsync")]
        Task<ApiResponse<List<Da_StaffListDto>>> GetCodeBmStaffListAsync(long CompanyID);
    }
}
