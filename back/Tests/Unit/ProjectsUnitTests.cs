using FluentAssertions;
using NUnit.Framework;
using Taskill.Domain;
using Taskill.Exceptions;

namespace Taskill.Tests.Unit;

public class ProjectsUnitTests
{
    [Test]
    [TestCaseSource(typeof(Streams), nameof(Streams.ValidProjectNamesStream))]
    public void On_project_creation__the_name_should_be_a_valid_value(string name)
    {
        var act = () => new Project(
            userId: 1,
            name: name
        );

        act.Should().NotThrow<DomainException>();
    }

    [Test]
    [TestCaseSource(typeof(Streams), nameof(Streams.InvalidProjectNamesStream))]
    public void On_project_creation__the_name_should_not_be_a_invalid_value(string name)
    {
        var act = () => new Project(
            userId: 1,
            name: name
        );

        act.Should().Throw<DomainException>()
            .WithMessage("The project name should be contains more that 3 letters.");
    }
}
