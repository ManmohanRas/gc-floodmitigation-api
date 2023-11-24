namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateParcelTrackingSqlCommand
{
    private readonly string _sqlCommand =
       @"UPDATE [Flood].[FloodParcelTracking] 
        SET 	   [ApplicationId] = @p_ApplicationId
                  ,[PamsPin] = @p_PamsPin
                  ,[ClosingDate] = @p_ClosingDate
                  ,[DeedBook] = @p_DeedBook
                  ,[DeedPage] = @p_DeedPage
                  ,[DeedDate] = @p_DeedDate
                  ,[DemolitionDate] = @p_DemolitionDate
                  ,[SiteVisitConfirmDate] = @p_SiteVisitConfirmDate
                  ,[PublicPark] = @p_PublicPark
                  ,[RainGarden] = @p_RainGarden
                  ,[CommunityGarden] = @p_CommunityGarden
                  ,[ActiveRecreation] = @p_ActiveRecreation
                  ,[NaturalHabitat] = @p_NaturalHabitat
                  ,[LastUpdatedBy] = @p_LastUpdatedBy
			      ,[LastUpdatedOn] = GETDATE()
             WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin;";

    public UpdateParcelTrackingSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
