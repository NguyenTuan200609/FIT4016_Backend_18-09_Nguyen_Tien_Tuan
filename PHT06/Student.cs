using System;

namespace StudentManagementSystem
{
    // Lớp Student chứa thông tin và điểm của 1 sinh viên
    public class Student
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public double Score { get; set; }

        // Constructor
        public Student(string id, string name, double score)
        {
            // Validation: StudentId không được rỗng
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("ID không được để trống.");

            // Validation: Name không được rỗng
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên không được để trống.");

            // Validation: Score phải từ 0 đến 10
            if (score < 0 || score > 10)
                throw new ArgumentException("Điểm phải từ 0 đến 10.");

            StudentId = id;
            Name = name;
            Score = score;
        }

        // Phương thức in thông tin
        public void Display()
        {
            Console.WriteLine($"ID: {StudentId} | Tên: {Name} | Điểm: {Score:F2}");
        }
    }
}