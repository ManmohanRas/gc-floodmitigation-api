namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFlapTargetAreaCommandMappingProfile: Profile
{
    public SaveFlapTargetAreaCommandMappingProfile()
    {
        CreateMap<SaveFlapTargetAreaCommand, FloodFlapTargetAreaEntity>();
        CreateMap<SaveFloodFlapParcelViewModel, FloodParcelEntity>();
    }
}
