namespace PresTrust.FloodMitigation.Application;

public class BaseHandler
{
    private IApplicationRepository repoApplication;

    public BaseHandler(IApplicationRepository repoApplication)
    {
        this.repoApplication = repoApplication;
    }

    /// <summary>
    /// Get Application for a given Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<FloodApplicationEntity> GetIfApplicationExists(int id)
    {
        var application = await repoApplication.GetApplicationAsync(id);

        if (application == null)
            throw new EntityNotFoundException($"Application (Id: {id}) does not exist or invalid");

        return application;
    }
}