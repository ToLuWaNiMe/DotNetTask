using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Services.Interfaces;

namespace DotNetTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkFlowController : ControllerBase
    {
        private readonly IWorkFlowService _workFlowService;
        public WorkFlowController(IWorkFlowService workFlowService)
        {
            _workFlowService = workFlowService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkFlow(string id)
        {
            var workFlow = await _workFlowService.GetByIdAsync(id);
            if (workFlow == null)
            {
                return NotFound($"WorkFlow with ID {id} not found.");
            }
            return Ok(workFlow);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetWorkFlow()
        {
            var workFlows = await _workFlowService.GetAllAsync();

            if (workFlows == null)
            {
                return NotFound("WorkFlow not found");
            }

            return Ok(workFlows);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkFlow(string id, [FromBody] WorkFlowDto updateDto)
        {
            var existingWorkFlow = await _workFlowService.GetByIdAsync(id);
            if (existingWorkFlow == null)
            {
                return NotFound($"WorkFlow with ID {id} not found.");
            }
            var updated = await _workFlowService.UpdateAsync(id, updateDto);

            if (updated)
            {
                return Ok("WorkFlow updated successfully.");
            }
            else
            {
                return BadRequest("Failed to update WorkFlow.");
            }
        }
    }
}
