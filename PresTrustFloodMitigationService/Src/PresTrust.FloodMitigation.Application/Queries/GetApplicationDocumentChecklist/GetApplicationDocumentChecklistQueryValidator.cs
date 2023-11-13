using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresTrust.FloodMitigation.Application.Queries.GetApplicationDocumentChecklist
{
    public class GetApplicationDocumentChecklistQueryValidator : AbstractValidator<GetApplicationDocumentChecklistQuery>
    {
        public GetApplicationDocumentChecklistQueryValidator()
        {
            RuleFor(query => query.ApplicationId)
                   .GreaterThan(0).WithMessage("Not a valid Application Id");
        }
    }
}
