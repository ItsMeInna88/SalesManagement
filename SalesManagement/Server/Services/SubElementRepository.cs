using Microsoft.EntityFrameworkCore;
using SalesManagement.Server.DatabaseAccess;
using SalesManagement.Server.Mappers;
using SalesManagement.Server.MappersDTO;
using SalesManagement.Shared.DTO;
using SalesManagement.Shared.IRepository;
using SalesManagement.Shared.Models;

namespace SalesManagement.Server.Services
{
    public class SubElementRepository : ISubElementRepository
    {
        readonly AppDbContext db;
        MappersToDTO mappers;
        MappersFromDTO fromDTO;
        public SubElementRepository(AppDbContext _appDbContext, MappersToDTO mapper, MappersFromDTO fromDTOmap)
        {
            db = _appDbContext;
            mappers = mapper;
            fromDTO = fromDTOmap;
        }
        #region AddSubElement
        async Task<Result<SubElementDTO>> ISubElementRepository.AddSubElement(SubElementDTO subElement)
        {
            try
            {
                var subElementToAdd = fromDTO.MapToSubElementEntity(subElement);
                db.SubElements.Add(subElementToAdd);
                db.SaveChangesAsync();
                return Result<SubElementDTO>.Success(mappers.MapToSubElementDTO(subElementToAdd));
            }
            catch (Exception ex)
            {
                return Result<SubElementDTO>.Failure(ex.Message);
            }
        }
        #endregion
        #region DeleteSubElement
        public async Task<Result<string>> DeleteSubElement(int id)
        {
            try
            {
                if (db.SubElements.Any(x => x.SubElementId == id))
                {
                    db.SubElements.Remove(db.SubElements.FirstOrDefault(x => x.SubElementId == id));
                    db.SaveChangesAsync();
                    return Result<string>.Success("SubElement deleted in the database.");
                }
                else
                {
                    return Result<string>.Failure("SubElement not found in the database.");
                }
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(ex.Message);
            }

        }
        #endregion
        #region UpdateSubElement
        async Task<Result<SubElementDTO>> ISubElementRepository.UpdateSubElement(SubElementDTO subElement)
        {
            try
            {
                if (db.SubElements.Any(x => x.SubElementId == subElement.SubElementId))
                {
                    var obj = db.SubElements.FirstOrDefault(x => x.SubElementId == subElement.SubElementId);

                    obj.SubElementId = subElement.SubElementId;
                    obj.Width = subElement.Width;
                    obj.Element = subElement.Element;
                    obj.Height = subElement.Height;
                    obj.Type = subElement.Type;
                    obj.WindowId = subElement.WindowId;

                    db.SaveChangesAsync();
                    return Result<SubElementDTO>.Success(mappers.MapToSubElementDTO(obj));
                }
                else
                {
                    return Result<SubElementDTO>.Failure("SubElement not found in the database.");
                }
            }
            catch (Exception ex)
            {
                return Result<SubElementDTO>.Failure(ex.Message);
            }

        }
        #endregion
        #region GetAllSubElements
        async Task<Result<List<SubElementDTO>>> ISubElementRepository.GetAllSubElements()
        {
            try
            {
                var subElement = await db.SubElements.ToListAsync();

                return Result<List<SubElementDTO>>.Success(subElement.Select(o => mappers.MapToSubElementDTO(o)).ToList());
            }
            catch (Exception ex)
            {
                return Result<List<SubElementDTO>>.Failure(ex.Message);
            }

        }
        #endregion
        #region GetSubElementById
        async Task<Result<SubElementDTO>> ISubElementRepository.GetSubElementById(int id)
        {
            try
            {
                if (db.SubElements.Any(x => x.SubElementId == id))
                {
                    var obj = await db.SubElements.FirstOrDefaultAsync(o => o.SubElementId == id);
                    return Result<SubElementDTO>.Success(mappers.MapToSubElementDTO(obj));
                }
                else
                {
                    return Result<SubElementDTO>.Failure("SubElement not found in the database.");
                }
            }
            catch (Exception ex)
            {
                return Result<SubElementDTO>.Failure(ex.Message);
            }

        }
        #endregion
    }
}
