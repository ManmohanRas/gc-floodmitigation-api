using System;
using System.IO;

namespace PresTrust.FloodMitigation.Application.Queries;

public class ExportTargetListQueryHandler: IRequestHandler<ExportTargetListQuery, Unit>
{
    private readonly IParcelRepository repoParcels;
    private readonly IFlapModuleRepository repoFlap;

    public ExportTargetListQueryHandler
        (
        IParcelRepository repoParcels,
        IFlapModuleRepository repoFlap
        )
    {
        this.repoParcels = repoParcels;
        this.repoFlap = repoFlap;
    }

    public async Task<Unit> Handle(ExportTargetListQuery request, CancellationToken cancellationToken)
    {
        string csv = String.Empty;
        string[] columnNames = { "PamsPin", "AgencyId", "Block", "Lot", "QualificationCode", "StreetNo", "StreetAddress", "OwnersName", "TargetArea" };
        var parcels = await repoParcels.GetParcelsByTargetAreaIdAsync(request.AgencyId);

        foreach (string columnName in columnNames)
        {
            csv += columnName + ',';
        }

        csv += "\r\n";

        foreach (var parcel in parcels)
        {
            csv += parcel.PamsPin.Replace(",", ";") + ",";
            csv += parcel.AgencyId.ToString().Replace(",", ";") + ",";
            csv += parcel.Block.Replace(",", ";") + ",";
            csv += parcel.Lot.Replace(",", ";") + ",";
            csv += parcel.QCode.Replace(",", ";") + ",";
            csv += parcel.StartNo.ToString().Replace(",", ";") + ",";
            csv += parcel.StreetAddress.Replace(",", ";") + ",";
            csv += parcel.LandOwner.Replace(",", ";") + ",";
            csv += parcel.TargetArea.Replace(",", ";") + ",";

            csv += "\r\n";

        }

        byte[] bytes = Encoding.ASCII.GetBytes(csv);
        return Unit.Value;

    }
}
