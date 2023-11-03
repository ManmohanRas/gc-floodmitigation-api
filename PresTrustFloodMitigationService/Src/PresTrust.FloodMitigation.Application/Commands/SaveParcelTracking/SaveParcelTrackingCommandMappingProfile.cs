namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveParcelTrackingCommandMappingProfile : Profile
{
    public SaveParcelTrackingCommandMappingProfile()
    {
        CreateMap<SaveParcelTrackingCommand, FloodParcelTrackingEntity>();
    }
}
