using PresTrust.FloodMitigation.Application.ApiExceptions;

namespace PresTrust.FloodMitigation.Application
{
    public class BaseHandler
    {
        private readonly IApplicationRepository repoApplication;

        BaseHandler(IApplicationRepository repoApplication)
        {
            this.repoApplication = repoApplication;
        }

        /// <summary>
        /// Get Application for a given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FlmitigApplicationEntity> GetIfApplicationExists(int id)
        {
            var application = await repoApplication.GetApplicationAsync(id);

            if (application == null)
                throw new EntityNotFoundException($"Application (Id: {id}) does not exist or invalid");

            return application;
        }
    }
}
