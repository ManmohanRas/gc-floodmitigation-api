using PresTrust.FloodMitigation.Application.Commands.CreateApplication;
using PresTrust.FloodMitigation.Application.Commands.DeleteFeedback;
using PresTrust.FloodMitigation.Application.Commands.MarkFeedbacksAsRead;
using PresTrust.FloodMitigation.Application.Commands.SaveFeedback;
using PresTrust.FloodMitigation.Application.CommonViewModels;


namespace PresTrust.FloodMitigation.API.Controllers.v1
{
    [Authorize()]
    [Route("api/v1/flmitig")]
    [ApiController]
    public class FloodMitigationController : ApiBaseController
    {
        public FloodMitigationController(IMediator mediator) : base(mediator) {}

        [HttpPost("saveTest")]
        [ProducesResponseType(typeof(SaveTestCommandViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<SaveTestCommandViewModel>> SaveTest([FromBody] SaveTestCommand command)
        {
            return Single(await CommandAsync(command));
        }

        [HttpPost("getTest")]
        [ProducesResponseType(typeof(GetTestQueryViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<GetTestQueryViewModel>> GetTest([FromBody] GetTestQuery query)
        {
            return Single(await QueryAsync(query));
        }

        [HttpPost("createApplication")]
        [ProducesResponseType(typeof(CreateApplicationCommandViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<CreateApplicationCommandViewModel>> CreateApplication([FromBody] CreateApplicationCommand command)
        {
            return Single(await CommandAsync(command));
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

    }
}
