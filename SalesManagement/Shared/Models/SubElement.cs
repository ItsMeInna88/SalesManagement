using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Shared.Models
{
    public class SubElement
    {
        public int SubElementId {  get; set; }
        public string Type {  get; set; }
        public int Element {  get; set; }
        public int Width {  get; set; }
        public int Height {  get; set; }
        public int WindowId { get; set; } // Foreign key
        public Window Window { get; set; } // Navigation property

    }
}
