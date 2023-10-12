namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    public ContactRepository
        (
        PresTrustSqlDbContext context,
        IOptions<SystemParameterConfiguration> systemParamConfigOptions
        )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }

    public async Task<IEnumerable<FloodContactEntity>> GetAllContactsAsync(int applicationId)
    {
        IEnumerable<FloodContactEntity> results;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetContactsSqlCommand();
        results = await conn.QueryAsync<FloodContactEntity>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_ApplicationId = applicationId });
        return results;
    }
    public async Task<FloodContactEntity> SaveAsync(FloodContactEntity contact)
    {
        if (contact.Id > 0)
            return await UpdateAsync(contact);
        else
            return await CreateAsync(contact);
    }

    private async Task<FloodContactEntity> UpdateAsync(FloodContactEntity contact)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateContactSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = contact.Id,
                @p_ApplicationId = contact.ApplicationId,
                @p_ContactName = contact.ContactName,
                @p_Agency = contact.Agency,
                @p_Email = contact.Email,
                @p_MainNumber = contact.MainNumber,
                @p_AlternateNumber = contact.AlternateNumber,
                @p_SelContact = contact.SelectContact,
                @p_LastUpdatedBy = contact.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return contact;
    }

    private async Task<FloodContactEntity> CreateAsync(FloodContactEntity contact)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateContactSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = contact.ApplicationId,
                @p_ContactName = contact.ContactName,
                @p_Agency = contact.Agency,
                @p_Email = contact.Email,
                @p_MainNumber = contact.MainNumber,
                @p_AlternateNumber = contact.AlternateNumber,
                @p_SelContact = contact.SelectContact,
                @p_LastUpdatedBy = contact.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        contact.Id = id;

        return contact;
    }

    public async Task DeleteContactAsync(FloodContactEntity contact)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new DeleteContactSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = contact.Id,
                @p_ApplicationId = contact.ApplicationId
            });
    }
}
