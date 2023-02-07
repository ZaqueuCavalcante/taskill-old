namespace Taskill.Domain;

public class Action
{
    public uint Id { get; set; }

    public uint UserId { get; set; }

    public ActionType Type { get; set; }

    public DateTime Date { get; set; }

    public uint? ProjectId { get; set; }

    public uint? TaskId { get; set; }

    public uint? LabelId { get; set; }

    public Action() { }

    public Action(
        uint userId,
        ActionType type,
        uint taskId
    ) {
        UserId = userId;
        Type = type;
        Date = DateTime.Now;
        TaskId = taskId;
    }
}
