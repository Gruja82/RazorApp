using RazorApp.Data.Entities;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Services.Repositories.Orders
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        Task<Pagination<OrderDto>> GetAllOrdersAsync(string searchText, string customer, int pageIndex, int pageSize);
        Task<OrderDto> GetSingleOrderAsync(int id);
        Task CreateNewOrderAsync(OrderDto orderDto);
        Task UpdateOrderAsync(OrderDto orderDto);
        Task DeleteOrderAsync(int id);
        Task<Dictionary<string,string>> ValidateOrderAsync(OrderDto orderDto);
    }
}
