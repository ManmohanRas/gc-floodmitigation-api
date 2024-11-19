namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetApplicationParcelSqlCommand
{
    private readonly string _sqlCommand =
            @" SELECT
	            FA.[ApplicationId],
	            FA.[PamsPin],
	            FA.[StatusId],
                FA.[IsLocked],
                FA.[IsSubmitted],
                FA.[IsApproved],
                CASE 
                    WHEN FP.TargetAreaId > 0 
                    THEN 1
                    ELSE 0
                    END AS IsFlap
            FROM	[Flood].[FloodApplicationParcel] FA
            LEFT JOIN [Flood].[FloodParcel] FP ON (FA.PamsPin = FP.PamsPin)
            WHERE	FA.[ApplicationId] = @p_ApplicationId and FA.[PamsPin] = @p_PamsPin;";

    public GetApplicationParcelSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
