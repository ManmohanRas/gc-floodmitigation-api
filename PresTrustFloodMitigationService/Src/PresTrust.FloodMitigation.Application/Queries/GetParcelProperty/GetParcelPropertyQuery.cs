using System.ComponentModel.DataAnnotations;

namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelPropertyQuery : IRequest<GetParcelPropertyQueryViewModel> 
{
    public int ApplicationId { get; set; }

    public required string PamsPin { get; set; }

}
