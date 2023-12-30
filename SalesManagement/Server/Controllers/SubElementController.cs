using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SalesManagement.Server.Services;
using SalesManagement.Shared.DTO;
using SalesManagement.Shared.IRepository;
using SalesManagement.Shared.Models;

namespace SalesManagement.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubElementController : Controller
    {
        readonly ISubElementRepository _subElementRepository;
        public SubElementController(ISubElementRepository subElementRepository)
        {
            _subElementRepository = subElementRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubElementDTO>>> Get()
        {
            var response = await _subElementRepository.GetAllSubElements();
            if (response.IsSuccess)
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                var json = JsonConvert.SerializeObject(response.Value, settings);
                return Ok(json);
            }
            return BadRequest(response.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubElementDTO>> Get(int id)
        {
            var response = await _subElementRepository.GetSubElementById(id);
            if (response.IsSuccess)
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore // Ignore reference loops
                };

                var json = JsonConvert.SerializeObject(response.Value, settings);
                return Ok(json);
            }
            return BadRequest(response.ErrorMessage);
        }
        [HttpPost]
        public async Task<ActionResult<SubElementDTO>> CreateSubElement(SubElementDTO subElement)
        {
           var response= _subElementRepository.AddSubElement(subElement);
           return response.Result.IsSuccess ? Ok(response.Result.Value) : BadRequest(response.Result.ErrorMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubElement(int id, SubElementDTO updatedSubElement)
        {
            var existingSubElement = _subElementRepository.UpdateSubElement(updatedSubElement);
            return existingSubElement.Result.IsSuccess ? Ok(existingSubElement.Result.Value) : BadRequest(existingSubElement.Result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var subElement = _subElementRepository.DeleteSubElement(id);
            return subElement.Result.IsSuccess ? Ok(subElement.Result.Value) : BadRequest(subElement.Result.ErrorMessage);
        }

    }
}
