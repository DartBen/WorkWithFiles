internal class Program
{
    private static void Main(string[] args)
    {
        string str = "C:\\Users\\Dima\\Desktop\\dmitriy.ryabov-client-Dmitriy_Ryabov (1)";
        long l = 0;

        l = WorkWithFiles.DirSize(str);

        Console.WriteLine("Текущий размер папки:",l);
    }
}

class WorkWithFiles
{
    public static long DirSize(string path)
    {
        long size = 0;
        try
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            if (directory.Exists)
            {
                FileInfo[] fi = directory.GetFiles();
                foreach (FileInfo fi2 in fi)
                {
                    size += fi2.Length;
                }

                DirectoryInfo[] dir = directory.GetDirectories();
                foreach (DirectoryInfo di2 in dir)
                {
                    size += DirSize(di2.FullName);
                }
            }
            else { Console.WriteLine("Ссылка на директорию отсутствует"); }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return size;
        }
        return size;
    }
}