using SalesManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Shared.DTO
{
    public class SubElementDTO
    {
        public int SubElementId { get; set; }
        public string Type { get; set; }
        public int Element { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int WindowId { get; set; } 
       
    }
}
