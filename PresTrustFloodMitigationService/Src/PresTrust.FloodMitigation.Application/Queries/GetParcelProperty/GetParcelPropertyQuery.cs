using System.ComponentModel.DataAnnotations;

namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelPropertyQuery : IRequest<GetParcelPropertyQueryViewModel> 
{
    public int ApplicationId { get; set; }
    public string UserId { get; set; }
    public string PamsPin { get; set; }

}
