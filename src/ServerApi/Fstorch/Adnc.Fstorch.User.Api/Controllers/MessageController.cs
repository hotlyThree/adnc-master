using Adnc.Fstorch.User.Application.Dtos.Message;
using Adnc.Fstorch.User.Application.Dtos.ReadDetail;

namespace Adnc.Fstorch.User.Api.Controllers
{
    /// <summary>
    /// 文章管理
    /// </summary>
    [ApiController]
    [Route($"{RouteConsts.MsgRoot}")]
    public class MessageController : AdncControllerBase
    {
        public readonly IMessageAppService _messageAppService;

        public MessageController( IMessageAppService messageAppService)
        {
            _messageAppService = messageAppService;
        }

        /// <summary>
        /// 创建文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("ins")]
        public async Task<ActionResult<long>> CreateAsync([FromBody]MessageCreationDto input)
            => CreatedResult(await _messageAppService.CreateAsync(input));


        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id">文章ID</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpDelete("del/{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute]long id)
            => Result(await  _messageAppService.DeleteAsync(id));

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="id">文章ID</param>
        /// <param name="input">更新内容</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut("upd/{id}")]
        public async Task<ActionResult> UpdateAsync([FromRoute]long id, [FromBody]MessageUpdationDto input)
            => Result(await _messageAppService.UpdateAsync(id, input));

        /// <summary>
        /// 更新文章缩略图
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut("change/path")]
        public async Task<ActionResult> ChangeThumimg([FromBody]MessageThumimgUpdationDto input)
            => Result(await _messageAppService.ChangeThumimg(input));

        /// <summary>
        /// 获取文章详细信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("info")]
        public async Task<ActionResult<MessageDto>> GetInfoAsync([FromQuery] ReadCreationDto input)
            => Result(await _messageAppService.GetInfoAsync(input));

        /// <summary>
        /// 更新阅读时长
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut("duration")]
        public async Task<ActionResult> ChangeDurationAsync([FromBody]ReadUpdationDto input)
            => Result(await _messageAppService.ChangeDurationAsync(input));


        /// <summary>
        /// 获取阅读者列表(单文章汇总)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("page/reader")]
        public async Task<PageModelDto<ReadDetailDto>> GetPageAsync([FromQuery]ReadDetailSearchPagedDto input)
            => await _messageAppService.GetPageAsync(input);

        /// <summary>
        /// 总阅读分组明细（发布人所有创作）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("page/group")]
        [AllowAnonymous]
        public async Task<PageModelDto<ReadDetailDto>> ReadCountGroupByAsync([FromQuery]ReadDetailGroupSearchPagedDto input)
            => await _messageAppService.ReadCountGroupByAsync(input);

        /// <summary>
        /// 阅读人员明细（通用）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("page/detail")]
        [AllowAnonymous]
        public async Task<PageModelDto<ReadDetailReaderDto>> ReadDetailPagedAsync([FromQuery]ReadDetailReaderSearchPagedDto input)
            => await _messageAppService.ReadDetailPagedAsync(input);



        /// <summary>
        /// 总阅读量（发布人所有文章）注:10分钟延迟 即新增阅读量后10分钟后重查才会更新
        /// </summary>
        /// <param name="releaseid">发布者ID</param>
        /// <returns></returns>
        [HttpGet("readcount/{releaseid}")]
        [AllowAnonymous]
        public async Task<ActionResult<int>> ReadCountByReleaseidAsync([FromRoute]long releaseid)
            => await _messageAppService.ReadCountByReleaseidAsync(releaseid);

        /// <summary>
        /// 今日阅读量（发布人所有文章） 注:10分钟延迟 即新增阅读量后10分钟后重查才会更新
        /// </summary>
        /// <param name="releaseid">发布者ID</param>
        /// <returns></returns>
        [HttpGet("readcount/today/{releaseid}")]
        [AllowAnonymous]
        public async Task<ActionResult<int>> ReadCountTodaytReleaseidAsync([FromRoute] long releaseid)
            => await _messageAppService.ReadCountTodaytReleaseidAsync(releaseid);

        /// <summary>
        /// 查询文章分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("page")]
        public async Task<PageModelDto<MessageTitleDto>> GetPagedAsync([FromQuery]MessageSearchPagedDto input)
            => await _messageAppService.GetPagedAsync(input);
    }
}
