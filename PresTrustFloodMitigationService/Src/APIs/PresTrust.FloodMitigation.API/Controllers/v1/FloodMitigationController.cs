using PresTrust.FloodMitigation.Application.Queries;

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

    [HttpPost("getPropComments")]
    [ProducesResponseType(typeof(IEnumerable<GetCommentsQueryViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetPropCommentsQueryViewModel>>> GetPropComments([FromBody] GetPropCommentsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("savePropComment")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SavePropComment([FromBody] SavePropCommentCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("deletePropComment")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> DeletePropComment([FromBody] DeletePropCommentCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("markPropCommentsAsRead")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> MarkPropCommentsAsRead([FromBody] MarkPropFeedbackAsReadCommand command)
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

    [HttpPost("requestForApplicationCorrection")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> RequestForApplicationCorrectionCommand([FromBody] RequestForApplicationCorrectionCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("responseToRequestForApplicationCorrectionCommand")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> ResponseToRequestForApplicationCorrectionCommand([FromBody] ResponseToRequestForApplicationCorrectionCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("getPropFeedbacks")]
    [ProducesResponseType(typeof(IEnumerable<GetPropFeedbacksQueryViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetPropFeedbacksQueryViewModel>>> getPropFeedbacks([FromBody] GetPropFeedbacksQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("savePropFeedback")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SavePropFeedback([FromBody] SavePropFeedbackCommand command)
    {
        return Single(await CommandAsync(command));
    }


    [HttpPost("deletePropFeedback")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> DeletePropFeedback([FromBody] DeletePropFeedbackCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("markPropFeedbacksAsRead")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> MarkPropFeedbacksAsRead([FromBody] MarkPropFeedbackAsReadCommand command)
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

    [HttpPost("responseToRequestForPropertyCorrectionCommand")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> ResponseToRequestForPropertyCorrectionCommand([FromBody] ResponseToRequestForPropertyCommand command)
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
    public async Task<ActionResult<int>> SaveOverviewDetails([FromBody] SaveOverviewDetailsCommand command)
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
    public async Task<ActionResult<int>> SaveApplicationFinance([FromBody] SaveApplicationFinanceCommand  command)
    {
        return Single(await CommandAsync(command));
    }

    /// Save Funding Agency.
    /// </summary>
    /// <param name="command"> Query Command.</param>
    /// <returns> Returns Funding Agency Reponse.</returns>
    [HttpPost("saveFundingAgency")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveFundingAgency([FromBody] SaveFundingAgencyCommand command)
    {
        return Single(await CommandAsync(command));
    }

    [HttpPost("deleteFundingAgency")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<bool>> DeleteFundingAgency([FromBody] DeleteFundingAgencyCommand command)
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

    /// <summary>
    /// Save Parcel Finance.
    /// </summary>
    /// <param name="command"> Query Command.</param>
    /// <returns> Returns Signature Reponse.</returns>
    [HttpPost("saveParcelFinance")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveParcelFinance([FromBody] SaveParcelFinanceCommand command)
    {
        return Single(await CommandAsync(command));
    }
    /// Get Tech Details
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("getTechDetails")]
    [ProducesResponseType(typeof(GetTechDetailsQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetTechDetailsQueryViewModel>> GetTech([FromBody] GetTechDetailsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    /// <summary>
    /// Save Tech Details.
    /// </summary>
    /// <param name="command"> Query Command.</param>
    /// <returns> Returns Signature Reponse.</returns>
    [HttpPost("saveTechDetails")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveTechDetails([FromBody] SaveTechDetailsCommand command)
    {
        return Single(await CommandAsync(command));
    }

    /// Get Tech Details
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("reCalculateParcelFinance")]
    [ProducesResponseType(typeof(ReCalculateParcelFinanceQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ReCalculateParcelFinanceQueryViewModel>> ReCalculateParcelFinance([FromBody] ReCalculateParcelFinanceQuery query)
    {
        return Single(await QueryAsync(query));
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

    [HttpPost("getSoftcostDetails")]
    [ProducesResponseType(typeof(GetSoftcostDetailsQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType ((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetSoftcostDetailsQueryViewModel>> GetSoftcostDetails([FromBody] GetSoftcostDetailsQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("saveSoftcost")]
    [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Unit>> saveSoftcost([FromBody] SaveSoftcostCommand command)
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

    [HttpPost("saveContact")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<int>> SaveContact([FromBody] SaveContactCommand command)
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
}