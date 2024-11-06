

namespace Fstorch.Product.Repository
{
    public class EntityInfo : AbstracSharedEntityInfo
    {
        public EntityInfo(UserContext userContext) : base(userContext)
        {
        }

        protected override Assembly GetCurrentAssembly() => GetType().Assembly;

        protected override void SetTableName(dynamic modelBuilder)
        {
            //base.SetTableName(modelBuilder);
        }
    }
}
