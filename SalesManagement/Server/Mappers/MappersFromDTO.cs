using SalesManagement.Shared.DTO;
using SalesManagement.Shared.Models;

namespace SalesManagement.Server.Mappers
{
    public class MappersFromDTO
    {
        public Order MapToOrderEntity(OrderDTO order)
        {
            return new Order
            {
                OrderId = order.OrderId,
                Name = order.Name,
                State = order.State
            };
        }
        public Window MapToWindowEntity(WindowDTO window)
        {
            return new Window
            {
                WindowId = window.WindowId,
                Name = window.Name,
                QuantityOfWindow = window.QuantityOfWindow,
                TotalSubElement = window.TotalSubElement,
                OrderId = window.OrderId
            };
        }
        public SubElement MapToSubElementEntity(SubElementDTO subElement)
        {
            return new SubElement
            {
                SubElementId = subElement.SubElementId,
                Element = subElement.Element,
                Height = subElement.Height,
                Type = subElement.Type,
                Width = subElement.Width,
                WindowId = subElement.WindowId
            };
        }
    }
}
