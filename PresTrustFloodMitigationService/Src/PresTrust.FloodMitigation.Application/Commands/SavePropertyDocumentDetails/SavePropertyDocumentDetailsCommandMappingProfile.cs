namespace PresTrust.FloodMitigation.Application.Commands 
{
    public class SavePropertyDocumentDetailsCommandMappingProfile : Profile
    {
        public SavePropertyDocumentDetailsCommandMappingProfile()
        {
            CreateMap<SaveApplicationDocumentCommand, FloodApplicationDocumentEntity>();
            CreateMap<FloodApplicationDocumentEntity, SaveApplicationDocumentCommandViewModel>();
        }
    }
}
   

