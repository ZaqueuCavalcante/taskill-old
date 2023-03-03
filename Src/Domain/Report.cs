namespace Taskill.Domain;

public class Report
{
    public uint Id { get; set; }

    public uint UserId { get; set; }

    public string Name { get; set; }

    public List<ReportTask> Tasks { get; set; }

    public Report(List<Action> actions)
    {
        Tasks = new List<ReportTask>();

        foreach (var taskId in actions.ConvertAll(a => a.TaskId).Distinct().ToList())
        {
            var completedAt = actions.First(a => a.TaskId == taskId && a.Type == ActionType.CompletedTask).Date;
            var addedAt = actions.First(a => a.TaskId == taskId && a.Type == ActionType.AddedTask).Date;

            Tasks.Add(new ReportTask() { Id = taskId!.Value, Duration = completedAt - addedAt, });
        }
    }
}

public class ReportTask
{
    public uint Id { get; set; }

    public TimeSpan Duration { get; set; }
}
