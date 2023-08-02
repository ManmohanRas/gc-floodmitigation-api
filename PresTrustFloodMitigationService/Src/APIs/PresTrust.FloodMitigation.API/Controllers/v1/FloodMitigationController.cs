namespace PresTrust.FloodMitigation.API.Controllers.v1;

[Authorize()]
[Route("api/v1/flood")]
[ApiController]
public class FloodMitigationController : ApiBaseController
{
    public FloodMitigationController(IMediator mediator) : base(mediator) { }

    [HttpPost("createApplication")]
    [ProducesResponseType(typeof(CreateApplicationCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<CreateApplicationCommandViewModel>> CreateApplication([FromBody] CreateApplicationCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getApplications")]
    [ProducesResponseType(typeof(IEnumerable<GetApplicationsQueryViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetApplicationsQueryViewModel>>> GetApplications([FromBody] GetApplicationsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("getApplicationDetails")]
    [ProducesResponseType(typeof(GetApplicationDetailsQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetApplicationDetailsQueryViewModel>> GetApplicationDetails([FromBody] GetApplicationDetailsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("getFloodParcelsByFilter")]
    [ProducesResponseType(typeof(IEnumerable<GetFloodParcelsByFilterQueryViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetFloodParcelsByFilterQueryViewModel>>> GetFloodParcelsByFilter([FromBody] GetFloodParcelsByFilterQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("getApplicationProperties")]
    [ProducesResponseType(typeof(IEnumerable<GetApplicationPropertiesQueryViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetApplicationPropertiesQueryViewModel>>> GetApplicationProperties([FromBody] GetApplicationPropertiesQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("getApplicationUsers")]
    [ProducesResponseType(typeof(IEnumerable<FloodApplicationUserViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<FloodApplicationUserViewModel>>> GetApplicationUsers([FromBody] GetApplicationUsersQuery query)
    {
        return Single(await QueryAsync(query));
    }

    /// <summary>
    /// Assign Application Users like Primary Contact, Applicant Contractor
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("assignApplicationUsers")]
    [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Unit>> AssignApplicationUsers([FromBody] AssignApplicationUsersCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("saveDeclaration")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> SaveDeclaration([FromBody] SaveDeclarationCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getComments")]
    [ProducesResponseType(typeof(IEnumerable<GetCommentsQueryViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetCommentsQueryViewModel>>> GetComments([FromBody] GetCommentsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveComment")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveComment([FromBody] SaveCommentCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("deleteComment")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> DeleteComment([FromBody] DeleteCommentCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("markCommentsAsRead")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> MarkCommentsAsRead([FromBody] MarkCommentsAsReadCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getFeedbacks")]
    [ProducesResponseType(typeof(IEnumerable<GetFeedbacksQueryViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetFeedbacksQueryViewModel>>> getFeedbacks([FromBody] GetFeedbacksQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveFeedback")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveFeedback([FromBody] SaveFeedbackCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("deleteFeedback")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> DeleteFeedback([FromBody] DeleteFeedbackCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("markFeedbacksAsRead")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> MarkFeedbacksAsRead([FromBody] MarkFeedbacksAsReadCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getOverviewDetails")]
    [ProducesResponseType(typeof(GetOverviewDetailsQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]

    public async Task<ActionResult<GetOverviewDetailsQueryViewModel>> GetOverviewDetails([FromBody] GetOverviewDetailsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveOverviewDetails")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> saveOverviewDetails([FromBody] SaveOverviewDetailsCommand command)
    {
        return Single(await CommandAsync(command));
    }  

    [HttpPost("getDocumentDetails")]
    [ProducesResponseType(typeof(IEnumerable<DocumentTypeViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<DocumentTypeViewModel>>> GetDocumentDetails([FromBody] GetDocumentsBySectionDetailsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveDocument")]
    [ProducesResponseType(typeof(SaveDocumentDetailsCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<SaveDocumentDetailsCommandViewModel>> SaveDocument([FromBody] SaveDocumentDetailsCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("deleteDocument")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> DeleteDocument([FromBody] DeleteDocumentCommand command)
    {
        return Single(await CommandAsync(command));
    }

    /// <summary>
    /// Get Signature Details
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("getSignatoryDetails")]
    [ProducesResponseType(typeof(GetSignatoryQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetSignatoryQueryViewModel>> GetSignatoryDetails([FromBody] GetSignatoryQuery query)
    {
        return Single(await QueryAsync(query));
    }

    /// <summary>
    /// Save Signature Details.
    /// </summary>
    /// <param name="command"> Query Command.</param>
    /// <returns> Returns Signature Reponse.</returns>
    [HttpPost("saveSignatoryDetails")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveSignatoryDetails([FromBody] SaveSignatoryCommand command)
    {
        return Single(await CommandAsync(command));
    }
     
}
