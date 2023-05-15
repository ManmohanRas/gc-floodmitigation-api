namespace PresTrust.FloodMitigation.Application.Commands.CreateApplication
{
    /// <summary>
    /// This class represents api's command input model and returns the response object
    /// </summary>
    public class CreateApplicationCommand : IRequest<CreateApplicationCommandViewModel>
    {
        public int AgencyId { get; set; }
    }
}
