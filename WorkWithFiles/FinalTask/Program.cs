
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string path = "C:\\Users\\Dima\\Desktop\\Students.dat";
            string endPath = "C:\\Users\\Dima\\Desktop\\Students";

            StudetExtension.SortStudent(path,endPath);
        }

    }

    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    class StudetExtension
    {
        public static void SortStudent(string pathOfSourse,string endPath)
        {

            DirectoryInfo directoryInfo = new DirectoryInfo(endPath);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            Student[] students;
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(pathOfSourse, FileMode.Open))
                {
                    students = (Student[])binaryFormatter.Deserialize(fs);

                    Console.WriteLine("Из файла считано:");

                    foreach (var student in students)
                    {
                        Console.WriteLine(($"{student.Name} \t {student.Group}"));
                        string pathToGroup = endPath + "\\" + student.Group + ".txt";
                        using (StreamWriter sw = new StreamWriter(pathToGroup, true))
                        {
                            sw.WriteLine($"Имя:{student.Name}, дата рождения: {student.DateOfBirth.ToString("D")}");
                        }
                    }
                }
            }
            catch(Exception e) { Console.WriteLine(e.Message); }
        }
    }

}
