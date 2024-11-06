namespace Adnc.Fstorch.User.Application.Dtos.ReadDetail
{
    public class ReadDetailGroupSearchPagedDto : SearchPagedDto
    {
        /// <summary>
        /// 发布者ID
        /// </summary>
        public long Releaseid { get; set; }


        /// <summary>
        /// 是否查询今日 默认0 查询全部   为1查今日
        /// </summary>
        public int Today { get; set; } = 0;
    }
}
