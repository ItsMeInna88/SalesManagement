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
    public class WindowController : Controller
    {
        private readonly IWindowRepository _windowRepository;

        public WindowController(IWindowRepository windowRepository)
        {
            _windowRepository = windowRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WindowDTO>>> Get()
        {
            var response = await _windowRepository.GetAllWindows();
            if (response.IsSuccess)
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                var json = JsonConvert.SerializeObject(response.Value, settings);
                return Ok(json);
            }
            else
            {
                return BadRequest(response.ErrorMessage);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WindowDTO>> Get(int id)
        {
            var response = await _windowRepository.GetWindowById(id);
            if (response.IsSuccess)
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                var json = JsonConvert.SerializeObject(response.Value, settings);
                return Ok(json);
            }
            else
            {
                return BadRequest(response.ErrorMessage);
            }
        }
        [HttpPost]
        public async Task<ActionResult<WindowDTO>> CreateWindow(WindowDTO window)
        {
            var response = await _windowRepository.AddWindow(window);
            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.ErrorMessage);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateWindow(int id, WindowDTO updatedwindow)
        {
            var response = _windowRepository.UpdateWindow(updatedwindow);
            return response.Result.IsSuccess ? Ok(response.Result.Value) : BadRequest(response.Result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = _windowRepository.DeleteWindow(id);
            return response.Result.IsSuccess ? Ok(response.Result.Value) : BadRequest(response.Result.ErrorMessage);
        }

    }
}
