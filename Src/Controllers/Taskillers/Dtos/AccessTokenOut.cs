namespace Taskill.Controllers;

public class AccessTokenOut
{
    /// <summary>
    /// The JWT itself.
    /// </summary>
    /// <example>header_lalala.payload_lalala.signature_lalala</example>
    public string access_token { get; set; }

    /// <summary>
    /// The JWT expiration time in minutes.
    /// </summary>
    /// <example>60</example>
    public int expires_in { get; set; }

    /// <summary>
    /// The token type.
    /// </summary>
    /// <example>Bearer</example>
    public string token_type { get; set; } = "Bearer";
}
