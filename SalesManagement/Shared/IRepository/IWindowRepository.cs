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
    public interface IWindowRepository
    {
        Task<Result<WindowDTO>>AddWindow(WindowDTO window);
        Task<Result<WindowDTO>>UpdateWindow(WindowDTO window);
        Task<Result<String>> DeleteWindow(int id);
        Task<Result<List<WindowDTO>>> GetAllWindows();
        Task<Result<WindowDTO>> GetWindowById(int id);
       

    }
}
