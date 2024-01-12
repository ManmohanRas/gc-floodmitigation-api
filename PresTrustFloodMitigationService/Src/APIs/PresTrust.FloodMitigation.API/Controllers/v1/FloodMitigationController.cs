using PresTrust.FloodMitigation.Domain.Entities;

namespace PresTrust.FloodMitigation.API.Controllers.v1;

[Authorize()]
[Route("api/v1/flood")]
[ApiController]
public class FloodMitigationController : FloodMitigationWorkflowController
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

    [HttpPost("getPropertyList")]
    [ProducesResponseType(typeof(IEnumerable<GetParcelListQueryViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetParcelListQueryViewModel>>> GetPropertyList([FromBody] GetParcelListQuery query)
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

    [HttpPost("getApplicationComments")]
    [ProducesResponseType(typeof(IEnumerable<GetApplicationCommentsQueryViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetApplicationCommentsQueryViewModel>>> GetApplicationComments([FromBody] GetApplicationCommentsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveApplicationComment")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveApplicationComment([FromBody] SaveApplicationCommentCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("deleteApplicationComment")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> DeleteApplicationComment([FromBody] DeleteApplicationCommentCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("markApplicationCommentsAsRead")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> MarkApplicationCommentsAsRead([FromBody] MarkApplicationCommentsAsReadCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getPropertyComments")]
    [ProducesResponseType(typeof(IEnumerable<GetApplicationCommentsQueryViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetPropertyCommentsQueryViewModel>>> GetPropertyComments([FromBody] GetPropertyCommentsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("savePropertyComment")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SavePropertyComment([FromBody] SavePropertyCommentCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("deletePropertyComment")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> DeletePropertyComment([FromBody] DeletePropertyCommentCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("markPropertyCommentsAsRead")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> MarkPropertyCommentsAsRead([FromBody] MarkPropertyFeedbackAsReadCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getApplicationFeedbacks")]
    [ProducesResponseType(typeof(IEnumerable<GetApplicationFeedbacksQueryViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetApplicationFeedbacksQueryViewModel>>> getApplicationFeedbacks([FromBody] GetApplicationFeedbacksQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveApplicationFeedback")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveApplicationFeedback([FromBody] SaveApplicationFeedbackCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("deleteApplicationFeedback")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> DeleteApplicationFeedback([FromBody] DeleteApplicationFeedbackCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("markApplicationFeedbacksAsRead")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> MarkApplicationFeedbacksAsRead([FromBody] MarkApplicationFeedbacksAsReadCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("requestForApplicationCorrection")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> RequestForApplicationCorrectionCommand([FromBody] RequestForApplicationCorrectionCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("responseToRequestForApplicationCorrection")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> ResponseToRequestForApplicationCorrectionCommand([FromBody] ResponseToRequestForApplicationCorrectionCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getPropertyFeedbacks")]
    [ProducesResponseType(typeof(IEnumerable<GetPropertyFeedbacksQueryViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetPropertyFeedbacksQueryViewModel>>> getPropertyFeedbacks([FromBody] GetPropertyFeedbacksQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("savePropertyFeedback")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SavePropertyFeedback([FromBody] SavePropertyFeedbackCommand command)
    {
        return Single(await CommandAsync(command));
    }


    [HttpPost("deletePropertyFeedback")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> DeletePropertyFeedback([FromBody] DeletePropertyFeedbackCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("markPropertyFeedbacksAsRead")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> MarkPropertyFeedbacksAsRead([FromBody] MarkPropertyFeedbackAsReadCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("requestForPropertyCorrection")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> RequestForPropertyCorrectionCommand([FromBody] RequestForPropertyCorrectionCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("responseToRequestForPropertyCorrection")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> ResponseToRequestForPropertyCorrectionCommand([FromBody] ResponseToRequestForPropertyCorrectionCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getApplicationOverviewDetails")]
    [ProducesResponseType(typeof(GetApplicationOverviewQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetApplicationOverviewQueryViewModel>> GetApplicationOverviewDetails([FromBody] GetApplicationOverviewQuery query)
    {
        return Single(await QueryAsync(query));
    }


    [HttpPost("saveApplicationOverviewDetails")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveOverviewDetails([FromBody] SaveApplicationOverviewCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("saveProjectArea")]
    [ProducesResponseType(typeof(SaveProjectAreaCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<SaveProjectAreaCommandViewModel>> SaveProjectArea([FromBody] SaveProjectAreaCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getApplicationDocuments")]
    [ProducesResponseType(typeof(IEnumerable<ApplicationDocumentTypeViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<ApplicationDocumentTypeViewModel>>> GetApplicationDocuments([FromBody] GetApplicationDocumentsBySectionQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveApplicationDocument")]
    [ProducesResponseType(typeof(SaveApplicationDocumentCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<SaveApplicationDocumentCommandViewModel>> SaveApplicationDocument([FromBody] SaveApplicationDocumentCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("deleteApplicationDocument")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> DeleteApplicationDocument([FromBody] DeleteApplicationDocumentCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getApplicationSignatoryDetails")]
    [ProducesResponseType(typeof(GetApplicationSignatoryQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetApplicationSignatoryQueryViewModel>> GetApplicationSignatoryDetails([FromBody] GetApplicationSignatoryQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveApplicationSignatoryDetails")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveApplicationSignatoryDetails([FromBody] SaveApplicationSignatoryCommand command)
    {
        return Single(await CommandAsync(command));
    }

    /// <summary>
    /// Get Parcel Finance
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("getParcelFinance")]
    [ProducesResponseType(typeof(GetParcelFinanceQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetParcelFinanceQueryViewModel>> GetParcelFinance([FromBody] GetParcelFinanceQuery query)
    {
        return Single(await QueryAsync(query));
    }
    /// Get Finance Details
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("getApplicationFinanceDetails")]
    [ProducesResponseType(typeof(GetApplicationFinanceDetailsQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetApplicationFinanceDetailsQueryViewModel>> GetApplicationFinnceDetails([FromBody] GetApplicationFinanceDetailsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveApplicationFinance")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveApplicationFinance([FromBody] SaveApplicationFinanceCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("saveApplicationFundingAgency")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveApplicationFundingAgency([FromBody] SaveApplicationFundingAgencyCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("deleteApplicationFundingAgency")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> DeleteApplicationFundingAgency([FromBody] DeleteApplicationFundingAgencyCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("deleteFundingSource")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> DeleteFundingSource([FromBody] DeleteFundingSourceCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("ReCalculateFinanceLineItems")]
    [ProducesResponseType(typeof(ReCalculateFinanceLineItemsQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ReCalculateFinanceLineItemsQueryViewModel>> ReCalculateFinanceLineItems([FromBody] ReCalculateFinanceLineItemsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveParcelFinance")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveParcelFinance([FromBody] SaveParcelFinanceCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getTechDetails")]
    [ProducesResponseType(typeof(GetTechDetailsQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetTechDetailsQueryViewModel>> GetTech([FromBody] GetTechDetailsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveTechDetails")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveTechDetails([FromBody] SaveTechDetailsCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getParcelReleaseOfFunds")]
    [ProducesResponseType(typeof(GetPropReleaseOfFundsQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetPropReleaseOfFundsQueryViewModel>> GetReleaseOfFunds([FromBody] GetPropReleaseOfFundsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveParcelReleaseOfFunds")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveReleaseOfFunds([FromBody] SavePropReleaseOfFundsCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getApplicationAdminDetails")]
    [ProducesResponseType(typeof(GetApplicationAdminDetailsQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetApplicationAdminDetailsQueryViewModel>> GetApplicationAdminDetails([FromBody] GetApplicationAdminDetailsQuery query)
    {
        return Single(await QueryAsync(query));
    }


    [HttpPost("saveApplicationAdminDetails")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveApplicationAdminDetails([FromBody] SaveApplicationAdminDetailsCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getPropertyAdminDetails")]
    [ProducesResponseType(typeof(GetPropertyAdminDetailsQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetPropertyAdminDetailsQueryViewModel>> GetPropertyAdminDetails([FromBody] GetPropertyAdminDetailsQuery query)
    {
        return Single(await QueryAsync(query));
    }


    [HttpPost("savePropertyAdminDetails")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SavePropertyAdminDetails([FromBody] SavePropertyAdminDetailsCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("reCalculateParcelFinance")]
    [ProducesResponseType(typeof(ReCalculateParcelFinanceQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ReCalculateParcelFinanceQueryViewModel>> ReCalculateParcelFinance([FromBody] ReCalculateParcelFinanceQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("getApplicationDocumentChecklist")]
    [ProducesResponseType(typeof(IEnumerable<ApplicationDocumentChecklistSectionViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<ApplicationDocumentChecklistSectionViewModel>>> GetApplicationDocumentChecklist([FromBody] GetApplicationDocumentChecklistQuery query)
    {
        return Single(await QueryAsync(query));
    }

    /// <summary>
    /// Save Document Checklist. 
    /// </summary>
    /// <param name="command"> Query Command.</param>
    /// <returns> Returns Document Checklist Reponse.</returns>
    [HttpPost("saveApplicationDocumentChecklist")]
    [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Unit>> SaveApplicationDocumentChecklist([FromBody] SaveApplicationDocumentChecklistCommand command)
    {
        return Single(await CommandAsync(command));
    }

    /// Get Broken rules
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("getBrokenRules")]
    [ProducesResponseType(typeof(IEnumerable<GetBrokenRulesQueryViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetBrokenRulesQueryViewModel>>> GetBrokenRules([FromBody] GetBrokenRulesQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("getSoftCostDetails")]
    [ProducesResponseType(typeof(GetSoftCostDetailsQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetSoftCostDetailsQueryViewModel>> GetSoftCostDetails([FromBody] GetSoftCostDetailsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveSoftCost")]
    [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Unit>> saveSoftCost([FromBody] SaveSoftCostCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("deleteParcelSoftCost")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> DeleteParcelSoftCost([FromBody] DeleteParcelSoftCostCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getContacts")]
    [ProducesResponseType(typeof(IEnumerable<GetContactsQueryViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetContactsQueryViewModel>>> GetContacts([FromBody] GetContactsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveContacts")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> SaveContacts([FromBody] SaveContactsCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("deleteContact")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> DeleteContact([FromBody] DeleteContactCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getPropertyDetails")]
    [ProducesResponseType(typeof(GetPropertyDetailsQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetPropertyDetailsQueryViewModel>> GetPropertyDetails([FromBody] GetPropertyDetailsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("getReleaseOfFunds")]
    [ProducesResponseType(typeof(GetApplicationReleaseOfFundsQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetApplicationReleaseOfFundsQueryViewModel>> GetReleaseOfFunds([FromBody] GetApplicationReleaseOfFundsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveReleaseOfFunds")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveReleaseOfFunds([FromBody] SaveApplicationReleaseOfFundsCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getPropertyDocument")]
    [ProducesResponseType(typeof(IEnumerable<PropertyDocumentTypeViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<PropertyDocumentTypeViewModel>>> GetPropertyDocument([FromBody] GetPropertyDocumentQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("getParcelSurvey")]
    [ProducesResponseType(typeof(GetParcelSurveyQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetParcelSurveyQueryViewModel>> GetParcelSurvey([FromBody] GetParcelSurveyQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveParcelSurvey")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveParcelSurvey([FromBody] SaveParcelSurveyCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getParcelTracking")]
    [ProducesResponseType(typeof(GetParcelTrackingQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetParcelTrackingQueryViewModel>> GetParcelTracking([FromBody] GetParcelTrackingQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveParcelTracking")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveParcelTracking([FromBody] SaveParcelTrackingCommand command)
    {
        return Single(await CommandAsync(command));
    } 

    [HttpPost("savePropertyDocument")]
    [ProducesResponseType(typeof(SavePropertyDocumentDetailsCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<SavePropertyDocumentDetailsCommandViewModel>> SavePropertyDocument([FromBody] SavePropertyDocumentDetailsCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("deletePropertyDocument")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> DeletePropertyDocument([FromBody] DeletePropertyDocumentCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getParcelProperty")]
    [ProducesResponseType(typeof(GetParcelPropertyQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetParcelPropertyQueryViewModel>> GetParcelProperty([FromBody] GetParcelPropertyQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveParcelProperty")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveParcelProperty([FromBody] SaveParcelPropertyCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("releaseApplicationPayments")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> ReleaseApplicationPayments([FromBody] ReleasePaymentsCommand command)
    {
        return Single(await CommandAsync(command));
    }

    /// <summary>
    /// Get Property Document Checklist
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("getPropertyDocumentChecklist")]
    [ProducesResponseType(typeof(IEnumerable<PropertyDocumentChecklistSectionViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<PropertyDocumentChecklistSectionViewModel>>> GetPropertyDocumentChecklist([FromBody] GetPropertyDocumentChecklistQuery query)
    {
        return Single(await QueryAsync(query));
    }

    /// <summary>
    /// Save Property Document Checklist. 
    /// </summary>
    /// <param name="command"> Query Command.</param>
    /// <returns> Returns Document Checklist Reponse.</returns>
    [HttpPost("savePropertyDocumentChecklist")]
    [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Unit>> SavePropertyDocumentChecklistAsync([FromBody] SavePropertyDocumentChecklistCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getApplicationStatusLog")]
    [ProducesResponseType(typeof(IEnumerable<GetApplicationStatusLogQueryViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetApplicationStatusLogQueryViewModel>>> GetApplicationStatusLog([FromBody] GetApplicationStatusLogQuery query)
    {
        return Single(await QueryAsync(query));
    }
    [HttpPost("getPropertyStatusLog")]
    [ProducesResponseType(typeof(IEnumerable<GetPropertyStatusLogQueryViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetPropertyStatusLogQueryViewModel>>> GetPropertyStatusLog([FromBody] GetPropertyStatusLogQuery query)
    {
        return Single(await QueryAsync(query));
    }
    /// Get PropertyBroken rules
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("getPropertyBrokenRules")]
    [ProducesResponseType(typeof(IEnumerable<GetPropertyBrokenRulesQueryViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetPropertyBrokenRulesQueryViewModel>>> GetPropertyBrokenRules([FromBody] GetPropertyBrokenRulesQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("submitApproveParcelSoftCostStatus")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> SubmitApproveParcelSoftCostStatus([FromBody] SubmitApproveParcelSoftCostStatusCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getFlapDetails")]
    [ProducesResponseType(typeof(GetFlapDetailsQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetFlapDetailsQueryViewModel>> GetFlapDetails([FromBody] GetFlapDetailsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveFlapDetails")]
    [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Unit>> SaveFlapDetails([FromBody] SaveFlapDetailsCommand command)
    {
        return Single(await CommandAsync(command));
    }

    /// <summary>
    /// Get County Users 
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("getCountyUsers")]
    [ProducesResponseType(typeof(IEnumerable<PresTrustUserEntity>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<PresTrustUserEntity>>> GetCountyUsers([FromBody] GetCountyUsersQuery query)
    {
        return Single(await QueryAsync(query));
    }
    /// <summary>
    /// Delete County User Role.
    /// </summary>
    /// <param name="command"> Query Command.</param>
    /// <returns></returns>
    /// 
    [HttpPost("deleteCountyUserRole")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> DeleteCountyUserRole([FromBody] DeleteCountyUserRoleCommand command)
    {
        return Single(await CommandAsync(command));
    }
    /// <summary>
    /// County User Role Change Request.
    /// </summary>
    /// <param name="command"> Query Command.</param>
    /// <returns></returns>
    /// 
    [HttpPost("countyUserRoleChangeRequest")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> CountyUserRoleChangeRequest([FromBody] CountyUserRoleChangeRequestCommand command)
    {
        return Single(await CommandAsync(command));
    }

    /// <summary>
    /// Save flap document
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("saveFlapDocument")]
    [ProducesResponseType(typeof(SaveFlapDocumentCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<SaveFlapDocumentCommandViewModel>> SaveFlapDocument([FromBody] SaveFlapDocumentCommand command)
    {
        return Single(await CommandAsync(command));
    }

    /// <summary>
    /// Delete flap document
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("deleteFlapDocument")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> DeleteFlapDocument([FromBody] DeleteFlapDocumentCommand command)
    {
        return Single(await CommandAsync(command));
    }

    /// Get Agency Users 
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("getAgencyUsers")]
    [ProducesResponseType(typeof(IEnumerable<PresTrustUserEntity>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<PresTrustUserEntity>>> GetAgencyUsers([FromBody] GetAgencyUsersQuery query)
    {
        return Single(await QueryAsync(query));
    }

    /// <summary>
    /// Delete Agency User Role.
    /// </summary>
    /// <param name="command"> Query Command.</param>
    /// <returns></returns>
    /// 
    [HttpPost("deleteAgencyUserRole")]
    public async Task<ActionResult<bool>> DeleteAgencyUserRole([FromBody] DeleteAgencyUserRoleCommand command)
    {
        return Single(await CommandAsync(command));
    }

    /// <summary>
    /// Agency User Role Change Request.
    /// </summary>
    /// <param name="command"> Query Command.</param>
    /// <returns></returns>
    /// 
    [HttpPost("agencyUserRoleChangeRequest")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> AgencyUserRoleChangeRequest([FromBody] AgencyUserRoleChangeRequestCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("saveFlapTargetArea")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveFlapTargetArea([FromBody] SaveFlapTargetAreaCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getFlapTargetAreaDetails")]
    [ProducesResponseType(typeof(GetTargetAreaDetailsQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetTargetAreaDetailsQueryViewModel>> GetFlapTargetAreaDetails([FromBody] GetTargetAreaDetailsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    /// <summary>
    /// Get Program Manager Parcels
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("getProgramManagerParcels")]
    [ProducesResponseType(typeof(GetProgramManagerParcelsQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetProgramManagerParcelsQueryViewModel>> GetProgramManagerParcels([FromBody] GetProgramManagerParcelsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    /// <summary>
    /// Save Program Manager Parcel
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("saveProgramManagerParcel")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveProgramManagerParcel([FromBody] SaveProgramManagerParcelCommand query)
    {
        return Single(await QueryAsync(query));
    }

    //Municipal comments
    [HttpPost("getMunicipalComments")]
    [ProducesResponseType(typeof(IEnumerable<GetMunicipalCommentsQueryViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetMunicipalCommentsQueryViewModel>>> GetMunicipalComments([FromBody] GetMunicipalCommentsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveMunicipalComment")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveMunicipalComment([FromBody] SaveMunicipalCommentCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("deleteMunicipalComment")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> DeleteMunicipalComment([FromBody] DeleteMunicipalCommentCommand command)
    {
        return Single(await CommandAsync(command));
    }
}

public class FloodMitigationWorkflowController : ApiBaseController
{
    public FloodMitigationWorkflowController(IMediator mediator) : base(mediator) { }

    [HttpPost("submitDeclaration")]
    [ProducesResponseType(typeof(SubmitDeclarationCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<SubmitDeclarationCommandViewModel>> SubmitDeclaration([FromBody] SubmitDeclarationCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("approveDeclaration")]
    [ProducesResponseType(typeof(ApproveDeclarationCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ApproveDeclarationCommandViewModel>> ApproveDeclaration([FromBody] ApproveDeclarationCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("submitApplication")]
    [ProducesResponseType(typeof(SubmitApplicationCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<SubmitApplicationCommandViewModel>> SubmitApplication([FromBody] SubmitApplicationCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("reviewApplication")]
    [ProducesResponseType(typeof(ReviewApplicationCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ReviewApplicationCommandViewModel>> ReviewApplication([FromBody] ReviewApplicationCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("activateApplication")]
    [ProducesResponseType(typeof(ActivateApplicationCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ActivateApplicationCommandViewModel>> ActivateApplication([FromBody] ActivateApplicationCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("closeApplication")]
    [ProducesResponseType(typeof(CloseApplicationCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<CloseApplicationCommandViewModel>> CloseApplication([FromBody] CloseApplicationCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("rejectApplication")]
    [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Unit>> RejectApplication([FromBody] RejectApplicationCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("withdrawApplication")]
    [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Unit>> WithdrawApplication([FromBody] WithdrawApplicationCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("reinitiateApplication")]
    [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Unit>> ReinitiateApplication([FromBody] ReinitiateApplicationCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("reviewProperty")]
    [ProducesResponseType(typeof(ReviewPropertyCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ReviewPropertyCommandViewModel>> ReviewProperty([FromBody] ReviewPropertyCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("pendingProperty")]
    [ProducesResponseType(typeof(PendingPropertyCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<PendingPropertyCommandViewModel>> PendingProperty([FromBody] PendingPropertyCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("rejectProperty")]
    [ProducesResponseType(typeof(RejectPropertyCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<RejectPropertyCommandViewModel>> RejectProperty([FromBody] RejectPropertyCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("withdrawProperty")]
    [ProducesResponseType(typeof(WithdrawPropertyCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<WithdrawPropertyCommandViewModel>> WithdrawProperty([FromBody] WithdrawPropertyCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("approveProperty")]
    [ProducesResponseType(typeof(ApprovePropertyCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ApprovePropertyCommandViewModel>> ApproveProperty([FromBody] ApprovePropertyCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("projectAreaExpireProperty")]
    [ProducesResponseType(typeof(ProjectAreaExpirePropertyCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ProjectAreaExpirePropertyCommandViewModel>> ProjectAreaExpireProperty([FromBody] ProjectAreaExpirePropertyCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("grantExpireProperty")]
    [ProducesResponseType(typeof(GrantExpirePropertyCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GrantExpirePropertyCommandViewModel>> GrantExpireProperty([FromBody] GrantExpirePropertyCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("preserveProperty")]
    [ProducesResponseType(typeof(PreservePropertyCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<PreservePropertyCommandViewModel>> PreserveProperty([FromBody] PreservePropertyCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("submitProperty")]
    [ProducesResponseType(typeof(ReviewPropertyCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<SubmitPropertyCommandViewModel>> SubmitProperty([FromBody] SubmitPropertyCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("transferProperty")]
    [ProducesResponseType(typeof(TransferPropertyCommandViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<TransferPropertyCommandViewModel>> TransferProperty([FromBody] TransferPropertyCommand command)
    {
        return Single(await CommandAsync(command));
    }
}


