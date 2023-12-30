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
    public interface ISubElementRepository
    {
        Task<Result<SubElementDTO>>AddSubElement(SubElementDTO subElement);
        Task<Result<SubElementDTO>> UpdateSubElement(SubElementDTO subElement);
        Task<Result<string>> DeleteSubElement(int id);
        Task<Result<List<SubElementDTO>>> GetAllSubElements();
        Task<Result<SubElementDTO>> GetSubElementById(int id);
    }
}

