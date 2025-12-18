using System;

namespace StudentManagementSystem
{
    public class StudentManager
    {
        private Student[] students = new Student[50];
        private int count = 0; // Số lượng sinh viên hiện tại

        // Phương thức thêm sinh viên mới
        public void AddStudent(string id, string name, double score)
        {
            // Kiểm tra danh sách đầy
            if (count >= students.Length)
                throw new InvalidOperationException("Danh sách sinh viên đã đầy (tối đa 50).");

            // Kiểm tra trùng ID
            if (FindStudentById(id) != null)
                throw new ArgumentException($"ID '{id}' đã tồn tại.");

            // Tạo sinh viên mới (validation trong constructor)
            students[count] = new Student(id, name, score);
            count++;
            Console.WriteLine("Thêm sinh viên thành công.");
        }

        // Phương thức xóa sinh viên theo ID
        public void RemoveStudent(string id)
        {
            int index = -1;
            for (int i = 0; i < count; i++)
            {
                if (students[i].StudentId == id)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
                throw new ArgumentException($"Không tìm thấy sinh viên với ID '{id}'.");

            // Dồn mảng
            for (int i = index; i < count - 1; i++)
            {
                students[i] = students[i + 1];
            }
            students[count - 1] = null;
            count--;
            Console.WriteLine("Xóa sinh viên thành công.");
        }

        // Phương thức cập nhật điểm
        public void UpdateScore(string id, double newScore)
        {
            var student = FindStudentById(id);
            if (student == null)
                throw new ArgumentException($"Không tìm thấy sinh viên với ID '{id}'.");

            if (newScore < 0 || newScore > 10)
                throw new ArgumentException("Điểm phải từ 0 đến 10.");

            student.Score = newScore;
            Console.WriteLine("Cập nhật điểm thành công.");
        }

        // Phương thức tính điểm trung bình
        public double GetAverageScore()
        {
            if (count == 0)
                throw new InvalidOperationException("Danh sách sinh viên trống.");

            double sum = 0;
            for (int i = 0; i < count; i++)
            {
                sum += students[i].Score;
            }
            return sum / count;
        }

        // Phương thức tìm điểm cao nhất
        public double GetMaxScore()
        {
            if (count == 0)
                throw new InvalidOperationException("Danh sách sinh viên trống.");

            double max = students[0].Score;
            for (int i = 1; i < count; i++)
            {
                if (students[i].Score > max)
                    max = students[i].Score;
            }
            return max;
        }

        // Phương thức tìm sinh viên theo ID
        public Student FindStudentById(string id)
        {
            for (int i = 0; i < count; i++)
            {
                if (students[i].StudentId == id)
                    return students[i];
            }
            return null;
        }

        // Phương thức in danh sách tất cả sinh viên
        public void DisplayAllStudents()
        {
            if (count == 0)
            {
                Console.WriteLine("Danh sách sinh viên trống.");
                return;
            }

            Console.WriteLine("\n=== DANH SÁCH SINH VIÊN ===");
            for (int i = 0; i < count; i++)
            {
                students[i].Display();
            }
            Console.WriteLine($"Tổng số: {count} sinh viên");
        }

        // Phương thức lấy số lượng sinh viên hiện tại
        public int GetStudentCount()
        {
            return count;
        }
    }
}