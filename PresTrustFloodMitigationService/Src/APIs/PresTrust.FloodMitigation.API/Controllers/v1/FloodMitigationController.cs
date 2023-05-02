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
    }
}
