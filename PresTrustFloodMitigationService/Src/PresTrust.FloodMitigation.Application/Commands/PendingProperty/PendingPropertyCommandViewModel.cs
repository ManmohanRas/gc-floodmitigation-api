namespace PresTrust.FloodMitigation.Application.Commands;

public class PendingPropertyCommandViewModel
{
    public bool IsSuccess { get; set; } = false;
    public IEnumerable<ApplicationBrokenRuleViewModel> BrokenRules { get; set; } = new List<ApplicationBrokenRuleViewModel>();
}
