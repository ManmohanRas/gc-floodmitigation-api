using System;
using System.Collections;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PresTrust.FloodMitigation.Application.Queries;

public class ExportTargetListQueryHandler: IRequestHandler<ExportTargetListQuery, Unit>
{
    private readonly IParcelRepository repoParcels;
    private readonly IFlapModuleRepository repoFlap;
    private readonly IHttpContextAccessor httpContextAccessor;

    public ExportTargetListQueryHandler
        (
        IParcelRepository repoParcels,
        IFlapModuleRepository repoFlap,
        IHttpContextAccessor httpContextAccessor
        )
    {
        this.repoParcels = repoParcels;
        this.repoFlap = repoFlap;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(ExportTargetListQuery request, CancellationToken cancellationToken)
    {
        string csv = string.Empty;
        string[] columnNames = { "PamsPin", "AgencyId", "Block", "Lot", "QualificationCode", "StreetNo", "StreetAddress", "OwnersName", "TargetArea" };
        var parcels = await repoParcels.GetParcelsInTargetAreaByAgencyIdAsync(request.AgencyId);

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
            csv += !string.IsNullOrEmpty(parcel.QCode) ? parcel.QCode.Replace(",", ";") : string.Empty + ",";
            csv += parcel.StreetNo.ToString().Replace(",", ";") + ",";
            csv += parcel.StreetAddress.Replace(",", ";") + ",";
            csv += !string.IsNullOrEmpty(parcel.LandOwner) ? parcel.LandOwner.Replace(",", ";") : string.Empty + ",";
            csv += parcel.TargetArea.Replace(",", ";") + ",";

            csv += "\r\n";
        }

        byte[] bytes = Encoding.ASCII.GetBytes(csv);
        httpContextAccessor.HttpContext.Response.Headers.Add("content-disposition", "attachment;filename=targetList.csv");
        httpContextAccessor.HttpContext.Response.ContentType = "text/csv";
        httpContextAccessor.HttpContext.Response.Body.WriteAsync(bytes);
        httpContextAccessor.HttpContext.Response.Body.Flush();
        //File.WriteAllBytes("C:\\Downloads\\list.csv", bytes);
        return Unit.Value;

    }
}
