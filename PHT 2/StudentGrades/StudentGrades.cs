// ...existing code...
using System;

class StudentGrades
{
    static void Main_Old(string[] args)
    {
        string[] tenSV = { "Nguyễn Văn Tùng", "Trần Thị Ánh", "Lê Văn Vinh" };
        double[] diemSV = { 8.5, 7.2, 5.8 };

        if (tenSV.Length != diemSV.Length)
        {
            Console.WriteLine("Lỗi: số lượng tên và điểm không khớp.");
            return;
        }

        Console.WriteLine("\n=== Bảng Điểm ===");
        // dùng vòng for để in
        for (int i = 0; i < tenSV.Length; i++)
        {
            Console.WriteLine($"ten: {tenSV[i]}, diem: {diemSV[i]}");
        }

        // dùng vòng while để tính tổng
        double tongDiem = 0;
        int j = 0;
        while (j < diemSV.Length)
        {
            tongDiem += diemSV[j];
            j++;
        }

        if (diemSV.Length == 0)
        {
            Console.WriteLine("\nKhông có điểm để tính.");
            return;
        }

        Console.WriteLine($"\nTổng điểm: {tongDiem}");
        Console.WriteLine($"Điểm trung bình: {tongDiem / diemSV.Length:F2}");
    }
}
//