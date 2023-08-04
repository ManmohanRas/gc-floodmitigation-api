namespace PresTrust.FloodMitigation.Application.Commands;

public class SubmitApplicationCommandViewModel
{
    public bool IsSuccess { get; set; } = false;
    public IEnumerable<ApplicationBrokenRuleViewModel> BrokenRules { get; set; } = new List<ApplicationBrokenRuleViewModel>();
}
