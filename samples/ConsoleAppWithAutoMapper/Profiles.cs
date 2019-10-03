namespace ConsoleAppWithAutoMapper
{
    using AutoMapper;

    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<InfinityStone, InfinityStoneDto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => $"{s.Name} Stone"));
        }
    }
}
