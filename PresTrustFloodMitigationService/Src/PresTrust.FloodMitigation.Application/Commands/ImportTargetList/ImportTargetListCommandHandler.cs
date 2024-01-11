using Microsoft.AspNetCore.Http;
using PresTrust.FloodMitigation.Domain.Entities;
using System.Collections.Generic;
using System.Text.Json;

namespace PresTrust.FloodMitigation.Application.Commands;

public class ImportTargetListCommandHandler : IRequestHandler<ImportTargetListCommand, Unit>
{
    private readonly IHttpContextAccessor accessor;
    private readonly IFlapModuleRepository repoFlap;
    private readonly IParcelRepository repoParcels;

    public ImportTargetListCommandHandler
        (
        IHttpContextAccessor accessor,
        IFlapModuleRepository repoFlap,
        IParcelRepository repoParcels
        )
    {
        this.accessor = accessor;
        this.repoFlap = repoFlap;
        this.repoParcels = repoParcels;
    }
    public async Task<Unit> Handle(ImportTargetListCommand request, CancellationToken cancellationToken)
    {
        List<FloodParcelEntity> importedParcels = new List<FloodParcelEntity>();
        string sessionParcels = default;

        if (accessor.HttpContext.Session.GetString("Parcels") !=  null)
        {
            sessionParcels = accessor.HttpContext.Session.GetString("Parcels"); // Get Session  
            importedParcels = (List<FloodParcelEntity>)JsonConvert.DeserializeObject(sessionParcels);
        }

        var existingTargerAreas = await repoFlap.GetFlapTargetAreasAsync(request.AgencyId);

        //var reqTargetAreas = importParcels.ToList().Select(x => x.TargetArea);


        return Unit.Value;
    }
}
