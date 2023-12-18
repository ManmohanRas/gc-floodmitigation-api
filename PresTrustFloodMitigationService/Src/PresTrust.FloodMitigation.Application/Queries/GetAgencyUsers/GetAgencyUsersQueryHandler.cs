using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresTrust.FloodMitigation.Application.Queries
{
    public class GetAgencyUsersQueryHandler : IRequestHandler<GetAgencyUsersQuery, IEnumerable<PresTrustUserEntity>>
    {
        private readonly IMapper mapper;
        private readonly IApplicationRepository applicationRepo;
        private readonly IPresTrustUserContext userContext;
        private readonly IIdentityApiConnect identityApiConnect;
        private readonly SystemParameterConfiguration systemParamOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userContext"></param>
        /// <param name="applicationRepo"></param>
        /// <param name="commentRepo"></param>
        /// <param name="feedbackRepo"></param>
        public GetAgencyUsersQueryHandler(
                    IMapper mapper,
                    IPresTrustUserContext userContext,
                   IOptions<SystemParameterConfiguration> systemParamOptions,
                    IIdentityApiConnect identityApiConnect,
                    IApplicationRepository applicationRepo)
        {
            this.mapper = mapper;
            this.identityApiConnect = identityApiConnect;
            this.systemParamOptions = systemParamOptions.Value;
            this.applicationRepo = applicationRepo;
            this.userContext = userContext;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PresTrustUserEntity>> Handle(GetAgencyUsersQuery request, CancellationToken cancellationToken)
        {
            // Identity's Users api - IdentityApi
            var endPoint = $"{systemParamOptions.IdentityApiSubDomain}/UserAdmin/users/pres-trust/flood/{request.AgencyId}";
            var resultUsers = await identityApiConnect.GetDataAsync<List<IdentityApiUser>>(endPoint);
            var agencyUsers = mapper.Map<IEnumerable<IdentityApiUser>, IEnumerable<PresTrustUserEntity>>(resultUsers);

            return agencyUsers;
        }
    }
}
