namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveProgramExpensesCommandMappingProfile : Profile
{
    public SaveProgramExpensesCommandMappingProfile()
    {
        CreateMap<SaveProgramExpensesCommand, FloodProgramExpensesEntity>();
            //.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()));
    }
}
