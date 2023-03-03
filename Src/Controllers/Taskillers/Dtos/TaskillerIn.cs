namespace Taskill.Controllers;

public class TaskillerIn
{
    /// <summary>
    /// The user email.
    /// </summary>
    /// <example>taskiller@gmail.com</example>
    public string email { get; set; }

    /// <summary>
    /// The user password.
    /// </summary>
    /// <example>F@d03Ec8394f99</example>
    public string password { get; set; }
}
