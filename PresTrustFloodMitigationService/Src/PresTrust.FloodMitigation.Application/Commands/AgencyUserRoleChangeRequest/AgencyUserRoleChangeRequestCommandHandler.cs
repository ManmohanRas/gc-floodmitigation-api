namespace PresTrust.FloodMitigation.Application.Commands
{
    public class AgencyUserRoleChangeRequestCommandHandler : BaseHandler, IRequestHandler<AgencyUserRoleChangeRequestCommand, bool>
    {
        private readonly IMapper mapper;
        private readonly IPresTrustUserContext userContext;
        private readonly SystemParameterConfiguration systemParamOptions;
        private readonly IIdentityApiConnect identityApiConnect;
        public AgencyUserRoleChangeRequestCommandHandler(
            IMapper mapper,
            IPresTrustUserContext userContext,
            IOptions<SystemParameterConfiguration> systemParamOptions,
            IIdentityApiConnect identityApiConnect
            )
        {
            this.mapper = mapper;
            this.userContext = userContext;
            this.systemParamOptions = systemParamOptions.Value;
            this.identityApiConnect = identityApiConnect;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(AgencyUserRoleChangeRequestCommand request, CancellationToken cancellationToken)
        {
            userContext.DeriveRole(request.AgencyId);

            if (userContext.Role != UserRoleEnum.AGENCY_ADMIN && userContext.Role != UserRoleEnum.PROGRAM_ADMIN && userContext.Role != UserRoleEnum.SYSTEM_ADMIN)
                throw new UnauthorizedAccessException("Unauthorized operation.");

            if (string.IsNullOrEmpty(request.Role))
            {
                request.Role = request.NewRole;
                request.NewRole = null;
            }

            // call external api - IdentityApi

            IdentityUserClaim claim = default;
            IdentityUserClaim newClaim = default;
            JsonContent postUserJson = default;

            claim = new IdentityUserClaim()
            {
                ClaimType = request.Role,
                ClaimValue = request.AgencyId.ToString()
            };

            if (!string.IsNullOrEmpty(request.NewRole))
            {
                newClaim = new IdentityUserClaim()
                {
                    ClaimType = request.NewRole,
                    ClaimValue = request.AgencyId.ToString()
                };
            }

            if (newClaim != null)
            {
                postUserJson = new JsonContent(new PutAgencyUserRoleChangeRequest()
                {
                    Email = request.Email,
                    Claim = claim,
                    NewClaim = newClaim
                });

                await this.identityApiConnect.PutDataAsync<string, JsonContent>($"{systemParamOptions.IdentityApiSubDomain}/UserAdmin/user-claim", postUserJson);
            }
            else
            {
                postUserJson = new JsonContent(new PostUserRoleRequest()
                {
                    Email = request.Email,
                    Claims = new IdentityUserClaim[] { claim }
                });

                await this.identityApiConnect.PostDataAsync<string, JsonContent>($"{systemParamOptions.IdentityApiSubDomain}/UserAdmin", postUserJson);
            }

            return true;
        }
    }
}
