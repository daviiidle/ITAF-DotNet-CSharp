namespace ITAF.Core.Utilities;

public static class PathResolver
{
    public static string EnsureDirectory(string path)
    {
        Directory.CreateDirectory(path);
        return Path.GetFullPath(path);
    }

    public static string SanitizeFileName(string value)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        return string.Join("_", value.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries)).Trim();
    }
}

