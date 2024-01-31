namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;
public class CreateParcelHistorySqlCommand
{
    private readonly string _sqlCommand =
                    @"  INSERT INTO [Flood].[FloodParcelHistory]
						(
							ParcelId,
							CurrentPamsPin,
							PreviousPamsPin,
							Section,
							Acres,
							AcresToBeAcquired,
							Partial,
							InterestType,
							IsThisAnExclusionArea,
							Notes,
							EasementId,
							IsActive,
							ChangeType,
							ChangeDate,
							ReasonForChange,
							LastUpdatedBy,
							LastUpdatedOn
						)
						VALUES
						(
							@p_ParcelId,
							@p_CurrentPamsPin,
							@p_PreviousPamsPin,
							@p_Section,
							@p_Acres,
							@p_AcresToBeAcquired,
							@p_Partial,
							@p_InterestType,
							@p_IsThisAnExclusionArea,
							@p_Notes,
							@p_EasementId,
							@p_IsActive,
							@p_ChangeType,
							@p_ChangeDate,
							@p_ReasonForChange,
							@p_LastUpdatedBy,
							GetDate()
						)

						SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateParcelHistorySqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}