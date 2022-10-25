internal class Program
{
    private static void Main(string[] args)
    {

        string str = "C:\\Users\\Dima\\Desktop\\dmitriy.ryabov-client-Dmitriy_Ryabov (1)";


        try
        {
            WorkWithFiles.CheckDeleteCheck(str, 30);
        }
        catch { Console.ReadKey(); }

    }
}

class WorkWithFiles
{

    public static void CheckDeleteCheck(string path, int time)
    {
        int deletedFilesCount = 0;

        long lCheck1 = DirSize(path);
        DirDeleteOld(path, time, ref deletedFilesCount);
        long lCheck2 = DirSize(path);
        long lCheck3 = lCheck1 - lCheck2;


        Console.WriteLine($"Исходный размер папки: {lCheck1} байт");
        Console.WriteLine($"Удалено:\t{deletedFilesCount} файлов. Освобожденно {lCheck3} байт");
        Console.WriteLine($"Текущий размер папки: {lCheck2}");
    }

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

    public static void DirDeleteOld(string path, int time, ref int deletedCount)
    {
        DateTime checkTime = DateTime.Now - TimeSpan.FromMinutes(time);


        if (Directory.Exists(path))
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            FileInfo[] fi = directory.GetFiles();
            if (directory.Exists)
            {
                try
                {
                    foreach (FileInfo fi2 in fi)
                    {
                        if (fi2.LastAccessTime < checkTime)
                        {
                            Console.WriteLine("Удаление:" + fi2.FullName);
                            deletedCount++;
                            fi2.Delete();
                        }
                    }

                    DirectoryInfo[] di = directory.GetDirectories();
                    foreach (DirectoryInfo di2 in di)
                    {
                        DirDeleteOld(di2.FullName, time, ref deletedCount);

                        //проверка что текущая дериктория пуста
                        FileInfo[] fi2 = di2.GetFiles();
                        if (fi2.Length <= 0 || fi2 == null)
                        {
                            Console.WriteLine("Удаление:" + di2.FullName);
                            deletedCount++;
                            di2.Delete();

                        }
                    }
                }
                catch (Exception e) { Console.WriteLine(e); }
            }
            else
            {
                Console.WriteLine("Ссылка на директорию отсутствует");
            }
        }
        else { Console.WriteLine("Директрория не существует"); }
    }

}




