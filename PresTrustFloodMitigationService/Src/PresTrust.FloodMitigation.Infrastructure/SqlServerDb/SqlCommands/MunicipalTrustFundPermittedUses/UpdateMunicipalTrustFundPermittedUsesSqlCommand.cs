namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateMunicipalTrustFundPermittedUsesSqlCommand
{
    private readonly string _sqlCommand =
      @"UPDATE [Flood].[FloodMunicipalTrustFundPermittedUses]
                     SET   [AgencyId]                   = @p_AgencyId
                          ,[YearOfInception]            = @p_YearOfInception
                          ,[AcquisitionOfLands]         = @p_AcquisitionOfLands
						  ,[AcquisitionOfFarmLands]     = @p_AcquisitionOfFarmLands
						  ,[DevelopmentOfLands]         = @p_DevelopmentOfLands
						  ,[MaintenanceOfLands]         = @p_MaintenanceOfLands
						  ,[SalariesAndBenefits]        = @p_SalariesAndBenefits
						  ,[BondDownPayments]           = @p_BondDownPayments
						  ,[HistoricPreservation]       = @p_HistoricPreservation
						  ,[OpenspaceMasterPlan]        = @p_OpenspaceMasterPlan
						  ,[OpenspaceMasterPlanDate]    = @p_OpenspaceMasterPlanDate
						  ,[GreenAcresGrant]            = @p_GreenAcresGrant
                          ,[Other]                      = @p_Other
						  ,[TrustFundComments]	        = @p_TrustFundComments
             WHERE Id = @p_Id;";

    public UpdateMunicipalTrustFundPermittedUsesSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
