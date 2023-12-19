using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresTrust.FloodMitigation.Application.Queries
{
    public class GetAgencyUsersQueryValidator : AbstractValidator<GetAgencyUsersQuery>
    {
        /// <summary>
        /// create rules for attributes
        /// </summary>
        public GetAgencyUsersQueryValidator()
        {
            RuleFor(query => query.AgencyId)
                .Cascade(CascadeMode.Stop)
                .NotNull().NotEmpty().WithMessage("AgencyId is required.")
                .GreaterThan(0)
                .WithMessage("AgencyId must be greater than 0");
        }
    }
}
