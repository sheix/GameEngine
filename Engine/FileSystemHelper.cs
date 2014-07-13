namespace Engine
{
    public static class FileSystemHelper
    {
        public static readonly char FileSystemSeparator = System.IO.Path.DirectorySeparatorChar;
        public static readonly string PathToResources = @".." + FileSystemSeparator + ".." + FileSystemSeparator + ".." + FileSystemSeparator + "Resources" + FileSystemSeparator;
    }
}
