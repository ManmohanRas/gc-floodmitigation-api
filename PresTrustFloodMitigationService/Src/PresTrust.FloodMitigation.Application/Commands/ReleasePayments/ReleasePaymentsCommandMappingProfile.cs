namespace PresTrust.FloodMitigation.Application.Commands;

public class ReleasePaymentsCommandMappingProfile: Profile
{
    public ReleasePaymentsCommandMappingProfile() 
    {
        CreateMap<FloodParcelReleaseOfFundsViewModel, FloodPropReleaseOfFundsEntity>();
    }
}
