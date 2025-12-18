using System;

namespace StudentManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentManager manager = new StudentManager();
            bool running = true;

            Console.WriteLine("=== HỆ THỐNG QUẢN LÝ SINH VIÊN ===");
            Console.WriteLine("Phiên bản: 1.0");
            Console.WriteLine("Tối đa: 50 sinh viên\n");

            while (running)
            {
                try
                {
                    Console.WriteLine("\n========== MENU ==========");
                    Console.WriteLine("1. Thêm sinh viên");
                    Console.WriteLine("2. Xóa sinh viên");
                    Console.WriteLine("3. Cập nhật điểm");
                    Console.WriteLine("4. In danh sách");
                    Console.WriteLine("5. Tính điểm trung bình");
                    Console.WriteLine("6. Tìm điểm cao nhất");
                    Console.WriteLine("7. Tìm sinh viên theo ID");
                    Console.WriteLine("8. Xem số lượng sinh viên");
                    Console.WriteLine("0. Thoát");
                    Console.WriteLine("==========================");

                    Console.Write("Chọn chức năng: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1": // Thêm sinh viên
                            Console.Write("Nhập ID: ");
                            string id = Console.ReadLine();
                            Console.Write("Nhập tên: ");
                            string name = Console.ReadLine();
                            Console.Write("Nhập điểm (0-10): ");
                            double score = double.Parse(Console.ReadLine());
                            manager.AddStudent(id, name, score);
                            break;

                        case "2": // Xóa sinh viên
                            Console.Write("Nhập ID cần xóa: ");
                            string removeId = Console.ReadLine();
                            manager.RemoveStudent(removeId);
                            break;

                        case "3": // Cập nhật điểm
                            Console.Write("Nhập ID: ");
                            string updateId = Console.ReadLine();
                            Console.Write("Nhập điểm mới (0-10): ");
                            double newScore = double.Parse(Console.ReadLine());
                            manager.UpdateScore(updateId, newScore);
                            break;

                        case "4": // In danh sách
                            manager.DisplayAllStudents();
                            break;

                        case "5": // Tính điểm trung bình
                            double avg = manager.GetAverageScore();
                            Console.WriteLine($"Điểm trung bình của lớp: {avg:F2}");
                            break;

                        case "6": // Tìm điểm cao nhất
                            double max = manager.GetMaxScore();
                            Console.WriteLine($"Điểm cao nhất: {max:F2}");
                            break;

                        case "7": // Tìm sinh viên theo ID
                            Console.Write("Nhập ID cần tìm: ");
                            string findId = Console.ReadLine();
                            var student = manager.FindStudentById(findId);
                            if (student != null)
                            {
                                Console.WriteLine("\nThông tin sinh viên:");
                                student.Display();
                            }
                            else
                                Console.WriteLine("Không tìm thấy sinh viên.");
                            break;

                        case "8": // Xem số lượng sinh viên
                            int currentCount = manager.GetStudentCount();
                            Console.WriteLine($"Số lượng sinh viên hiện tại: {currentCount}/50");
                            break;

                        case "0": // Thoát
                            running = false;
                            Console.WriteLine("\nChương trình kết thúc.");
                            Console.WriteLine("Cảm ơn bạn đã sử dụng!");
                            break;

                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn từ 0-8.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Lỗi: Dữ liệu nhập không đúng định dạng (ví dụ: điểm phải là số).");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Lỗi nhập liệu: {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Lỗi hoạt động: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi không xác định: {ex.Message}");
                }
            }
        }
    }
}