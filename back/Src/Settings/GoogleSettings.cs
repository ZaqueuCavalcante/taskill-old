namespace Taskill.Settings;

public class GoogleSettings
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }

    public GoogleSettings(IConfiguration configuration)
    {
        configuration.GetSection("Google").Bind(this);
    }
}
