using Microsoft.EntityFrameworkCore;
using SalesManagement.Server.DatabaseAccess;
using SalesManagement.Server.Mappers;
using SalesManagement.Server.MappersDTO;
using SalesManagement.Shared.DTO;
using SalesManagement.Shared.IRepository;
using SalesManagement.Shared.Models;
using System;
using System.Collections.Generic;

namespace SalesManagement.Server.Services
{
    public class OrderRepository : IOrderRepository
    {
        readonly AppDbContext db;
        MappersToDTO mappers;
        MappersFromDTO fromDTO;
        public OrderRepository(AppDbContext _appDbContext, MappersToDTO mapper, MappersFromDTO mapfromDTO)
        {
            db = _appDbContext;
            mappers = mapper;
            fromDTO = mapfromDTO;
        }
        #region AddOrder
        async Task<Result<OrderDTO>> IOrderRepository.AddOrder(OrderDTO order)
        {
            var orderToAdd = fromDTO.MapToOrderEntity(order);
            try
            {
                db.Orders.Add(orderToAdd);
                db.SaveChangesAsync();
                var addedOrderDTO = mappers.MapToOrderDTO(orderToAdd);
                return Result<OrderDTO>.Success(addedOrderDTO);
            }
            catch (Exception ex)
            {
                return Result<OrderDTO>.Failure(ex.Message);

            }
        }
        #endregion
        #region UpdateOrder
        async Task<Result<OrderDTO>> IOrderRepository.UpdateOrder(OrderDTO order)
        {
            var orderToUpdate = db.Orders.Include(o => o.windows).ThenInclude(w => w.subelements).Where(x=>x.OrderId==order.OrderId).FirstOrDefault();
            if (orderToUpdate == null)
            {
                return Result<OrderDTO>.Failure("Order not found in the database.");

            }
            try
            {
                orderToUpdate.Name = order.Name;
                orderToUpdate.State = order.State;
                orderToUpdate.OrderId = order.OrderId;
               
                db.SaveChangesAsync();
                return Result<OrderDTO>.Success(mappers.MapToOrderDTO(orderToUpdate));
            }
            catch (Exception ex)
            {
                return Result<OrderDTO>.Failure(ex.Message);
            }
        }
        #endregion
        #region DeleteOrder
        public async Task<Result<String>> DeleteOrder(int id)
        {
            try
            {
                if (db.Orders.Any(x => x.OrderId == id))
                {
                    db.Orders.Remove(db.Orders.FirstOrDefault(x => x.OrderId == id));
                    db.SaveChangesAsync();
                    return Result<string>.Success("Order deleted successfully.");
                }
                else
                {
                    return Result<string>.Failure("Order not found in the database.");
                }
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"Error deleting order: {ex.Message}");
            }
        }
        #endregion
        #region GetAllOrders
        async Task<Result<List<OrderDTO>>> IOrderRepository.GetAllOrders()
        {
            try
            {
                var ordersWithRelatedData = db.Orders.Include(o => o.windows).ThenInclude(w => w.subelements).ToList();
                return Result<List<OrderDTO>>.Success(ordersWithRelatedData.Select(o => mappers.MapToOrderDTO(o)).ToList());
            }
            catch (Exception ex)
            {
                return Result<List<OrderDTO>>.Failure($"Error Gettin order: {ex.Message}");
            }
        }
        #endregion
        #region GetOrderById
        async Task<Result<OrderDTO>> IOrderRepository.GetOrderById(int id)
        {
            try
            {
                if (db.Orders.Any(x => x.OrderId == id))
                {
                    var orderbyId = db.Orders.Include(x => x.windows).ThenInclude(w => w.subelements).FirstOrDefault(o => o.OrderId == id);
                    return Result<OrderDTO>.Success(mappers.MapToOrderDTO(orderbyId));
                }
                else
                {
                    return Result<OrderDTO>.Failure("Order not found in the database.");
                }
            }
            catch (Exception ex)
            {
                return Result<OrderDTO>.Failure($"Error Gettin order: {ex.Message}");
            }

        }
        #endregion
    }
}

