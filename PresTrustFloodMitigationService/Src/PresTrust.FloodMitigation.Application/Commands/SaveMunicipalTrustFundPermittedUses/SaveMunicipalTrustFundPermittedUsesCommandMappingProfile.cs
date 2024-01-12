namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveMunicipalTrustFundPermittedUsesCommandMappingProfile: Profile
{
    public SaveMunicipalTrustFundPermittedUsesCommandMappingProfile() {
        CreateMap<SaveMunicipalTrustFundPermittedUsesCommand, FloodMunicipalTrustFundPermittedUsesEntity>();
    }
}
