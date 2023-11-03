namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateParcelTrackingSqlCommand
{
    private readonly string _sqlCommand=
        @"INSERT INTO [Flood].[FloodParcelTracking]
                   ([ApplicationId]
                   ,[PamsPin]
                   ,[ClosingDate]
                   ,[DeedBook]
                   ,[DeedPage]
                   ,[DeedDate]
                   ,[DemolitionDate]
                   ,[SiteVisitConfirmDate]
                   ,[PublicPark]
                   ,[RainGarden]
                   ,[CommunityGarden]
                   ,[ActiveRecreation]
                   ,[NaturalHabitat]
                   ,[LastUpdatedBy]
                   ,[LastUpdatedOn])
             VALUES
                   (@p_ApplicationId
                   ,@p_PamsPin
                   ,@p_ClosingDate
                   ,@p_DeedBook
                   ,@p_DeedPage
                   ,@p_DeedDate
                   ,@p_DemolitionDate
                   ,@p_SiteVisitConfirmDate
                   ,@p_PublicPark
                   ,@p_RainGarden
                   ,@p_CommunityGarden
                   ,@p_ActiveRecreation
                   ,@p_NaturalHabitat
                   ,@p_LastUpdatedBy
                   ,GETDATE());

           SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateParcelTrackingSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
