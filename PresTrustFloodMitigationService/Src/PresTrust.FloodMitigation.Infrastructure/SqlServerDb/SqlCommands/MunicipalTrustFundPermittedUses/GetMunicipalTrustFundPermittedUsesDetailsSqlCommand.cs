namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetMunicipalTrustFundPermittedUsesDetailsSqlCommand
{
    private readonly string _sqlCommand =
        @"SELECT [Id]
                      ,[AgencyId]
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
             FROM   [Flood].[FloodMunicipalTrustFundPermittedUses]
             WHERE	AgencyId = @p_AgencyId;";


    public GetMunicipalTrustFundPermittedUsesDetailsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
