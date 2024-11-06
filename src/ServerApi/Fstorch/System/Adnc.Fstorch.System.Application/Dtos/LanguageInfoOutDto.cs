using Adnc.Shared.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.System.Application.Dtos
{
    [Serializable]
    public class LanguageInfoOutDto :OutputDto
    {
        
        /// <summary>
        /// 产品ID
        /// </summary>
        public long ProductID { get; set; }
        /// <summary>
        /// 语言类型（A字段名 B返回参数）
        /// </summary>
        public string LanguageType { get; set; }
        /// <summary>
        /// 语言名称（A中文 B英文 ）
        /// </summary>
        public string LanguageName { get; set; }
        /// <summary>
        /// 数据表名（类型为字段时为表名，返回参数时为接口名）
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 翻译名（类型为字段时为字段名，返回参数时为参数名）
        /// </summary>
        public string TranslateName { get; set; }
        /// <summary>
        /// 翻译值
        /// </summary>
        public string TranslateValue { get; set; }
    }
}
