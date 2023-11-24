namespace PresTrust.FloodMitigation.Application.Commands;

public class SubmitPropertyCommandViewModel
{
    public bool IsSuccess { get; set; } = false;
    public IEnumerable<PropertyBrokenRulesViewModel> BrokenRules { get; set; } = new List<PropertyBrokenRulesViewModel>();
}
