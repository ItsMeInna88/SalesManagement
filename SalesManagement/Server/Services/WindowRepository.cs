using Microsoft.EntityFrameworkCore;
using SalesManagement.Server.DatabaseAccess;
using SalesManagement.Server.Mappers;
using SalesManagement.Server.MappersDTO;
using SalesManagement.Shared.DTO;
using SalesManagement.Shared.IRepository;
using SalesManagement.Shared.Models;

namespace SalesManagement.Server.Services
{
    public class WindowRepository : IWindowRepository
    {
        readonly AppDbContext db;
        MappersToDTO mappers;
        MappersFromDTO fromDTO;
        public WindowRepository(AppDbContext _appDbContext, MappersToDTO mapper, MappersFromDTO fromDTOmap)
        {
            db = _appDbContext;
            mappers = mapper;
            fromDTO = fromDTOmap;
        }
        #region AddWindow
        async Task<Result<WindowDTO>> IWindowRepository.AddWindow(WindowDTO window)
        {
            window.TotalSubElement = TotalSubElements(window.WindowId) ;
            try
            {
                var windowToAdd = fromDTO.MapToWindowEntity(window);
                db.Windows.Add(windowToAdd);
                db.SaveChanges();
                return Result<WindowDTO>.Success(mappers.MapToWindowDTO(windowToAdd));
            }
            catch (Exception ex)
            {
                return Result<WindowDTO>.Failure(ex.Message);
            }
        }
        #endregion 

        #region UpdateWindow
        async Task<Result<WindowDTO>> IWindowRepository.UpdateWindow(WindowDTO window)
        {
            try
            {
                if (db.Windows.Any(x => x.WindowId == window.WindowId))
                {
                    var obj = db.Windows.FirstOrDefault(x => x.WindowId == window.WindowId);
                    obj.QuantityOfWindow = window.QuantityOfWindow;
                    obj.TotalSubElement = TotalSubElements(window.WindowId);
                    obj.OrderId = window.OrderId;
                    obj.Name= window.Name; ;
                    try { db.SaveChanges(); }
                    catch(Exception ex) { }
                   
                    return Result<WindowDTO>.Success(mappers.MapToWindowDTO(obj));
                }
                else
                {
                    return Result<WindowDTO>.Failure("Window not found in the database.");
                }
            }
            catch (Exception ex)
            {
                return Result<WindowDTO>.Failure(ex.Message);
            }

        }
        #endregion
        #region DeleteWindow
        public async Task<Result<String>> DeleteWindow(int id)
        {
            try
            {
                if (db.Windows.Any(x => x.WindowId == id))
                {
                    db.Windows.Remove(db.Windows.FirstOrDefault(x => x.WindowId == id)); db.SaveChangesAsync();
                    return Result<string>.Success("Window succeed deletin.");
                }
                else
                {
                    return Result<string>.Failure("Window not found in the database.");
                }
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(ex.Message);
            }
        }
        #endregion
        #region GetAllWindows
        async Task<Result<List<WindowDTO>>> IWindowRepository.GetAllWindows()
        {
            try
            {
                var window = await db.Windows.ToListAsync();
                foreach(var w in window)
                {
                    w.TotalSubElement = TotalSubElements(w.WindowId);
                }
                return Result<List<WindowDTO>>.Success(window.Select(o => mappers.MapToWindowDTO(o)).ToList());
            }
            catch (Exception ex)
            {
                return Result<List<WindowDTO>>.Failure(ex.Message);
            }

        }
        #endregion
        #region GetWindowById
        async Task<Result<WindowDTO>> IWindowRepository.GetWindowById(int id)
        {
            try 
            {
                if (db.Windows.Any(x => x.WindowId == id))
                {
                    var obj = await db.Windows.FirstOrDefaultAsync(o => o.WindowId == id);
                    return Result<WindowDTO>.Success(mappers.MapToWindowDTO(obj));
                }
                else
                {
                    return Result<WindowDTO>.Failure("Window not found in the database.");
                }
            }
            catch(Exception ex)
            {
                return Result<WindowDTO>.Failure(ex.Message);
            }
            
        }
        #endregion
        int TotalSubElements(int id)
        {
            if (db.SubElements.Any(x => x.WindowId == id))
            {
                return db.SubElements.Where(x => x.WindowId == id).Count();
            }
            else
            {
                return 0;

            }
        }
    }
}

