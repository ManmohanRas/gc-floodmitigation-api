namespace PresTrust.FloodMitigation.Application.Queries;

public class GetProgramManagerParcelsQuery : IRequest<GetProgramManagerParcelsQueryViewModel>
{
    public int pageNumber { get; set; } = 1;
    public int pageRows { get; set; } = 10;
    public string searchText { get; set; } = "";
}
