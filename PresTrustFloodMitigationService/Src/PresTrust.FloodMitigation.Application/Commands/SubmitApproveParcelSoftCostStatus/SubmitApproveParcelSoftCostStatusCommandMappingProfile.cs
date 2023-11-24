namespace PresTrust.FloodMitigation.Application.Commands;

public class SubmitApproveParcelSoftCostStatusCommandMappingProfile: Profile
{
    public SubmitApproveParcelSoftCostStatusCommandMappingProfile() {
        CreateMap<SubmitApproveParcelSoftCostStatusCommand, FloodApplicationParcelEntity>();
    }
}
