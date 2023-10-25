using PresTrust.FloodMitigation.Application.Commands;
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

    [HttpPost("getPropComments")]
    [ProducesResponseType(typeof(IEnumerable<GetApplicationCommentsQueryViewModel>), (int)HttpStatusCode.OK)]
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

    [HttpPost("getApplicationOverviewDetails")]
    [ProducesResponseType(typeof(GetApplicationOverviewQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GetApplicationOverviewQueryViewModel>> GetApplicationOverviewDetails([FromBody] GetApplicationOverviewQuery query)
    {
        return Single(await QueryAsync(query));
    }


    [HttpPost("saveOverviewDetails")]
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
    public async Task<ActionResult<int>> SaveApplicationFinance([FromBody] SaveApplicationFinanceCommand  command)
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

    [HttpPost("reCalculateParcelFinance")]
    [ProducesResponseType(typeof(ReCalculateParcelFinanceQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ReCalculateParcelFinanceQueryViewModel>> ReCalculateParcelFinance([FromBody] ReCalculateParcelFinanceQuery query)
    {
        return Single(await QueryAsync(query));
    }

    [HttpPost("getDocumentChecklist")]
    [ProducesResponseType(typeof(IEnumerable<DocumentCheckListSectionViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<DocumentCheckListSectionViewModel>>> GetDocumentChecklist([FromBody] GetDocumentCheckListQuery query)
    {
        return Single(await QueryAsync(query));
    }

    /// <summary>
    /// Save Document Checklist. 
    /// </summary>
    /// <param name="command"> Query Command.</param>
    /// <returns> Returns Document Checklist Reponse.</returns>
    [HttpPost("saveDocumentCheckList")]
    [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Unit>> SaveDocumentCheckList([FromBody] UpdateDocumentCheckListCommand command)
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

    [HttpPost("getSoftcostDetails")]
    [ProducesResponseType(typeof(GetSoftcostDetailsQueryViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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
    public async Task<ActionResult<IEnumerable<PropertyDocumentTypeViewModel>>> GetPropertyDocument([FromBody] GetPropertyDocumentsQuery query)
    {
        return Single(await QueryAsync(query));
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

    //// Property Status Workflow
    ///

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
}
