namespace PresTrust.FloodMitigation.Application.Queries;

public class CheckDuplicatePropertyQuery : IRequest<bool> 
{
    public int Id { get; set; }
    public string PamsPin { get; set; }
}
