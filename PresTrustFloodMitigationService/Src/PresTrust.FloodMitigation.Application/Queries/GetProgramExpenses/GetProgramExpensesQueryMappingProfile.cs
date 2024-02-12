namespace PresTrust.FloodMitigation.Application.Queries;

public class GetProgramExpensesQueryMappingProfile : Profile
{
    public GetProgramExpensesQueryMappingProfile()
    {
        CreateMap<FloodProgramExpensesEntity, GetProgramExpensesQueryViewModel>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()));
    }
}
