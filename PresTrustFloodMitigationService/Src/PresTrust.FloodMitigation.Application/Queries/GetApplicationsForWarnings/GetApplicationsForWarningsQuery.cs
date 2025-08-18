using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationsForWarningsQuery : IRequest<IEnumerable<GetApplicationsForWarningsQueryViewModel>>
{
    public string ApplicationIds { get; set; }
    public string PamsPin { get; set; }
    public bool IsTransfer { get; set; }
    public string UserId { get; set; }
}
