using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Services.Interfaces;

namespace DotNetTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationFormController : ControllerBase
    {
        private readonly IApplicationFormService _applicationFormService;

        public ApplicationFormController(IApplicationFormService applicationFormService)
        {
            _applicationFormService = applicationFormService;
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetApplicationForm(string id)
        {
            var appForm = await _applicationFormService.GetByIdAsync(id);
            if (appForm == null)
            {
                return NotFound($"Application Form with ID {id} not found.");
            }

            return Ok(appForm);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetApplicationForms()
        {
            var appForms = await _applicationFormService.GetAllAsync();

            if (appForms == null)
            {
                return NotFound("Application Form not found");
            }
            return Ok(appForms);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplicationForm(string id, [FromBody] ApplicationFormDto updateModel)
        {
            var updated = await _applicationFormService.UpdateAsync(id, updateModel);

            if (updated)
            {
                return Ok("Application Form updated successfully.");
            }

            return NotFound($"Application Form with ID {id} not found.");
        }

    }
}
