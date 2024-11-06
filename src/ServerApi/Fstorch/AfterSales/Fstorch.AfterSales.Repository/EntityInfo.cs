namespace Fstorch.AfterSales.Repository
{
    public class EntityInfo : AbstracSharedEntityInfo
    {
        public EntityInfo(UserContext userContext) : base(userContext)
        {
        }

        //重写实体表映射关系，可自定义复合主键
        /*public override void OnModelCreating(dynamic modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            if (modelBuilder is ModelBuilder builder)
            {
                *//*builder.Entity<Bm_Service_Type>()
                    .HasKey(b => new { b.CompanyId, b.ServiceTypeId });*//*

            }
        }*/

        protected override Assembly GetCurrentAssembly() => GetType().Assembly;

        protected override void SetTableName(dynamic modelBuilder)
        {
            if (modelBuilder is not ModelBuilder builder)
                throw new ArgumentNullException(nameof(modelBuilder));
            builder.Entity<Bm_Service_Type>().ToTable("bm_service_type");
            builder.Entity<Bm_Service_Nature>().ToTable("bm_service_nature");
            builder.Entity<Bm_Information_Source>().ToTable("bm_information_source");
            builder.Entity<Bm_User_Type>().ToTable("bm_user_type");
            builder.Entity<Bm_Buy_Market>().ToTable("bm_buy_market");
            builder.Entity<Bm_Service_Brand>().ToTable("bm_service_brand");
            builder.Entity<Bm_Brand_Content>().ToTable("bm_brand_content");
            builder.Entity<Bm_Settlement_Code>().ToTable("bm_settlement_code");
            builder.Entity<Bm_Service_Request>().ToTable("bm_service_request");
            builder.Entity<Bm_Service_Cause>().ToTable("bm_service_cause");
            builder.Entity<Da_Service_Order_Produc>().ToTable("da_service_order_produc");
            builder.Entity<Da_Service_Order>().ToTable("da_service_order");
            builder.Entity<Da_Service_Product>().ToTable("da_service_product");
            builder.Entity<Da_Service_Order_Process>().ToTable("da_service_order_process");
        }
    }
}
