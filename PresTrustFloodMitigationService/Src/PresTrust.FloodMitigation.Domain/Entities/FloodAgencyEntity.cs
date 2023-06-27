namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodAgencyEntity
{
    public int Id { get; set; }
    public string AgencyName { get; set; }
    public string AgencyLabel { get; set; }
    public string AgencyType { get; set; }
    public string EntityType { get; set; }
    public string EntityName { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string AddressLine3 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    public string Address
    {
        get
        {
            string result = string.Empty;
            result = (this.AddressLine1 ?? string.Empty) + " " + (this.AddressLine2 ?? string.Empty) + " " + (this.AddressLine3 ?? string.Empty);
            result += " " + (this.City ?? string.Empty) + " " + (this.State ?? string.Empty) + " " + (this.ZipCode ?? string.Empty);
            return result;
        }
    }
}
