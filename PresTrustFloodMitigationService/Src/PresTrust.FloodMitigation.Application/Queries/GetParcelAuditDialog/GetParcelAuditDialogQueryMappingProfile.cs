namespace PresTrust.FloodMitigation.Application.Queries;
public class GetParcelAuditDialogQueryMappingProfile: Profile
{
    public GetParcelAuditDialogQueryMappingProfile()
    {
        CreateMap < FloodParcelAuditDialogEntity, GetParcelAuditDialogQueryViewModel>();
    }
}
