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
}
