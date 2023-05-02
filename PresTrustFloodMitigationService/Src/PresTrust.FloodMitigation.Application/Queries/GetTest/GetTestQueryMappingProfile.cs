namespace PresTrust.FloodMitigation.Application.Queries
{
    public class GetTestQueryMappingProfile : Profile
    {
        public GetTestQueryMappingProfile()
        {
            CreateMap<FlmitigTestEntity, GetTestQueryViewModel>();
        }
    }
}
