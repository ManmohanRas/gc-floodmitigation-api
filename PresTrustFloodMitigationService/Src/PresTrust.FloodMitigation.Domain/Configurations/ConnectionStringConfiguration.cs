namespace PresTrust.FloodMitigation.Domain.Configurations;

public class ConnectionStringConfiguration
{
    public ConnectionStringConfiguration(string value) => Value = value;

    public String Value { get; set; }
}
