using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderManagementApp.Data;
using OrderManagementApp.Models;

namespace OrderManagementApp.Services
{
    public class OrderService
    {
        private readonly OrderDbContext _context;

        public OrderService(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            // Kiểm tra product tồn tại
            var product = await _context.Products.FindAsync(order.ProductId);
            if (product == null)
                throw new Exception("Product not found");
            
            // Kiểm tra stock
            if (order.Quantity > product.StockQuantity)
                throw new Exception($"Insufficient stock. Available: {product.StockQuantity}");
            
            // Tạo order number nếu chưa có
            if (string.IsNullOrEmpty(order.OrderNumber))
            {
                var today = DateTime.Today.ToString("yyyyMMdd");
                var lastOrder = await _context.Orders
                    .Where(o => o.OrderNumber.StartsWith($"ORD-{today}"))
                    .OrderByDescending(o => o.OrderNumber)
                    .FirstOrDefaultAsync();
                    
                var nextNumber = lastOrder != null 
                    ? int.Parse(lastOrder.OrderNumber.Split('-').Last()) + 1 
                    : 1;
                order.OrderNumber = $"ORD-{today}-{nextNumber:D4}";
            }
            
            // Cập nhật stock
            product.StockQuantity -= order.Quantity;
            _context.Products.Update(product);
            
            // Thêm order
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            
            return order;
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            var existingOrder = await _context.Orders.FindAsync(order.Id);
            if (existingOrder == null)
                return false;
            
            // Cập nhật các field được phép
            existingOrder.CustomerName = order.CustomerName;
            existingOrder.CustomerEmail = order.CustomerEmail;
            existingOrder.Quantity = order.Quantity;
            existingOrder.DeliveryDate = order.DeliveryDate;
            existingOrder.UpdatedAt = DateTime.Now;
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
                
            if (order == null)
                return false;
            
            // Hoàn trả stock
            if (order.Product != null)
            {
                order.Product.StockQuantity += order.Quantity;
                _context.Products.Update(order.Product);
            }
            
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<List<Order>> SearchOrdersAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return await GetAllOrdersAsync();
            
            keyword = keyword.ToLower();
            
            return await _context.Orders
                .Include(o => o.Product)
                .Where(o => o.OrderNumber.ToLower().Contains(keyword) ||
                           o.CustomerName.ToLower().Contains(keyword) ||
                           o.CustomerEmail.ToLower().Contains(keyword) ||
                           o.Product.Name.ToLower().Contains(keyword))
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}