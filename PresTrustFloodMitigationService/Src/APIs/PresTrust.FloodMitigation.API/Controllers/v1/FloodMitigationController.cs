using PresTrust.FloodMitigation.Application.Commands.CreateApplication;
using PresTrust.FloodMitigation.Application.CommonViewModels;

namespace PresTrust.FloodMitigation.API.Controllers.v1
{
    [Authorize()]
    [Route("api/v1/flood")]
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
    }
}
