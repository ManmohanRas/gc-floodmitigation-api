namespace PresTrust.FloodMitigation.Application.Commands;

public class DeletePropertyFeedbackCommandMappingProfile : Profile
{
    public DeletePropertyFeedbackCommandMappingProfile()
    {
        CreateMap<DeletePropertyFeedbackCommand, FloodPropertyFeedbackEntity>();
    }
}
