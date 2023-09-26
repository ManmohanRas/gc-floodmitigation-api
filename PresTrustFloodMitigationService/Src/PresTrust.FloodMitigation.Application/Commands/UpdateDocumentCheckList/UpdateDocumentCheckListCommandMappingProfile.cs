using AutoMapper;
using PresTrust.FloodMitigation.Application.CommonViewModels;
using PresTrust.FloodMitigation.Domain.Entities;
using PresTrust.FloodMitigation.Domain.Enums;
using System;

namespace PresTrust.FloodMitigation.Application.Commands
{
    /// <summary>
    /// This class defines the configuration using profiles.
    /// </summary>
    public class UpdateDocumentCheckListCommandMappingProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public UpdateDocumentCheckListCommandMappingProfile()
        {
            CreateMap<ApplicationDocumentViewModel, FloodApplicationDocumentEntity>()
              .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => MapDocumentType(src.DocumentType)));
        }

        /// <summary>
        /// Parse string to enum typeof(DocumentTypeEnum)
        /// </summary>
        /// <param name="docType"></param>
        /// <returns></returns>
        public ApplicationDocumentTypeEnum MapDocumentType(string docType)
        {
            Enum.TryParse(value: docType, ignoreCase: true, out ApplicationDocumentTypeEnum histDocType);
            return histDocType;
        }
    }
}
