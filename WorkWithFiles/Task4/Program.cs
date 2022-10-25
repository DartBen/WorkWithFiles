
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string path = "C:\\Users\\Dima\\Desktop\\Students.dat";
            string endPath = "C:\\Users\\Dima\\Desktop\\Students";

            DirectoryInfo directoryInfo = new DirectoryInfo(endPath);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            Student[] students;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                students = (Student[])binaryFormatter.Deserialize(fs);

                Console.WriteLine("Из файла считано:");

                foreach (var student in students)
                {
                    Console.WriteLine(($"{student.Name} \t {student.Group}"));

                    string pathToGroup = endPath + "\\" + student.Group + ".txt";
                    if (!File.Exists(pathToGroup))
                    {

                    }
                    using (StreamWriter sw = new StreamWriter(pathToGroup, true))
                    {
                        sw.WriteLine($"Имя:{student.Name}, дата рождения: {student.DateOfBirth}");
                    }
                }
            }

        }

    }

    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
