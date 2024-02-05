namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveProgramExpensesCommandMappingProfile : Profile
{
    public SaveProgramExpensesCommandMappingProfile()
    {
        CreateMap<SaveProgramExpensesCommand, FloodProgramExpensesEntity>();
    }
}
