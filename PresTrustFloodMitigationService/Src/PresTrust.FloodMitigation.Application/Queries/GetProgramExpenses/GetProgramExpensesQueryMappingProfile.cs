namespace PresTrust.FloodMitigation.Application.Queries;

public class GetProgramExpensesQueryMappingProfile : Profile
{
    public GetProgramExpensesQueryMappingProfile()
    {
        CreateMap<FloodProgramExpensesEntity, GetProgramExpensesQueryViewModel>();
    }

}
