namespace PresTrust.FloodMitigation.Application
{
    public class BaseHandler
    {
        private IApplicationRepository repoApplication;

        public BaseHandler(IApplicationRepository repoApplication)
        {
            this.repoApplication = repoApplication;
        }
    }
}
