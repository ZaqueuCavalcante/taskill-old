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
            title: title,
            description: "Now",
            priority: 0
        );

        act.Should().Throw<DomainException>()
            .WithMessage("The task title should be contains more that 3 letters.");
    }
}
