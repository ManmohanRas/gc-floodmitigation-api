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
            //TBD
            var resultUsers = new List<IdentityApiUser>() {
                    new IdentityApiUser() { Email = "agencyadmin_1401@gmail.com", IsEnabled = true, PhoneNumber="9873734737", UserId="1401", UserName="agencyadmin_1401", Title="",
                    Roles = new List<IdentityUserRole>(){ new IdentityUserRole() { Name = "flood_agencyadmin" } } },
                    new IdentityApiUser() { Email = "agencyeditor_1402@gmail.com", IsEnabled = false, PhoneNumber="9786756756", UserId="1402", UserName="agencyeditor_1402", Title="",
                    Roles = new List<IdentityUserRole>(){ new IdentityUserRole() { Name = "flood_agencyeditor" } } },
                    new IdentityApiUser() { Email = "agencysignatory_1403@gmail.com", IsEnabled = false, PhoneNumber="9786756756", UserId="1403", UserName="agencysignatory_1403", Title="",
                    Roles = new List<IdentityUserRole>(){ new IdentityUserRole() { Name = "flood_agencysignature" } } },
                    new IdentityApiUser() { Email = "agencyreadonly_1404@gmail.com", IsEnabled = false, PhoneNumber="9786756756", UserId="1404", UserName="agencyreadonly_1404", Title="",
                    Roles = new List<IdentityUserRole>(){ new IdentityUserRole() { Name = "flood_agencyreadonly" } } },
                };
            var agencyUsers = mapper.Map<IEnumerable<IdentityApiUser>, IEnumerable<PresTrustUserEntity>>(resultUsers);
            foreach (var item in agencyUsers)
            {
                item.Status = item.IsEnabled ? "Active" : "In-Active";
            }
            return agencyUsers;
        }
    }
}
