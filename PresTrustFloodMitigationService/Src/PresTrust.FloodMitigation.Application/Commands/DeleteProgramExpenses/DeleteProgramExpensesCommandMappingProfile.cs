namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteProgramExpensesCommandMappingProfile : Profile
{
    public DeleteProgramExpensesCommandMappingProfile()
    {
        CreateMap<DeleteProgramExpensesCommand, FloodProgramExpensesEntity>();
    }
}
