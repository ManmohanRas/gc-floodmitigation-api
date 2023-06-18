namespace PresTrust.FloodMitigation.Application.Commands
{
    public class SaveTestCommandHandler : IRequestHandler<SaveTestCommand, SaveTestCommandViewModel>
    {
        private readonly IMapper mapper;
        private readonly ITestRepository repoTest;

        public SaveTestCommandHandler(
            IMapper mapper,
            ITestRepository repoTest
        )
        {
            this.mapper = mapper;
            this.repoTest = repoTest;
        }

        public async Task<SaveTestCommandViewModel> Handle(SaveTestCommand request, CancellationToken cancellationToken)
        {
            SaveTestCommandViewModel result = new();

            var testEntity = mapper.Map<SaveTestCommand, FloodTestEntity>(request);
            result.Id = await repoTest.SaveTestAsync(testEntity);

            return result;
        }
    }
}
