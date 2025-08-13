namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class represents api's command input model and returns the response object
/// </summary>
public class SaveProjectAreaCommand : IRequest<SaveProjectAreaCommandViewModel>
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public int ApplicationSubTypeId { get; set; }
    public int? NoOfHomes { get; set; }
    public int? NoOfContiguousHomes { get; set; }
    public List<string> Parcels { get; set; } = new List<string>();
    public string UserId { get; set; }

}

