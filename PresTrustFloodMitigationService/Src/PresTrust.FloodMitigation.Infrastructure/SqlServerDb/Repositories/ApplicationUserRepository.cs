namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories
{
    public class ApplicationUserRepository: IApplicationUserRepository
    {
        private readonly PresTrustSqlDbContext context;
        protected readonly SystemParameterConfiguration systemParamConfig;

        public ApplicationUserRepository
            (
            PresTrustSqlDbContext context,
            IOptions<SystemParameterConfiguration> systemParamConfigOptions
            )
        {
            this.context = context;
            this.systemParamConfig = systemParamConfigOptions.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<FloodApplicationUserEntity>> GetPrimaryContactsAsync(int applicationId)
        {
            IEnumerable<FloodApplicationUserEntity> results = default;

            using var conn = context.CreateConnection();
            var sqlCommand = new GetPrimaryContactsByApplicationIdSqlCommand();
            results = await conn.QueryAsync<FloodApplicationUserEntity>(sqlCommand.ToString(),
                        commandType: CommandType.Text,
                        commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                        param: new { @p_ApplicationId = applicationId });

            return results;
        }
    }
}
