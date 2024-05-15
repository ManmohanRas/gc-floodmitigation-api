namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetApplicationsForWarningsSqlCommand
{
    private readonly string _sqlCommand =
       @"   SELECT
	            AP.ApplicationId AS Id,
	            A.Title,
	            A.StatusId AS ApplicationStatusId,
	            AP.StatusId AS PropertyStatusId,
	            PSL.LastUpdatedOn AS StatusChangeDate
            FROM Flood.FloodApplicationParcel AP
            LEFT JOIN Flood.FloodParcelStatusLog PSL ON AP.ApplicationId = PSL.ApplicationId AND AP.PamsPin = PSL.PamsPin AND AP.StatusId = PSL.StatusId
            JOIN Flood.FloodApplication A ON AP.ApplicationId = A.Id
            WHERE AP.ApplicationId IN @p_ApplicationIds AND AP.PamsPin = @p_PamsPin;";

    public GetApplicationsForWarningsSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
