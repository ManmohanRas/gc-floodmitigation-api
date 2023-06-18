namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly PresTrustSqlDbContext context;
        protected readonly SystemParameterConfiguration systemParamConfig;

        public TestRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfig)
        {
            this.context = context;
            this.systemParamConfig = systemParamConfig.Value;
        }

        public async Task<FloodTestEntity> GetTestAsync(int id)
        {
            FloodTestEntity result = default;
            using var conn = context.CreateConnection();
            var sqlCommand = new GetTestSqlCommand();
            var results = await conn.QueryAsync<FloodTestEntity>(sqlCommand.ToString(),
                                commandType: CommandType.Text,
                                commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                                param: new { @p_Id = id });
            result = results.FirstOrDefault();

            return result;
        }

        public async Task<int> SaveTestAsync(FloodTestEntity test)
        {
            int id = default;

            using var conn = context.CreateConnection();
            var sqlCommand = new SaveTestSqlCommand();
            id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
                commandType: CommandType.Text,
                commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                param: new
                {
                    @p_Id = test.Id,
                    @p_Name = test.Name
                });

            return id;
        }
    }
}
