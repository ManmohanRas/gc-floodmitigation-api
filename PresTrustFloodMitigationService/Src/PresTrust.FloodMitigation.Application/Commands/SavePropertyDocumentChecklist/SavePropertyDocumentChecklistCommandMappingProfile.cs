namespace PresTrust.FloodMitigation.Application.Commands
{
    public class SavePropertyDocumentChecklistCommandMappingProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public SavePropertyDocumentChecklistCommandMappingProfile()
        {
            CreateMap<PropertyDocumentViewModel, FloodPropertyDocumentEntity>()
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
