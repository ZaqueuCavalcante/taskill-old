using FluentAssertions;
using NUnit.Framework;
using Taskill.Domain;
using Taskill.Exceptions;

namespace Taskill.Tests.Unit;

public class TasksUnitTests
{
    [Test]
    [TestCaseSource(typeof(Streams), nameof(Streams.ValidTitlesStream))]
    public void On_task_creation__the_title_should_be_a_valid_value(string title)
    {
        // Arrange / Act
        Action act = () => new Domain.Task(
            userId: 1,
            projectId: 1,
            sectionId: null,
            title: title,
            description: "Now",
            priority: Priority.High
        );

        // Assert
        act.Should().NotThrow<DomainException>();
    }

    [Test]
    [TestCaseSource(typeof(Streams), nameof(Streams.InvalidTitlesStream))]
    public void On_task_creation__the_title_should_not_be_a_invalid_value(string title)
    {
        // Arrange / Act
        Action act = () => new Domain.Task(
            userId: 1,
            projectId: 1,
            sectionId: null,
            title: title,
            description: "Now",
            priority: Priority.Medium
        );

        // Assert
        act.Should().Throw<DomainException>()
            .WithMessage("The task title should be contains more that 3 letters.");
    }

    [Test]
    public void On_task_creation__the_completion_date_should_be_null()
    {
        // Arrange / Act
        var task = new Domain.Task(
            userId: 1,
            projectId: 1,
            sectionId: null,
            title: "Finish this project",
            description: "Now",
            priority: Priority.Low
        );

        // Assert
        task.CompletionDate.Should().BeNull();
    }

    [Test]
    public void After_task_creation__should_complete_the_task()
    {
        // Arrange
        var task = new Domain.Task(
            userId: 1,
            projectId: 1,
            sectionId: null,
            title: "Finish this project",
            description: "Now",
            priority: Priority.Low
        );

        // Act
        task.Complete();

        // Assert
        task.CompletionDate.Should().NotBeNull();
    }

    [Test]
    public void After_task_creation_and_complete__should_uncomplete_the_task()
    {
        // Arrange
        var task = new Domain.Task(
            userId: 1,
            projectId: 1,
            sectionId: null,
            title: "Finish this project",
            description: "Now",
            priority: Priority.Low
        );

        // Act / Assert
        task.Complete();
        task.CompletionDate.Should().NotBeNull();

        task.Uncomplete();
        task.CompletionDate.Should().BeNull();
    }

    [Test]
    [TestCaseSource(typeof(Streams), nameof(Streams.ValidTitlesStream))]
    public void After_task_creation__should_change_the_task_title(string title)
    {
        // Arrange
        var task = new Domain.Task(
            userId: 1,
            projectId: 1,
            sectionId: null,
            title: "Finish this project",
            description: "Now",
            priority: Priority.Low
        );

        // Act
        task.SetTitle(title);

        // Assert
        task.Title.Should().Be(title);
    }

    [Test]
    public void After_task_creation__should_change_the_task_description()
    {
        // Arrange
        var task = new Domain.Task(
            userId: 1,
            projectId: 1,
            sectionId: null,
            title: "Finish this project",
            description: "Now",
            priority: Priority.Low
        );

        const string newDescription = "New description lalala...";

        // Act
        task.SetDescription(newDescription);

        // Assert
        task.Description.Should().Be(newDescription);
    }
}
