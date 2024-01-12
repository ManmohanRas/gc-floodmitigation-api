namespace PresTrust.FloodMitigation.Application.Queries;

public class GetProgramManagerParcelsQuery : IRequest<GetProgramManagerParcelsQueryViewModel>
{
    public int PageNumber { get; set; } = 1;
    public int PageRows { get; set; } = 10;
    public string SearchBlockText { get; set; } = "";
    public string SearchLotText { get; set; } = "";
    public string SearchAddressText { get; set; } = "";
}
