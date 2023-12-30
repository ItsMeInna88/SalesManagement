using SalesManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Shared.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public List<WindowDTO>? windows { get; set; } 
    }
}
