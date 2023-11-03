namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetParcelTrackingSqlCommand
{
    private readonly string _sqlCommand =
        @"  SELECT [Id]   
                   ,[ApplicationId]
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
                   ,[LastUpdatedOn]
                   ,[LastUpdatedBy]
                FROM [Flood].[FloodParcelTracking]
                WHERE [ApplicationId] = @p_ApplicationId AND [PamsPin] = @p_PamsPin;";
    public GetParcelTrackingSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
