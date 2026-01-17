using System;
using System.Text.RegularExpressions;
using OrderManagementApp.Models;

namespace OrderManagementApp.Services
{
    public class ValidationService
    {
        public (bool isValid, string errorMessage) ValidateOrder(Order order)
        {
            // Validate Order Number
            if (string.IsNullOrWhiteSpace(order.OrderNumber))
                return (false, "Order number is required.");
            
            if (!Regex.IsMatch(order.OrderNumber, @"^ORD-\d{8}-\d{4}$"))
                return (false, "Order number must be in format ORD-YYYYMMDD-XXXX.");
            
            // Validate Customer Name
            if (string.IsNullOrWhiteSpace(order.CustomerName) || order.CustomerName.Length < 2 || order.CustomerName.Length > 100)
                return (false, "Customer name must be 2-100 characters.");
            
            // Validate Email
            if (string.IsNullOrWhiteSpace(order.CustomerEmail))
                return (false, "Customer email is required.");
            
            if (!IsValidEmail(order.CustomerEmail))
                return (false, "Invalid email format.");
            
            // Validate Quantity
            if (order.Quantity <= 0)
                return (false, "Quantity must be greater than 0.");
            
            // Validate Order Date
            if (order.OrderDate > DateTime.Today)
                return (false, "Order date cannot be in the future.");
            
            // Validate Delivery Date
            if (order.DeliveryDate.HasValue && order.DeliveryDate < order.OrderDate)
                return (false, "Delivery date cannot be earlier than order date.");
            
            return (true, "");
        }
        
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}