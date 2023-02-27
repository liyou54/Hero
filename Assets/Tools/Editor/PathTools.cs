using System.IO;

public static class PathTools
{
    public static  void CreateDirIfNotExists(string path)
    {
        // 创建多级目录
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}