namespace PresTrust.FloodMitigation.Application.Commands
{
    public class UpdatePropertyDocumentCheckListCommandMappingProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public UpdatePropertyDocumentCheckListCommandMappingProfile()
        {
            CreateMap<PropertyDocumentTypeViewModel, FloodPropertyDocumentEntity>()
              .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => MapDocumentType(src.DocumentType)));
        }

        /// <summary>
        /// Parse string to enum typeof(DocumentTypeEnum)
        /// </summary>
        /// <param name="docType"></param>
        /// <returns></returns>
        public PropertyDocumentTypeEnum MapDocumentType(string docType)
        {
            Enum.TryParse(value: docType, ignoreCase: true, out PropertyDocumentTypeEnum propDocType);
            return propDocType;
        }
    }

}
