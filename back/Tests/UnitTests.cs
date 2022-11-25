using FluentAssertions;
using NUnit.Framework;

namespace Taskill.Tests;

public class UnitTests
{
    [Test]
    public void On_task_creation__when_priority_is_not_defined__it_should_be_zero()
    {
        var task = new Domain.Task();

        task.Priority.Should().Be(0);
    }
}
