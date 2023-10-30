namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateFinanceLineItemSqlCommand
{
    private readonly string _sqlCommand =
            @" UPDATE [Flood].[FloodApplicationFinanceLineItems]
                   SET [ValueEstimate] = @p_ValueEstimate
                      ,[LastUpdatedBy] = @p_LastUpdatedBy
                      ,[LastUpdatedOn] = @p_LastUpdatedOn
                WHERE  Id = @p_Id AND PamsPin = @p_PamsPin;";

    public UpdateFinanceLineItemSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
