namespace PresTrust.FloodMitigation.Application.Commands;

public class ApproveDeclarationCommandViewModel
{
    public bool IsSuccess { get; set; } = false;
    public IEnumerable<ApplicationBrokenRuleViewModel> BrokenRules { get; set; } = new List<ApplicationBrokenRuleViewModel>();
}
