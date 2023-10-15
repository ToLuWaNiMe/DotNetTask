using Domain.Models;
using Shared.DTOs;

namespace Shared.Services.Interfaces
{
    public interface IWorkFlowService
    {
        Task<IEnumerable<WorkFlow>> GetAllAsync();
        Task<WorkFlow> GetByIdAsync(string id);
        Task<bool> AddAsync(WorkFlowDto entity);
        Task<bool> UpdateAsync(string id, WorkFlowDto updateDto);
        Task<bool> DeleteAsync(string id);
    }
}
