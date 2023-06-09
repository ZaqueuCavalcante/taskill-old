namespace Taskill.Tests;

public static class Streams
{
    public static IEnumerable<object[]> InvalidTitlesStream()
    {
        foreach (var title in new List<string>() { null, "", "a", "", " ", "  ", "     ", "La", })
        {
            yield return new object[] { title };
        }
    }

    public static IEnumerable<object[]> ValidTitlesStream()
    {
        foreach (var title in new List<string>() { "Finish this app today", "Learning english", "Make some stuff lalala", })
        {
            yield return new object[] { title };
        }
    }

    public static IEnumerable<object[]> InvalidProjectNamesStream()
    {
        foreach (var name in new List<string>() { null, "", "a", "", " ", "  ", "     ", "La", "Pr", })
        {
            yield return new object[] { name };
        }
    }

    public static IEnumerable<object[]> ValidProjectNamesStream()
    {
        foreach (var name in new List<string>() { "Taskill", "English", "Cloud", "DevOps", })
        {
            yield return new object[] { name };
        }
    }
}
