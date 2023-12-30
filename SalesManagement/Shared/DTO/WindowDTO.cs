using SalesManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Shared.DTO
{
    public class WindowDTO
    {
        public int WindowId { get; set; }
        public string Name { get; set; }
        public int QuantityOfWindow { get; set; }
        public int TotalSubElement { get; set; }
        public int OrderId { get; set; } // Foreign key
        public List<SubElementDTO>? subelements { get; set; }

    }
}
