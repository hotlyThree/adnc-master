using Adnc.Fstorch.System.Application.Dtos;
using Adnc.Fstorch.System.Repository.Entities;
using Adnc.Infra.IRepositories;
using Adnc.Infra.Redis.Caching;
using Adnc.Shared;
using Adnc.Shared.Application.Caching;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.System.Application.Cache
{

    public sealed class CacheService : AbstractCacheService, ICachePreheatable
    {
        
        private const string CurrentSystemProductID = "22220"; //当前系统的产品ID，每个系统必须唯一（开发时针对系统进行修改一次）
        public CacheService(
            Lazy<ICacheProvider> cacheProvider,
            Lazy<IServiceProvider> serviceProvider)
            : base(cacheProvider, serviceProvider)
        {
        }

        public override async Task PreheatAsync()
        {

        }

        /// <summary>
        /// 获取对应产品语言接口
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public async Task<List<LanguageInfoOutDto>> GetAllLanguageFromCacheAsync()
        {
            var cacheValue = await CacheProvider.Value.GetAsync("adnc:language:user:"+ CurrentSystemProductID, async () =>
            {
                //从指定服务器获取产品语言信息
                try
                {
                    string s = Core.WebHelper.HttpPost.PostAsync(@"http://192.168.100.28:5108/Server/Language/QueryLanguageProductAsync?ProductID=22220", "").Result;
                    if (s.Equals("error"))
                    {
                        return new List<LanguageInfoOutDto>();
                    }
                    else
                    {
                        List<LanguageInfoOutDto> cr = (List<LanguageInfoOutDto>)JsonConvert.DeserializeObject<List<LanguageInfoOutDto>>(s);
                        return cr;
                    }
                }
                catch
                {
                    return new List<LanguageInfoOutDto>();
                }
            }, TimeSpan.FromSeconds(GeneralConsts.OneYear));
            return cacheValue.Value;
        }

        public async Task<string> FindLanguageFromCacheAsync(string LanguageType, string TableName, string TranslateName)
        {
            List<LanguageInfoOutDto> lang= await GetAllLanguageFromCacheAsync();
            if (lang != null && lang.Count > 0)
            {               
                    return JsonConvert.SerializeObject(lang.FindAll(x => x.LanguageType.Equals(LanguageType) &&
                        x.TableName.Equals(TableName) &&
                        x.TranslateName.Equals(TranslateName)));               
            }
            else
            {
                return "";
            }
        }
    }
}
