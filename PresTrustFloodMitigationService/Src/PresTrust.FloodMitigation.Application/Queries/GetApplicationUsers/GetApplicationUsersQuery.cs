using PresTrust.FloodMitigation.Application.CommonViewModels;

namespace PresTrust.FloodMitigation.Application.Queries
{
    /// <summary>
    /// This class represents api's query input model and returns the response object
    /// </summary>
    public class GetApplicationUsersQuery: IRequest<IEnumerable<FloodApplicationUserViewModel>>
    {
        public int ApplicationId { get; set; }

    }
}
