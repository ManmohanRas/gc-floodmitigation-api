namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveAnnualFundingDetailsCommandMappingProfile : Profile
{
   public SaveAnnualFundingDetailsCommandMappingProfile() 
    {
        CreateMap<SaveAnnualFundingDetailsCommand, FloodAnnualFundingEntity>();
    }
}
