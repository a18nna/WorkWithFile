using System;
using System.Collections.Generic;
using System.IO;

namespace BinaryReadWrite
{
    internal class Program
    {
        internal class Student
        {
            public string Name { get; set; }
            public string Group { get; set; }
            public DateTime DateOfBirth { get; set; }
            public decimal AverageScore { get; set; }
        }

        static void Main(string[] args)
        {
            List<Student> studentsToWrite = new List<Student>
            {
                new Student { Name = "Жульен", Group = "G1", DateOfBirth = new DateTime(2001, 10, 22), AverageScore = 3.3M },
                new Student { Name = "Боб", Group = "G1", DateOfBirth = new DateTime(1999, 5, 25), AverageScore = 4.5M},
                new Student { Name = "Лилия", Group = "F2", DateOfBirth = new DateTime(1999, 1, 11), AverageScore = 5M},
                new Student { Name = "Роза", Group = "F2", DateOfBirth = new DateTime(1989, 9, 19), AverageScore = 3.7M}
            };

            WriteStudentsToBinFile(studentsToWrite, "students.dat");

            List<Student> studentsToRead = ReadStudentsFromBinFile("students.dat");

            foreach (Student studentProp in studentsToRead)
            {
                Console.WriteLine(studentProp.Name + " " + studentProp.Group + " " + studentProp.DateOfBirth + " " + studentProp.AverageScore);
            }

            CreateStudentGroupFiles(studentsToRead);
        }

        static void WriteStudentsToBinFile(List<Student> students, string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            foreach (Student student in students)
            {
                bw.Write(student.Name);
                bw.Write(student.Group);
                bw.Write(student.DateOfBirth.ToBinary());
                bw.Write(student.AverageScore);
            }
            bw.Flush();
            bw.Close();
            fs.Close();

        }

        static List<Student> ReadStudentsFromBinFile(string fileName)
        {
            List<Student> result = new List<Student>();
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            Console.WriteLine(sr.ReadToEnd());

            fs.Position = 0;

            BinaryReader br = new BinaryReader(fs);

            while (fs.Position < fs.Length)
            {
                Student student = new Student();
                student.Name = br.ReadString();
                student.Group = br.ReadString();
                long dt = br.ReadInt64();
                student.DateOfBirth = DateTime.FromBinary(dt);
                student.AverageScore = br.ReadDecimal();

                result.Add(student);
            }

            fs.Close();
            return result;
        }

        static void CreateStudentGroupFiles(List<Student> students)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string directoryPath = Path.Combine(desktopPath, "Students");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            Dictionary<string, List<Student>> groupedStudents = new Dictionary<string, List<Student>>();
            foreach (var student in students)
            {
                if (!groupedStudents.ContainsKey(student.Group))
                {
                    groupedStudents[student.Group] = new List<Student>();
                }
                groupedStudents[student.Group].Add(student);
            }

            foreach (var group in groupedStudents)
            {
                string fileName = Path.Combine(directoryPath, $"{group.Key}.txt");
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    foreach (var student in group.Value)
                    {
                        sw.WriteLine($"{student.Name}, {student.DateOfBirth.ToString("dd.MM.yyyy")}, {student.AverageScore}");
                    }
                }
            }

            Console.WriteLine($"Файлы с информацией о студентах созданы в директории {directoryPath}.");
        }
    }
}