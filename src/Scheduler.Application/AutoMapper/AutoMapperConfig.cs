using AutoMapper;

namespace ProductionOrder.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelMapping());
                cfg.AddProfile(new ViewModelToDomainMapping());
            });
        }
    }
}