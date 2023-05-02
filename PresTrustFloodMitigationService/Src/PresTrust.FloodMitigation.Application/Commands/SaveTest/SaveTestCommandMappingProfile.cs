namespace PresTrust.FloodMitigation.Application.Commands
{
    public class SaveTestCommandMappingProfile : Profile
    {
        public SaveTestCommandMappingProfile()
        {
            CreateMap<SaveTestCommand, FlmitigTestEntity>();
        }
    }
}
