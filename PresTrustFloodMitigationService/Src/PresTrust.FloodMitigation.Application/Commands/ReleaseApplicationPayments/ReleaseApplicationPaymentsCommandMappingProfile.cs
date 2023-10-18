namespace PresTrust.FloodMitigation.Application.Commands;

public class ReleaseApplicationPaymentsCommandMappingProfile: Profile
{
    public ReleaseApplicationPaymentsCommandMappingProfile() 
    {
        CreateMap<FloodParcelReleaseOfFundsViewModel, FloodPropReleaseOfFundsEntity>();
    }
}
