using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Shared.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public List<Window>? windows { get; set; }
    }
}
