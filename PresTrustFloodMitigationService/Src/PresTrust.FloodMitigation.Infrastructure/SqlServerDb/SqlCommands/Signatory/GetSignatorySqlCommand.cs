namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetSignatorySqlCommand
{
    private readonly string _sqlCommand =
        @"  SELECT   [Id]
                    ,[ApplicationId]
                    ,[Designation]
                    ,[Title]
                    ,[SignedOn]						AS SignatureOn
                    ,[LastUpdatedBy]
                    ,[LastUpdatedOn]
            FROM [Flood].[FloodSignature]
            WHERE ApplicationId = @p_ApplicationId";


    public GetSignatorySqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
