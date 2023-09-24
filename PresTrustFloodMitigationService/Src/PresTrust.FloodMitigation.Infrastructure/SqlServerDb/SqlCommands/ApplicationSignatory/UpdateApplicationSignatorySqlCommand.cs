namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

internal class UpdateApplicationSignatorySqlCommand
{
    private readonly string _sqlCommand =
      @" UPDATE [Flood].[FloodApplicationSignatory]
               SET  ApplicationId = @p_ApplicationId
                   ,Designation   = @p_Designation
                   ,Title         = @p_Title
                   ,SignedOn      = @p_SignedOn
                   ,LastUpdatedBy = @p_LastUpdatedBy
                   ,LastUpdatedOn = @p_LastUpdatedOn
             WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId";

    public UpdateApplicationSignatorySqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
