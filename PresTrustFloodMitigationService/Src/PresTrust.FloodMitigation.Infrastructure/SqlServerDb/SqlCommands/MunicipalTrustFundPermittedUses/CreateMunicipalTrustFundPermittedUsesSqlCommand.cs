namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

    public class CreateMunicipalTrustFundPermittedUsesSqlCommand
    {
	private readonly string _sqlCommand =
             @"INSERT INTO [Flood].[FloodMunicipalTrustFundPermittedUses]
						(
						   [AgencyId]
						  ,[YearOfInception]
						  ,[AcquisitionOfLands]
						  ,[AcquisitionOfFarmLands]
						  ,[DevelopmentOfLands]
						  ,[MaintenanceOfLands]
						  ,[SalariesAndBenefits]
						  ,[BondDownPayments]
						  ,[HistoricPreservation]
						  ,[OpenspaceMasterPlan]
						  ,[OpenspaceMasterPlanDate]
						  ,[GreenAcresGrant]
						  ,[Other]
						  ,[TrustFundComments]
						  ,[LastUpdatedOn]
						)
						VALUES
						(
							 @p_AgencyId
							,@p_YearOfInception
							,@p_AcquisitionOfLands
							,@p_AcquisitionOfFarmLands
							,@p_DevelopmentOfLands
							,@p_MaintenanceOfLands
							,@p_SalariesAndBenefits
							,@p_BondDownPayments 
							,@p_HistoricPreservation
							,@p_OpenspaceMasterPlan
							,@p_OpenspaceMasterPlanDate
							,@p_GreenAcresGrant
							,@p_Other
							,@p_TrustFundComments
							,GETDATE()
						);
 
			SELECT CAST( SCOPE_IDENTITY() AS INT);";

	public CreateMunicipalTrustFundPermittedUsesSqlCommand()
	{
	}

	public override string ToString()
	{
		return _sqlCommand;
	}
}
