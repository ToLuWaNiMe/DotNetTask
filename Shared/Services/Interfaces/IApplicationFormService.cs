using Domain.Models;
using Shared.DTOs;

namespace Shared.Services.Interfaces
{
    public interface IApplicationFormService
    {
        Task<IEnumerable<ApplicationForm>> GetAllAsync();
        Task<ApplicationForm> GetByIdAsync(string id);
        Task<bool> AddAsync(ApplicationFormDto entity);
        Task<bool> UpdateAsync(string id, ApplicationFormDto updateDto);
        Task<bool> DeleteAsync(string id);
    }
}
