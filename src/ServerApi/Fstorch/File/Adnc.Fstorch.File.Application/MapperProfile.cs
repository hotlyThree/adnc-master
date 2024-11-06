

using Adnc.Fstorch.File.Application.Dtos.File;

namespace Adnc.Fstorch.File.Application
{
    public sealed class MapperProfile : Profile
    {

        public MapperProfile()
        {
            //映射配置，需要用到Map方法的映射都需要在这里进行统一注册，否则无法正常使用
            //CreateMap(传入的类，目标类)
            CreateMap<FileCreationDto, Da_Files>();
            CreateMap<Da_Files, FileDto>();
            CreateMap<FilePublicCreationDto, Da_Files>();
        }
    }
}
