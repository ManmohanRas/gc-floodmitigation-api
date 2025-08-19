using MediatR;
using PresTrust.FloodMitigation.Application.CommonViewModels;
using System;
using System.Collections.Generic;

namespace PresTrust.FloodMitigation.Application.Commands
{
    /// <summary>
    /// This class represents api's command input model and returns the response object
    /// </summary>
    public class SaveApplicationDocumentChecklistCommand : IRequest<Unit>
    {
        public int ApplicationId { get; set; }
        public string UserId { get; set; }
        public IEnumerable<ApplicationDocumentViewModel> Documents { get; set; }
    }
}
