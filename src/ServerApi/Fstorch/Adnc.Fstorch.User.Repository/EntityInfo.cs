
using Adnc.Fstorch.User.Repository.Entities;

namespace Adnc.Fstorch.User.Repository
{
    public class EntityInfo : AbstracSharedEntityInfo
    {
        public EntityInfo(UserContext userContext) : base(userContext)
        {
        }

        protected override Assembly GetCurrentAssembly() => GetType().Assembly;

        protected override void SetTableName(dynamic modelBuilder)
        {
            if (modelBuilder is not ModelBuilder builder)
                throw new ArgumentNullException(nameof(modelBuilder));

            builder.Entity<Da_userinfo>().ToTable("da_userinfo");
            builder.Entity<Da_companyinfo>().ToTable("da_companyinfo");
            builder.Entity<Sys_Account>().ToTable("sys_account");
            builder.Entity<Da_cardinfo>().ToTable("da_cardinfo");
            builder.Entity<Da_ReadDetail>().ToTable("da_readdetail");
            builder.Entity<Sys_Program>().ToTable("sys_program");
            builder.Entity<Da_HomeInfo>().ToTable("da_homeinfo");
            builder.Entity<Sys_Style>().ToTable("sys_style");
            builder.Entity<Da_SubScribe>().ToTable("da_subscribe");
            builder.Entity<Da_Subscribe_Detail>().ToTable("da_subscribe_detail");
            builder.Entity<Bm_Member_Type>().ToTable("bm_member_type");
            builder.Entity<Da_Card_Giveaway>().ToTable("da_card_giveaway");
            builder.Entity<Da_Cash_Out>().ToTable("da_cash_out");
            builder.Entity<Da_Pay_Detail>().ToTable("da_pay_detail");
            builder.Entity<Da_Revenue_Detail>().ToTable("da_revenue_detail");
        }
    }
}
