namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IEmailTemplateRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<FloodEmailTemplateEntity> GetEmailTemplate(string emailTemplateCode);
}
