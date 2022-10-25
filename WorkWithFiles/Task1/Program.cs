using System.Drawing;

internal class Program
{
    private static void Main(string[] args)
    {
        string str = "C:\\Users\\Dima\\Desktop\\dmitriy.ryabov-client-Dmitriy_Ryabov (1) — копия — копия";

        WorkWithFiles.DirDeleteOld(str, 30);
    }
}

class WorkWithFiles
{
    public static void DirDeleteOld(string path, int time)
    {
        DateTime checkTime = DateTime.Now - TimeSpan.FromMinutes(time);

        try
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);

                FileInfo[] fi = directory.GetFiles();
                if (directory.Exists)
                {
                    foreach (FileInfo fi2 in fi)
                    {
                        if (fi2.LastAccessTime < checkTime)
                        {
                            Console.WriteLine("Удаление:" + fi2.FullName);
                            fi2.Delete();
                        }
                    }

                    DirectoryInfo[] di = directory.GetDirectories();
                    foreach (DirectoryInfo di2 in di)
                    {
                        DirDeleteOld(di2.FullName, time);

                        //проверка что текущая дериктория пуста
                        FileInfo[] fi2 = di2.GetFiles();
                        if (fi2.Length <= 0 || fi2 == null)
                        {
                            Console.WriteLine("Удаление:" + di2.FullName);
                            di2.Delete();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Ссылка на директорию отсутствует");
                }
            }
            else { Console.WriteLine("Директрория не существует"); }
        }
        catch (Exception e) { Console.WriteLine(e); }
    }

}