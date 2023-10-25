namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetApplicationParcelSqlCommand
{
    private readonly string _sqlCommand =
            @" SELECT
					FA.[ApplicationId],	FA.[PamsPin], FA.[StatusId], FP.[LastUpdatedBy]
				FROM	[Flood].[FloodApplicationParcel] FA, [Flood].[FloodParcelProperty] FP
				where	FA.[ApplicationId] = FP.[ApplicationId] and FA.[PamsPin] = FP.[PamsPin] and FA.[ApplicationId] = @p_ApplicationId and FA.[PamsPin] = @p_PamsPin;";

    public GetApplicationParcelSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
