using FluentAssertions;
using NUnit.Framework;
using Taskill.Domain;
using Taskill.Exceptions;
using Taskill.Extensions;
using Task = Taskill.Domain.Task;

namespace Taskill.Tests.Unit;

public class ProjectsUnitTests
{
    [Test]
    [TestCaseSource(typeof(Streams), nameof(Streams.ValidProjectNamesStream))]
    public void On_project_creation__the_name_should_be_a_valid_value(string name)
    {
        // Arrange + Act
        var act = () => new Project(
            userId: 1,
            name: name
        );

        // Assert
        act.Should().NotThrow<DomainException>();
    }

    [Test]
    [TestCaseSource(typeof(Streams), nameof(Streams.InvalidProjectNamesStream))]
    public void On_project_creation__when_name_is_invalid__should_throw_error(string name)
    {
        // Arrange + Act
        var act = () => new Project(
            userId: 1,
            name: name
        );

        // Assert
        act.Should().Throw<DomainException>()
            .WithMessage("The project name should be contains more that 3 letters.");
    }

    [Test]
    public void On_project_tasks_reorder__when_list_contains_only_one_task__should_not_change_the_list()
    {
        // Arrange
        var tasks = new List<Task>()
        {
            new Task() { Id = 123, Index = 0, },
        };

        // Act
        tasks = tasks.MoveTask(0, 1);

        // Assert
        tasks[0].Id.Should().Be(123);
    }

    [Test]
    public void On_project_tasks_reorder__when_start_and_end_are_equal__should_not_change_the_list()
    {
        // Arrange
        var tasks = new List<Task>()
        {
            new Task() { Id = 123, Index = 0, },
            new Task() { Id = 456, Index = 1, },
        };

        // Act
        tasks = tasks.MoveTask(1, 1);

        // Assert
        tasks[0].Id.Should().Be(123);
        tasks[1].Id.Should().Be(456);
    }

    [Test]
    public void On_project_tasks_reorder__should_move_task_from_0_to_1_index()
    {
        // Arrange
        var tasks = new List<Task>()
        {
            new Task() { Id = 123, Index = 0, },
            new Task() { Id = 456, Index = 1, },
        };

        // Act
        tasks = tasks.MoveTask(0, 1);

        // Assert
        tasks[0].Id.Should().Be(456);
        tasks[1].Id.Should().Be(123);
    }

    [Test]
    public void On_project_tasks_reorder__should_move_task_from_1_to_0_index()
    {
        // Arrange
        var tasks = new List<Task>()
        {
            new Task() { Id = 123, Index = 0, },
            new Task() { Id = 456, Index = 1, },
        };

        // Act
        tasks = tasks.MoveTask(1, 0);

        // Assert
        tasks[0].Id.Should().Be(456);
        tasks[1].Id.Should().Be(123);
    }

    [Test]
    public void On_project_tasks_reorder__should_move_task_from_0_to_2_index()
    {
        // Arrange
        var tasks = new List<Task>()
        {
            new Task() { Id = 123, Index = 0, },
            new Task() { Id = 456, Index = 1, },
            new Task() { Id = 789, Index = 2, },
        };

        // Act
        tasks = tasks.MoveTask(0, 2);

        // Assert
        tasks[0].Id.Should().Be(456);
        tasks[1].Id.Should().Be(789);
        tasks[2].Id.Should().Be(123);
    }

    [Test]
    public void On_project_tasks_reorder__should_move_task_from_2_to_0_index()
    {
        // Arrange
        var tasks = new List<Task>()
        {
            new Task() { Id = 123, Index = 0, },
            new Task() { Id = 456, Index = 1, },
            new Task() { Id = 789, Index = 2, },
        };

        // Act
        tasks = tasks.MoveTask(2, 0);

        // Assert
        tasks[0].Id.Should().Be(789);
        tasks[1].Id.Should().Be(123);
        tasks[2].Id.Should().Be(456);
    }

    [Test]
    public void On_project_tasks_reorder__should_move_task_from_0_to_2_index__5_tasks_case()
    {
        // Arrange
        var tasks = new List<Task>()
        {
            new Task() { Id = 12, Index = 0, },
            new Task() { Id = 34, Index = 1, },
            new Task() { Id = 56, Index = 2, },
            new Task() { Id = 78, Index = 3, },
            new Task() { Id = 91, Index = 4, },
        };

        // Act
        tasks = tasks.MoveTask(0, 2);

        // Assert
        tasks[0].Id.Should().Be(34);
        tasks[1].Id.Should().Be(56);
        tasks[2].Id.Should().Be(12);
        tasks[3].Id.Should().Be(78);
        tasks[4].Id.Should().Be(91);
    }

    [Test]
    public void On_project_tasks_reorder__should_move_task_from_2_to_0_index__5_tasks_case()
    {
        // Arrange
        var tasks = new List<Task>()
        {
            new Task() { Id = 12, Index = 0, },
            new Task() { Id = 34, Index = 1, },
            new Task() { Id = 56, Index = 2, },
            new Task() { Id = 78, Index = 3, },
            new Task() { Id = 91, Index = 4, },
        };

        // Act
        tasks = tasks.MoveTask(2, 0);

        // Assert
        tasks[0].Id.Should().Be(56);
        tasks[1].Id.Should().Be(12);
        tasks[2].Id.Should().Be(34);
        tasks[3].Id.Should().Be(78);
        tasks[4].Id.Should().Be(91);
    }

    [Test]
    public void On_project_tasks_reorder__should_move_task_from_1_to_3_index()
    {
        // Arrange
        var tasks = new List<Task>()
        {
            new Task() { Id = 12, Index = 0, },
            new Task() { Id = 34, Index = 1, },
            new Task() { Id = 56, Index = 2, },
            new Task() { Id = 78, Index = 3, },
            new Task() { Id = 91, Index = 4, },
        };

        // Act
        tasks = tasks.MoveTask(1, 3);

        // Assert
        tasks[0].Id.Should().Be(12);
        tasks[1].Id.Should().Be(56);
        tasks[2].Id.Should().Be(78);
        tasks[3].Id.Should().Be(34);
        tasks[4].Id.Should().Be(91);
    }

    [Test]
    public void On_project_tasks_reorder__should_move_task_from_3_to_1_index()
    {
        // Arrange
        var tasks = new List<Task>()
        {
            new Task() { Id = 12, Index = 0, },
            new Task() { Id = 34, Index = 1, },
            new Task() { Id = 56, Index = 2, },
            new Task() { Id = 78, Index = 3, },
            new Task() { Id = 91, Index = 4, },
        };

        // Act
        tasks = tasks.MoveTask(3, 1);

        // Assert
        tasks[0].Id.Should().Be(12);
        tasks[1].Id.Should().Be(78);
        tasks[2].Id.Should().Be(34);
        tasks[3].Id.Should().Be(56);
        tasks[4].Id.Should().Be(91);
    }

    [Test]
    public void On_project_tasks_reorder__should_move_task_from_2_to_6_index__7_tasks_case()
    {
        // Arrange
        var tasks = new List<Task>()
        {
            new Task() { Id = 12, Index = 0, },
            new Task() { Id = 34, Index = 1, },
            new Task() { Id = 56, Index = 2, },
            new Task() { Id = 78, Index = 3, },
            new Task() { Id = 91, Index = 4, },
            new Task() { Id = 93, Index = 5, },
            new Task() { Id = 95, Index = 6, },
        };

        // Act
        tasks = tasks.MoveTask(2, 6);

        // Assert
        tasks[0].Id.Should().Be(12);
        tasks[1].Id.Should().Be(34);
        tasks[2].Id.Should().Be(78);
        tasks[3].Id.Should().Be(91);
        tasks[4].Id.Should().Be(93);
        tasks[5].Id.Should().Be(95);
        tasks[6].Id.Should().Be(56);
    }

    [Test]
    public void On_project_tasks_reorder__should_move_task_from_6_to_2_index__7_tasks_case()
    {
        // Arrange
        var tasks = new List<Task>()
        {
            new Task() { Id = 12, Index = 0, },
            new Task() { Id = 34, Index = 1, },
            new Task() { Id = 56, Index = 2, },
            new Task() { Id = 78, Index = 3, },
            new Task() { Id = 91, Index = 4, },
            new Task() { Id = 93, Index = 5, },
            new Task() { Id = 95, Index = 6, },
        };

        // Act
        tasks = tasks.MoveTask(6, 2);

        // Assert
        tasks[0].Id.Should().Be(12);
        tasks[1].Id.Should().Be(34);
        tasks[2].Id.Should().Be(95);
        tasks[3].Id.Should().Be(56);
        tasks[4].Id.Should().Be(78);
        tasks[5].Id.Should().Be(91);
        tasks[6].Id.Should().Be(93);
    }
}
