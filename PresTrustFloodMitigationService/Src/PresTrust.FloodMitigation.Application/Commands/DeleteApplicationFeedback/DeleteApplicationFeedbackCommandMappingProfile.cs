namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteApplicationFeedbackCommandMappingProfile : Profile
{
    public DeleteApplicationFeedbackCommandMappingProfile()
    {
        CreateMap<DeleteApplicationFeedbackCommand, FloodApplicationFeedbackEntity>();
    }
}
