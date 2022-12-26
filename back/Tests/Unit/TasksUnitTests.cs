using FluentAssertions;
using NUnit.Framework;
using Taskill.Exceptions;

namespace Taskill.Tests.Unit;

public class TasksUnitTests
{
    [Test]
    [TestCaseSource(typeof(Streams), nameof(Streams.ValidPrioritiesStream))]
    public void On_task_creation__the_priority_should_be_a_valid_value(byte priority)
    {
        Action act = () => new Domain.Task(
            userId: 1,
            projectId: 1,
            sectionId: null,
            title: "Finish this project",
            description: "Now",
            priority: priority
        );

        act.Should().NotThrow<DomainException>();
    }

    [Test]
    [TestCaseSource(typeof(Streams), nameof(Streams.InvalidPrioritiesStream))]
    public void On_task_creation__the_priority_should_not_be_a_invalid_value(byte priority)
    {
        Action act = () => new Domain.Task(
            userId: 1,
            projectId: 1,
            sectionId: null,
            title: "Finish this project",
            description: "Now",
            priority: priority
        );

        act.Should().Throw<DomainException>()
            .WithMessage("The task priority should be 0, 1, 2 or 3.");
    }

    [Test]
    [TestCaseSource(typeof(Streams), nameof(Streams.ValidTitlesStream))]
    public void On_task_creation__the_title_should_be_a_valid_value(string title)
    {
        Action act = () => new Domain.Task(
            userId: 1,
            projectId: 1,
            sectionId: null,
            title: title,
            description: "Now",
            priority: 0
        );

        act.Should().NotThrow<DomainException>();
    }

    [Test]
    [TestCaseSource(typeof(Streams), nameof(Streams.InvalidTitlesStream))]
    public void On_task_creation__the_title_should_not_be_a_invalid_value(string title)
    {
        Action act = () => new Domain.Task(
            userId: 1,
            projectId: 1,
            sectionId: null,
            title: title,
            description: "Now",
            priority: 0
        );

        act.Should().Throw<DomainException>()
            .WithMessage("The task title should be contains more that 3 letters.");
    }

    [Test]
    public void After_task_creation__should_complete_the_task()
    {
        var task = new Domain.Task(
            userId: 1,
            projectId: 1,
            sectionId: null,
            title: "Finish this project",
            description: "Now",
            priority: 3
        );

        task.CompletionDate.Should().BeNull();

        task.Complete();

        task.CompletionDate.Should().NotBeNull();
    }

    [Test]
    public void After_task_creation_and_complete__should_uncomplete_the_task()
    {
        var task = new Domain.Task(
            userId: 1,
            projectId: 1,
            sectionId: null,
            title: "Finish this project",
            description: "Now",
            priority: 3
        );

        task.Complete();
        task.CompletionDate.Should().NotBeNull();

        task.Uncomplete();
        task.CompletionDate.Should().BeNull();
    }

    [Test]
    [TestCaseSource(typeof(Streams), nameof(Streams.ValidPrioritiesStream))]
    public void After_task_creation__should_change_the_task_priority(byte priority)
    {
        var task = new Domain.Task(
            userId: 1,
            projectId: 1,
            sectionId: null,
            title: "Finish this project",
            description: "Now",
            priority: 0
        );

        task.SetPriority(priority);
        task.Priority.Should().Be(priority);
    }

    [Test]
    [TestCaseSource(typeof(Streams), nameof(Streams.ValidTitlesStream))]
    public void After_task_creation__should_change_the_task_title(string title)
    {
        var task = new Domain.Task(
            userId: 1,
            projectId: 1,
            sectionId: null,
            title: "Finish this project",
            description: "Now",
            priority: 0
        );

        task.SetTitle(title);
        task.Title.Should().Be(title);
    }

    [Test]
    public void After_task_creation__should_change_the_task_description()
    {
        var task = new Domain.Task(
            userId: 1,
            projectId: 1,
            sectionId: null,
            title: "Finish this project",
            description: "Now",
            priority: 0
        );

        const string newDescription = "New description lalala...";

        task.SetDescription(newDescription);
        task.Description.Should().Be(newDescription);
    }
}
