using SalesManagement.Server;
using SalesManagement.Shared.DTO;
using SalesManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Shared.IRepository
{
    public interface IOrderRepository
    {
        Task<Result<OrderDTO>> AddOrder(OrderDTO order);
        Task<Result<OrderDTO>> UpdateOrder(OrderDTO order);
        Task<Result<String>> DeleteOrder(int id);
        Task<Result<List<OrderDTO>>> GetAllOrders();
        Task<Result<OrderDTO>> GetOrderById(int id);
    }
}
