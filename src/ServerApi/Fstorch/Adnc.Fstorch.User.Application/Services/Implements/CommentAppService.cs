

using Adnc.Fstorch.User.Application.Dtos.Comments;
using Adnc.Fstorch.User.Application.Dtos.WeChat;
using Microsoft.EntityFrameworkCore.Internal;
using System.Xml.Linq;

namespace Adnc.Fstorch.User.Application.Services.Implements
{
    public class CommentAppService : AbstractAppService, ICommentAppService
    {
        private readonly IEfRepository<Da_Comments> _comRepository;
        private readonly IEfRepository<Da_msg> _msgRepository;
        private readonly IEfRepository<Sys_Account> _accountRepository;
        private readonly IWeChatAppService _weChatAppService;
        private readonly CacheService _cacheService;

        public CommentAppService(IEfRepository<Da_Comments> comRepository, IEfRepository<Sys_Account> accountRepository, CacheService cacheService,
            IEfRepository<Da_msg> msgRepository, IWeChatAppService weChatAppService)
        {
            _comRepository = comRepository;
            _msgRepository = msgRepository;
            _accountRepository = accountRepository;
            _cacheService = cacheService;
            _weChatAppService = weChatAppService;
        }

        public async Task<AppSrvResult<long>> CreateAsync(long id, CommentCreationDto input)
        {
            input.TrimStringFields();
            var comment = Mapper.Map<Da_Comments>(input);
            comment.Id = IdGenerater.GetNextId();
            comment.Createtime = DateTime.Now;
            var openid = (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id == input.Reviewer).Openid;
            var objectCheck = await _weChatAppService.CommentCheck(new ContentCreationDto
            {
                openid = openid,
                content = input.Content
            });
            var checkResult = (dynamic)objectCheck;
            if (checkResult.errcode != 0 )
                return Problem(HttpStatusCode.BadRequest, checkResult.errmsg);
            var label = (int)checkResult.result.label switch
            {
                10001 => "包含广告言论",
                20001 => "包含时政言论",
                20002 => "包含色情言论",
                20003 => "包含辱骂言论",
                20006 => "包含违法犯罪言论",
                20008 => "包含欺诈言论",
                20012 => "包含低俗言论",
                20013 => "涉及版权言论",
                21000 => "其他",
                _ => ""
            };
            if (checkResult.result.label != 100)
                return Problem(HttpStatusCode.BadRequest, label);
            await _comRepository.InsertAsync(comment);
            return comment.Id;
        }

        public async Task<AppSrvResult> DeleteAsync(long msgid, long id)
        {
            await _comRepository.DeleteAsync(id);
            return AppSrvResult();
        }

        public async Task<List<CommentSimpleTreeListDto>> GetListAsync(long reader, long id)
        {
            if (reader == 0 || id == 0)
                return new List<CommentSimpleTreeListDto>();
            var commentTreeListDtos = await _cacheService.GetCommentsTreeFromCacheAsync(id, reader);
            if(commentTreeListDtos == null)
                return new List<CommentSimpleTreeListDto>();
            return commentTreeListDtos;
        }
    }
}
