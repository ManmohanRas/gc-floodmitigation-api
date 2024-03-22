namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class LinkTargetAreaIdToParcelSqlCommand
{
    private readonly string _sqlCommand =
            @"  UPDATE [Flood].[FloodParcel] SET
					[TargetAreaId] = @p_TargetAreaId,
                    [DateOfFLAP] = GETDATE(),
				WHERE [PamsPin] = @p_PamsPin";

    public LinkTargetAreaIdToParcelSqlCommand(bool isUpdate = false)
    {
        if (isUpdate)
        {
            _sqlCommand = 
            @"  UPDATE [Flood].[FloodParcel] SET
					[TargetAreaId] = @p_TargetAreaId,
                    [StreetNo] = @p_StreetNo,
                    [StreetAddress] = @p_StreetAddress,
                    [OwnersName]  = @p_OwnersName,
                    [DateOfFLAP] = GETDATE(),
                    [IsElevated] = @p_IsElevated
				WHERE [PamsPin] = @p_PamsPin";
        }else
        {
            _sqlCommand =
            @"  UPDATE [Flood].[FloodParcel] SET
					[TargetAreaId] = @p_TargetAreaId,
                    [DateOfFLAP] = GETDATE()
				WHERE [PamsPin] = @p_PamsPin";
        }
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
