namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands
{
    public class GetTestSqlCommand
    {
        //private readonly string _sqlCommand =
        //    @"SELECT
        //                [Id]
        //               ,[Name]
        //      FROM      [Flood].[FloodTest]
        //      WHERE     [Id] = @p_Id;";
        private readonly string _sqlCommand =
            @"SELECT
                        [Id]
                       ,[PresentName] AS [Name]
              FROM      [Hist].[HistSite]
              WHERE     [Id] = @p_Id;";
        public GetTestSqlCommand() { }

        public override string ToString()
        {
            return _sqlCommand;
        }
    }
}
