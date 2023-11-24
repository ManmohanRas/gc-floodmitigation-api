namespace PresTrust.FloodMitigation.Application.Commands;

public class PreservePropertyCommandViewModel
{
    public bool IsSuccess { get; set; } = false;
    public IEnumerable<PropertyBrokenRulesViewModel> BrokenRules { get; set; } = new List<PropertyBrokenRulesViewModel>();
}
