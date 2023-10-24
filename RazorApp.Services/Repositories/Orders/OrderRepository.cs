using Microsoft.EntityFrameworkCore;
using RazorApp.Data.Database;
using RazorApp.Data.Entities;
using RazorApp.Models.Dtos;
using RazorApp.Services.Extensions;
using RazorApp.Services.Repositories.Generic;
using RazorApp.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Services.Repositories.Orders
{
    public class OrderRepository:GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
            
        }

        public async Task CreateNewOrderAsync(OrderDto orderDto)
        {
            Order order = new();

            order.Code = orderDto.OrderCode;
            order.OrderDate = orderDto.OrderDate;
            order.Customer = (await context.Customers.FirstOrDefaultAsync(e => e.Name == orderDto.CustomerName))!;

            foreach (var orderDetailDto in orderDto.OrderDetailDtos)
            {
                OrderDetail orderDetail = new();

                orderDetail.Order = order;
                orderDetail.Product = (await context.Products.FirstOrDefaultAsync(e => e.Name == orderDetailDto.ProductName))!;
                orderDetail.Qty = orderDetailDto.Qty;
                orderDetail.Product.Qty -= orderDetailDto.Qty;

                context.OrderDetails.Add(orderDetail);
            }

            await CreateNewRecordAsync(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            Order order = (await context.Orders
                                .Include(e => e.OrderDetails)
                                .ThenInclude(e => e.Product)
                                .FirstOrDefaultAsync(e => e.Id == id))!;

            foreach (var orderDetail in order.OrderDetails)
            {
                orderDetail.Product.Qty += orderDetail.Qty;

                context.OrderDetails.Remove(orderDetail);
            }

            await DeleteRecordAsync(id);
        }

        public async Task<Pagination<OrderDto>> GetAllOrdersAsync(string searchText, string customer, int pageIndex, int pageSize)
        {
            var allOrders = await context.Orders
                                  .Include(e => e.Customer)
                                  .Include(e => e.OrderDetails)
                                  .ThenInclude(e => e.Product)
                                  .AsNoTracking()
                                  .ToListAsync();

            if (!string.IsNullOrEmpty(searchText))
            {
                allOrders = allOrders.Where(e => e.Code.ToLower().Contains(searchText.ToLower()))
                            .ToList();
            }

            if(!string.IsNullOrEmpty(customer) && customer!="Select Customer")
            {
                allOrders = allOrders.Where(e => e.Customer == context.Customers.FirstOrDefault(e => e.Name == customer)).ToList();
            }

            List<OrderDto> orderDtos = new();

            foreach (var order in allOrders)
            {
                orderDtos.Add(order.ConvertToDto());
            }

            return PaginationUtility<OrderDto>.GetPaginatedResult(in orderDtos, pageIndex == 0 ? 1 : pageIndex, pageSize == 0 ? 4 : pageSize);
        }

        public async Task<OrderDto> GetSingleOrderAsync(int id)
        {
            Order order = (await context.Orders
                                .Include(e => e.Customer)
                                .Include(e => e.OrderDetails)
                                .ThenInclude(e => e.Product)
                                .FirstOrDefaultAsync(e => e.Id == id))!;

            return order.ConvertToDto();
        }

        public async Task UpdateOrderAsync(OrderDto orderDto)
        {
            Order order = (await context.Orders
                                .Include(e => e.Customer)
                                .Include(e => e.OrderDetails)
                                .ThenInclude(e => e.Product)
                                .FirstOrDefaultAsync(e => e.Id == orderDto.Id))!;

            order.Code = orderDto.OrderCode;
            order.OrderDate = orderDto.OrderDate;
            order.Customer = (await context.Customers.FirstOrDefaultAsync(e => e.Name == orderDto.CustomerName))!;

            foreach (var orderDetail in order.OrderDetails)
            {
                orderDetail.Product.Qty += orderDetail.Qty;

                context.OrderDetails.Remove(orderDetail);
            }

            foreach (var orderDetailDto in orderDto.OrderDetailDtos)
            {
                OrderDetail orderDetail = new();

                orderDetail.Order = order;
                orderDetail.Product = (await context.Products.FirstOrDefaultAsync(e => e.Name == orderDetailDto.ProductName))!;
                orderDetail.Qty = orderDetailDto.Qty;

                orderDetail.Product.Qty -= orderDetailDto.Qty;

                await context.OrderDetails.AddAsync(orderDetail);
            }

            UpdateRecord(order);
        }

        public async Task<Dictionary<string, string>> ValidateOrderAsync(OrderDto orderDto)
        {
            Dictionary<string, string> errors = new();

            var allOrders = await context.Orders
                                .Include(e => e.OrderDetails)
                                .ThenInclude(e => e.Product)
                                .ToListAsync();

            if (orderDto.Id > 0)
            {
                Order order = (await context.Orders
                                .Include(e => e.OrderDetails)
                                .ThenInclude(e => e.Product)
                                .FirstOrDefaultAsync(e => e.Id == orderDto.Id))!;

                if (orderDto.OrderCode != order.Code)
                {
                    if (allOrders.Select(e => e.Code.ToLower()).Contains(orderDto.OrderCode.ToLower()))
                    {
                        errors.Add("OrderCode", "There is already Order with this code in database. Please provide different one!");
                    }
                }

                if (orderDto.OrderDate > DateTime.Now)
                {
                    errors.Add("OrderDate", "Order date must not be larger than today's date!");
                }

                if(orderDto.OrderDetailDtos == null || orderDto.OrderDetailDtos.Count <= 0)
                {
                    errors.Add("OrderDetailDtos", "There must be at least one Product in order list!");
                }

                if (orderDto.OrderDetailDtos != null)
                {
                    foreach (var orderDetailDto in orderDto.OrderDetailDtos)
                    {
                        Product product = (await context.Products.FirstOrDefaultAsync(e => e.Name == orderDetailDto.ProductName))!;

                        if (orderDetailDto.Qty > product.Qty)
                        {
                            errors.Add("OrderDetailDtos", "Product quantity you are triyng to sell is larger than avaliable quantity in stock!");
                        }
                    }
                }
            }
            else
            {
                if (allOrders.Select(e => e.Code.ToLower()).Contains(orderDto.OrderCode.ToLower()))
                {
                    errors.Add("OrderCode", "There is already Order with this code in database. Please provide different one!");
                }

                if (orderDto.OrderDate > DateTime.Now)
                {
                    errors.Add("OrderDate", "Order date must not be larger than today's date!");
                }

                if (orderDto.OrderDetailDtos == null || orderDto.OrderDetailDtos.Count <= 0)
                {
                    errors.Add("OrderDetailDtos", "There must be at least one Product in order list!");
                }

                if (orderDto.OrderDetailDtos != null)
                {
                    foreach (var orderDetailDto in orderDto.OrderDetailDtos)
                    {
                        Product product = (await context.Products.FirstOrDefaultAsync(e => e.Name == orderDetailDto.ProductName))!;

                        if (orderDetailDto.Qty > product.Qty)
                        {
                            errors.Add("OrderDetailDtos", "Product quantity you are triyng to sell is larger than avaliable quantity in stock!");
                        }
                    }
                }
            }

            return errors;
        }
    }
}
