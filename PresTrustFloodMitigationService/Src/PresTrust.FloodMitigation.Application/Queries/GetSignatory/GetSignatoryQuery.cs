namespace PresTrust.FloodMitigation.Application.Queries;

/// <summary>
/// This class represents api's query input model and returns the response object
/// </summary>
public class GetSignatoryQuery : IRequest<GetSignatoryQueryViewModel>
{
    public int ApplicationId { get; set; }

}
