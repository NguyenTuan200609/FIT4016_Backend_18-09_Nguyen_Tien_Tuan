using System;

class Program
{
    static void Main(string[] agrs)
    {
        Console.WriteLine("Chào mừng đến với C#!");
        string ten = "Nguyễn Văn A"; // Chuỗi ký tự
        int tuoi = 20; // Số nguyên
        double diem = 8.5; // Số thực
        Console.WriteLine("Tên: " + ten);
        Console.WriteLine("Tuổi: " + tuoi);
        Console.WriteLine("Điểm: " + diem);
        Console.WriteLine($"Thông tin: {ten}, tuổi {tuoi}, điểm {diem}");

    }
}