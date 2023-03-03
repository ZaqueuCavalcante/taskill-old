namespace Taskill.Domain;

public class Reminder
{
    public uint Id { get; set; }

    public uint TaskId { get; set; }

    public bool Viewed { get; set; }

    public int? BeforeInMinutes { get; set; }

    public Reminder(uint taskId, int? beforeInMinutes)
    {
        TaskId = taskId;
        BeforeInMinutes = beforeInMinutes;
    }
}
