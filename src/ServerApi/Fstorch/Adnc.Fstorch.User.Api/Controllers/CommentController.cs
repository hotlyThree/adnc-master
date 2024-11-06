using Adnc.Fstorch.User.Application.Dtos.Comments;
using Microsoft.AspNetCore.Mvc;

namespace Adnc.Fstorch.User.Api.Controllers
{
    /// <summary>
    /// 评论管理
    /// </summary>
    [ApiController]
    [Route($"{RouteConsts.CommRoot}")]
    public class CommentController : AdncControllerBase
    {
        private readonly ICommentAppService _commentAppService;
        public CommentController(ICommentAppService commentAppService)
        {
            _commentAppService = commentAppService;
        }


        /// <summary>
        /// 新增评论
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("ins")]
        [AllowAnonymous]
        public async Task<ActionResult<long>> CreateAsync([FromBody]CommentCreationDto input)
            => CreatedResult(await _commentAppService.CreateAsync(input.Msgid, input));

        /// <summary>
        /// 获取文章评论列表
        /// </summary>
        /// <param name="reader">读者ID</param>
        /// <param name="id">文章ID</param>
        /// <returns></returns>
        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CommentSimpleTreeListDto>>> GetList(long reader, long id)
            => await _commentAppService.GetListAsync(reader, id);


        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="msgid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("del/{msgid}/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> DeleteAsync([FromRoute]long msgid, [FromRoute]long id)
            => Result(await _commentAppService.DeleteAsync(msgid, id));
    }
}
