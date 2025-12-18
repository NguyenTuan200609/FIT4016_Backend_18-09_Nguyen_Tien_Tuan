using System;
class ShoppingCalculator
{
    static void Main(string[] args)
    {
        Console.WriteLine("===Chương trình xếp loại nhân viên===");
        string hoVaTen = "Nguyễn Tiến Tuấn";
        double diem = 10.0;
        Console.WriteLine($"Họ tên: {hoVaTen}");
        Console.WriteLine($"Điểm: {diem}\n");

        if (diem >= 8.5)
        {
            Console.WriteLine("xếp loại: giỏi");
        }
        else if (diem >= 7.0)
        {
            Console.WriteLine("xếp loại: khá");
        }
        else if (diem >= 5.5)
        {
            Console.WriteLine("xếp loại: trung binh");
        }
        else
        {
            Console.WriteLine("xếp loại: yếu");
        }
    }
}

