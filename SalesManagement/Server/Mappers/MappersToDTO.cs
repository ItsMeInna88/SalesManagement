using SalesManagement.Shared.DTO;
using SalesManagement.Shared.Models;

namespace SalesManagement.Server.MappersDTO
{
    public class MappersToDTO
    {
        public OrderDTO MapToOrderDTO(Order order)
        {
            return new OrderDTO
            {
                OrderId = order.OrderId,
                Name = order.Name,
                State = order.State,
                windows = order.windows?.Select(w => new WindowDTO
                {
                    WindowId = w.WindowId,
                    QuantityOfWindow = w.QuantityOfWindow,
                    TotalSubElement = w.TotalSubElement,
                    Name = w.Name,
                    OrderId = order.OrderId,
                    subelements = w.subelements?.Select(s => new SubElementDTO
                    {
                        SubElementId = s.SubElementId,
                        Element = s.Element,
                        Height = s.Height,
                        Type = s.Type,
                        Width = s.Width,
                        WindowId = s.WindowId
                    }).ToList()
                }).ToList()
            };
        }
        public WindowDTO MapToWindowDTO(Window window)
        {
            return new WindowDTO
            {
                WindowId = window.WindowId,
                Name = window.Name,
                QuantityOfWindow = window.QuantityOfWindow,
                TotalSubElement = window.TotalSubElement,
                OrderId = window.OrderId
            };
        }
        public SubElementDTO MapToSubElementDTO(SubElement subElement)
        {
            return new SubElementDTO
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
