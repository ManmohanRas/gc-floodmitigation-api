namespace PresTrust.FloodMitigation.Application.CommonViewModels;

public class FloodApplicationUserViewModel: PresTrustUserEntity
{
    public int Id { get; set; }
    public bool IsPrimaryContact { get; set; }
    public bool IsAlternateContact { get; set; }
}
