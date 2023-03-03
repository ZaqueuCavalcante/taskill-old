namespace Taskill.Controllers;

public class PremiumPlanIn
{
    /// <summary>
    /// The id of the taskiller who will join the premium plan.
    /// </summary>
    /// <example>42</example>
    public uint taskillerId { get; set; }

    /// <summary>
    /// The premium plan token, obtained after payment.
    /// </summary>
    /// <example>05357d902541d1d77e</example>
    public string token { get; set; }
}
