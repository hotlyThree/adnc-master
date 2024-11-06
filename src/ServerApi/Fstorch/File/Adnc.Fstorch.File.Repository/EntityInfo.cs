﻿



namespace Adnc.Fstorch.File.Repository
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
            builder.Entity<Da_Files>().ToTable("da_files");
        }
    }
}
