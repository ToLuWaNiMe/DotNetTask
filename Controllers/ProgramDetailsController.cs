using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Services.Interfaces;

namespace DotNetTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramDetailsController : ControllerBase
    {
        private readonly IProgramDetailsService _programDetailsService;

        public ProgramDetailsController(IProgramDetailsService programDetailsService)
        {
            _programDetailsService = programDetailsService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateProgram([FromBody] ProgramDetailsDto entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProgram = await _programDetailsService.AddAsync(entity);
            if (createdProgram)
            {
                return Ok(createdProgram);
            }
            else
            {
                return BadRequest(createdProgram);
            }

        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetProgram(string id)
        {
            var programDetail = await _programDetailsService.GetByIdAsync(id);
            if (programDetail == null)
            {
                return NotFound($"Program with ID {id} not found.");
            }

            return Ok(programDetail);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetPrograms()
        {
            var programDetails = await _programDetailsService.GetAllAsync();

            if (programDetails == null)
            {
                return NotFound("Programs not found");
            }
            return Ok(programDetails);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProgram(string id, [FromBody] ProgramDetailsDto updateModel)
        {
            var updated = await _programDetailsService.UpdateAsync(id, updateModel);

            if (updated)
            {
                return Ok("Program updated successfully.");
            }

            return NotFound($"Program with ID {id} not found.");
        }

    }
}
