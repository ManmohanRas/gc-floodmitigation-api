namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands
{
    public class SaveTestSqlCommand
    {
        private readonly string _sqlCommand =
            @"INSERT INTO [Flmitig].[FlmitigTest]
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
