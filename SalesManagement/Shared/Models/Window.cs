using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Shared.Models
{
    public class Window
    {
        public int WindowId {  get; set; }
        public string Name { get; set; }
        public int QuantityOfWindow {  get; set; }
        public int TotalSubElement {  get; set; }
        public int OrderId { get; set; } // Foreign key
        public Order Order { get; set; }
        public List<SubElement>?subelements { get; set; }

    }
}
