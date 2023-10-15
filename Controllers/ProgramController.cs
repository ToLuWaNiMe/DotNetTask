using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Services.Interfaces;

namespace DotNetTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramDetailsService _programService;

        public ProgramController(IProgramDetailsService programService)
        {
            _programService = programService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateProgram([FromBody] ProgramDto entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProgram = await _programService.AddAsync(entity);
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
            var programDetail = await _programService.GetByIdAsync(id);
            if (programDetail == null)
            {
                return NotFound($"Program with ID {id} not found.");
            }

            return Ok(programDetail);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetPrograms()
        {
            var programDetails = await _programService.GetAllAsync();

            if (programDetails == null)
            {
                return NotFound("Programs not found");
            }
            return Ok(programDetails);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProgram(string id, [FromBody] ProgramDto updateModel)
        {
            var updated = await _programService.UpdateAsync(id, updateModel);

            if (updated)
            {
                return Ok("Program updated successfully.");
            }

            return NotFound($"Program with ID {id} not found.");
        }

    }
}
