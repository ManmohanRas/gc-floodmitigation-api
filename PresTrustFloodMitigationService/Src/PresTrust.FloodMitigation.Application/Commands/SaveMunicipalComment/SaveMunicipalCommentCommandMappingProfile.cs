namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveMunicipalCommentCommandMappingProfile:Profile
{
    public SaveMunicipalCommentCommandMappingProfile()
    {
        CreateMap<SaveMunicipalCommentCommand, FloodMunicipalCommentEntity>();
    }
}
