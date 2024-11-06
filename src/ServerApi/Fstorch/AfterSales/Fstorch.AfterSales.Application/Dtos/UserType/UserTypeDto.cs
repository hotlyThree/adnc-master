namespace Fstorch.AfterSales.Application.Dtos.UserType
{
    [Serializable]
    public class UserTypeDto : OutputDto
    {

        /// <summary> 
        /// 用户类型名称
        /// </summary>
        public string UserTypeName { get; set; }

        /// <summary> 
        /// 显示序号
        /// </summary>
        public int DisplayNumber { get; set; }

        /// <summary> 
        /// 是否可用
        /// </summary>
        public string IsValid { get; set; }
    }
}
