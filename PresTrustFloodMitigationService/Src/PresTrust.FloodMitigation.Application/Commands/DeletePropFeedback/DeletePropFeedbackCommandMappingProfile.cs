namespace PresTrust.FloodMitigation.Application.Commands;

public class DeletePropFeedbackCommandMappingProfile : Profile
{
    public DeletePropFeedbackCommandMappingProfile()
    {
        CreateMap<DeleteApplicationFeedbackCommand, FloodPropertyFeedbackEntity>();
    }
}
