namespace Taskill.Tests;

public static class Streams
{
    private static readonly List<byte> InvalidPriorities = new()
    {
        4, 5, 6, 7,
    };
    public static IEnumerable<object[]> InvalidPrioritiesStream()
    {
        foreach (var priority in InvalidPriorities)
        {
            yield return new object[] { priority };
        }
    }

    private static readonly List<byte> ValidPriorities = new()
    {
        0, 1, 2, 3,
    };
    public static IEnumerable<object[]> ValidPrioritiesStream()
    {
        foreach (var priority in ValidPriorities)
        {
            yield return new object[] { priority };
        }
    }

    private static readonly List<string> InvalidTitles = new()
    {
        null, "", "a", "", " ", "  ", "     ", "La",
    };
    public static IEnumerable<object[]> InvalidTitlesStream()
    {
        foreach (var title in InvalidTitles)
        {
            yield return new object[] { title };
        }
    }

    private static readonly List<string> ValidTitles = new()
    {
        "Finish this app today", "Learning english", "Make some stuff lalala",
    };
    public static IEnumerable<object[]> ValidTitlesStream()
    {
        foreach (var title in ValidTitles)
        {
            yield return new object[] { title };
        }
    }

    private static readonly List<string> InvalidProjectNames = new()
    {
        null, "", "a", "", " ", "  ", "     ", "La", "Pr",
    };
    public static IEnumerable<object[]> InvalidProjectNamesStream()
    {
        foreach (var name in InvalidProjectNames)
        {
            yield return new object[] { name };
        }
    }

    private static readonly List<string> ValidProjectNames = new()
    {
        "Taskill", "English", "Cloud", "DevOps",
    };
    public static IEnumerable<object[]> ValidProjectNamesStream()
    {
        foreach (var name in ValidProjectNames)
        {
            yield return new object[] { name };
        }
    }
}
