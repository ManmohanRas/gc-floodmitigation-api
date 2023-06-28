namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

internal class UpdateSignatorySqlCommand
{
    private readonly string _sqlCommand =
      @" UPDATE [Flood].[FloodSignature]
               SET  ApplicationId = @p_ApplicationId
                   ,Designation   = @p_Designation
                   ,Title         = @p_Title
                   ,SignedOn      = @p_SignedOn
                   ,LastUpdatedBy = @p_LastUpdatedBy
                   ,LastUpdatedOn = @p_LastUpdatedOn
             WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId";

    public UpdateSignatorySqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
