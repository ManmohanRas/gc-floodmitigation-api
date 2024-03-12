namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IEmailTemplateRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<FloodEmailTemplateEntity>> GetAllEmailTemplates();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<FloodEmailTemplateEntity> GetEmailTemplate(string emailTemplateCode);

    /// <summary>
    /// Save Email Template.
    /// </summary>
    /// <param name="floodEmailTemplate"></param>
    /// <returns></returns>
    Task<FloodEmailTemplateEntity> SaveAsync(FloodEmailTemplateEntity floodEmailTemplate);
}
