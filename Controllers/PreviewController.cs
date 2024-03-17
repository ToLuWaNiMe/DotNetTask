using Microsoft.AspNetCore.Mvc;
using Shared.Services.Interfaces;

namespace DotNetTask.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class PreviewController : ControllerBase
    {
        private readonly IPreviewService _previewService;
        public PreviewController(IPreviewService previewService)
        {
            _previewService = previewService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPreview(string id)
        {
            var preview = await _previewService.GetByIdAsync(id);
            if (preview == null)
            {
                return NotFound($"Preview with ID {id} not found.");
            } 
            return Ok(preview);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetPreviews()
        {
            var preview = await _previewService.GetAllAsync();

            if (preview == null)
            {
                return NotFound("Preview not found");
            }
            return Ok(preview);
        }
    }
}
