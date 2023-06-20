namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteFeedbackCommandMappingProfile : Profile
{
    public DeleteFeedbackCommandMappingProfile()
    {
        CreateMap<DeleteFeedbackCommand, FlmitigFeedbackEntity>();
    }
}
