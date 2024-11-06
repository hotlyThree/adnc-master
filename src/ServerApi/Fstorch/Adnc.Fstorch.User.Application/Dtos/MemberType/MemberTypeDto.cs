namespace Adnc.Fstorch.User.Application.Dtos.MemberType
{
    public class MemberTypeDto : OutputDto
    {
        /// <summary>
        /// 会员名称
        /// </summary>
        public string Membername { get; set; } = string.Empty;

        /// <summary>
        /// 会员类型
        /// </summary>
        public string Membertype { get; set; }


        /// <summary>
        /// 会员类型名称
        /// </summary>
        public string MembertypeLabel
        {
            get
            {
                string result = string.Empty;
                if (Membertype.IsNotNullOrWhiteSpace())
                {
                    result = Membertype switch
                    {
                        "A" => "个人",
                        "B" => "企业",
                        _ => ""
                    };
                }
                return result;
            }
        }

        /// <summary>
        /// 会员费用/年
        /// </summary>
        public decimal Memberprice { get; set; }

        /// <summary>
        /// 会员权益描述
        /// </summary>
        public string Memberdescribe { get; set; } = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public string Memberstatus { get; set; }

        /// <summary>
        /// 状态名
        /// </summary>
        public string MemberstatusLabel
        {
            get
            {
                string result = "";
                if (Memberstatus.IsNotNullOrWhiteSpace())
                {
                    result = Memberstatus switch
                    {
                        "A" => "有效",
                        "B" => "无效",
                        _ => result = ""
                    };
                }
                return result;
            }
        }
    }
}
