using FluentAssertions;
using NUnit.Framework;
using Taskill.Exceptions;

namespace Taskill.Tests;

public class UnitTests
{
    [Test]
    public void On_task_creation__when_priority_is_not_defined__it_should_be_zero()
    {
        var task = new Domain.Task();

        task.Priority.Should().Be(0);
    }

    [Test]
    public void On_task_creation__the_priority_should_be_a_valid_value()
    {
        Action act = () => new Domain.Task(1, 1, "Finish this project", "Today", 4);

        act.Should().Throw<DomainException>()
            .WithMessage("The task priority should be 0, 1, 2 or 3.");
    }
}
