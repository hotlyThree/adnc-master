namespace Adnc.Fstorch.User.Application.Services
{
    public interface ICompanyAppService : IAppService
    {
        /// <summary>
        /// 新增公司
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [CachingEvict(CacheKey = CachingConsts.CompanyListCacheKey)]
        Task<AppSrvResult<long>> CreateAsync(CompanyCreation input);

        /// <summary>
        /// 更新公司信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [CachingEvict(CacheKey = CachingConsts.CompanyListCacheKey)]
        Task<AppSrvResult> UpdateAsync(long id, CompanyUpdation input);

        /// <summary>
        /// 删除公司
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CachingEvict(CacheKey = CachingConsts.CompanyListCacheKey)]
        Task<AppSrvResult> DeleteAsync(long id);


        /// <summary>
        /// 获取当前人员所在公司列表
        /// </summary>
        /// <param name="insider"></param>
        /// <returns></returns>
        Task<AppSrvResult<List<CompanyDto>>> GetListAsync(string insider);

        /// <summary>
        /// 根据名称模糊查询获取公司列表
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<object> GetListByNameAsync(long aid,string name);

        /// <summary>
        /// 申请待处理列表
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        Task<object> GetWaitingTask(long aid);

        /// <summary>
        /// 申请加入公司
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="cid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKey = CachingConsts.CompanyListCacheKey)]
        Task<AppSrvResult> ApplyCompany(long aid, long cid, long uid);

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="type"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKey = CachingConsts.CompanyListCacheKey)]
        Task<AppSrvResult> Review(long aid, string type, long cid);

        /// <summary>
        /// 移除指定人员
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKey = CachingConsts.CompanyListCacheKey)]
        Task<AppSrvResult> Remove(long aid, long cid);

        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppSrvResult<CompanyDto>> GetInfoAsync(long id);

        /// <summary>
        /// 查询公司内部人员列表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        Task<AppSrvResult<List<AccountDto>>> GetAccountListAsync(long cid);


        /*Task<AppSrvResult<string[]>> ConvertPdfToImages(IFormFile pdfFile);*/
    }
}
