using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Demo.Shared.Const.Caching.User
{
    public class CachingConsts
    {
        //cache prefix
        public const string UserValidatedInfoKeyPrefix = "adnc:fstorch:users:validatedinfo";

        public const string CommentsTreeListKeyPrefix = "adnc:fstorch:comments:tree";

        public const string NameCardListCacheKey = "adnc:fstorch:users:card:list";

        public const string CompanyListCacheKey = "adnc:fstorch:company:list";

        public const string AccountListCacheKey = "adnc:fstorch:account:info";

        public const string ReadCountKeyPrefix = "adnc:fstorch:readcount";
        public const string ReadCountTodayKeyPrefix = "adnc:fstorch:readcount:today";

        public const string CardCaseCountKeyPrefix = "adnc:fstorch:cardcase";
        public const string CardCaseCountTodayKeyPrefix = "adnc:fstorch:cardcase:today";

        public const string StyleListCacheKey = "adnc:fstorch:style:list";

        public const string BackGroundListCacheKey = "adnc:fstorch:background:list";

        public const string OfficeAccountAccessTokenCacheKey = "adnc:fstorch:office_account:access_token";
        public const string MiniProgramAccessTokenCacheKey = "adnc:fstorch:miniprogram:access_token";

        public const string AccountMemberPayTypeCacheKey = "adnc:fstorch:account:pay";
    }
}
