namespace PresTrust.FloodMitigation.Application.Queries;
    public class GetMunicipalCommentsQueryMappingProfile : Profile
    {
        public GetMunicipalCommentsQueryMappingProfile()
        {
           CreateMap<FloodMunicipalCommentEntity, GetMunicipalCommentsQueryViewModel>();
        }
    }

