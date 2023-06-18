namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands
{
    public class SaveTestSqlCommand
    {
        private readonly string _sqlCommand =
            @"INSERT INTO [Flood].[FloodTest]
                       ([Id]
                       ,[Name])
              VALUES
                       (@p_Id
                       ,@p_Name);";
        public SaveTestSqlCommand() { }

        public override string ToString()
        {
            return _sqlCommand;
        }
    }
}
