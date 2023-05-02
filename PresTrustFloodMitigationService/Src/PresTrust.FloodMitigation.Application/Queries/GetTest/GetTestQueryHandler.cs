namespace PresTrust.FloodMitigation.Application.Queries
{
    public class GetTestQueryHandler : IRequestHandler<GetTestQuery, GetTestQueryViewModel>
    {
        private readonly IMapper mapper;
        private readonly ITestRepository repoTest;

        public GetTestQueryHandler(
            IMapper mapper,
            ITestRepository repoTest
        )
        {
            this.mapper = mapper;
            this.repoTest = repoTest;
        }

        public async Task<GetTestQueryViewModel> Handle(GetTestQuery request, CancellationToken cancellationToken)
        {
            GetTestQueryViewModel result = new();

            var testEntity = await repoTest.GetTestAsync(request.Id);
            result = mapper.Map<FlmitigTestEntity, GetTestQueryViewModel>(testEntity);

            return result;
        }
    }
}
