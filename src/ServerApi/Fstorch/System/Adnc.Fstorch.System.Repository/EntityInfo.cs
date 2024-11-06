using Adnc.Fstorch.System.Repository.Entities;
using Adnc.Shared;
using Adnc.Shared.Repository.EfEntities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;
namespace Adnc.Fstorch.System.Repository
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

           
            builder.Entity<GroupInfo>().ToTable("groupinfo");
            builder.Entity<GroupUserInfo>().ToTable("groupuserinfo");
            builder.Entity<GroupMenuInfo>().ToTable("groupmenuinfo");
            builder.Entity<GroupWareHouseInfo>().ToTable("groupwarehouseinfo");
        }
    }
}
