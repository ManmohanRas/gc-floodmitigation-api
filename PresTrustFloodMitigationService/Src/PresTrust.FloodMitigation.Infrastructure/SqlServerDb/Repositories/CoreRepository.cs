namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories
{
    public class CoreRepository : ICoreRepository
    {
        #region " Members ... "

        private readonly PresTrustSqlDbContext context;
        protected readonly SystemParameterConfiguration systemParamConfig;

        #endregion

        #region " ctor ... "

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="systemParamConfigOptions"></param>
        public CoreRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
        {
            this.context = context;
            systemParamConfig = systemParamConfigOptions.Value;
        }

        #endregion

        /// <summary>
        ///  Procedure to fetch grant details by Id.
        /// </summary>
        /// <param name="id"> Id.</param>
        /// <param name="applicationId"> Id.</param>
        /// <returns> Returns Grant Entity.</returns>
        public async Task<IEnumerable<FloodAgencyEntity>> GetAgenciesAsync()
        {
            IEnumerable<FloodAgencyEntity> results = default;
            using var conn = context.CreateConnection();
            var sqlCommand = new GetFloodAgenciesSqlCommand();
            results = await conn.QueryAsync<FloodAgencyEntity>(sqlCommand.ToString(),
                        commandType: CommandType.Text,
                        commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds);

            return results;
        }

        public async Task<FloodAgencyEntity> GetAgencyByIdAsync(int id)
        {
            FloodAgencyEntity result = default;
            using var conn = context.CreateConnection();
            var sqlCommand = new GetFloodAgencyByIdSqlCommand();
            var results = await conn.QueryAsync<FloodAgencyEntity>(sqlCommand.ToString(),
                                commandType: CommandType.Text,
                                commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                                param: new { @p_Id = id });
            result = results.FirstOrDefault();

            return result;
        }

        public async Task<bool> IsValidPamsPinAsync(string pamsPin)
        {
            bool result = false;
            using var conn = context.CreateConnection();
            var sqlCommand = new IsValidPamsPinSqlCommand();
            var count = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
                commandType: CommandType.Text,
                commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                param: new { @p_PamsPin = pamsPin });

            if (count > 0) result = true;

            return result;
        }
    }
}
