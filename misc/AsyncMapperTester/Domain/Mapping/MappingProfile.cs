using AutoMapper;

namespace AsyncMapperTester
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SourceClass, DestinationClass>()
                .ForMember(x => x.Extra, o => o.Ignore());
        }
    }
}