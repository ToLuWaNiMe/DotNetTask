using Domain.Models;
using Shared.DTOs;

namespace Shared.Services.Interfaces
{
    public interface IProgramDetailsService
    {
        Task<IEnumerable<ProgramDetails>> GetAllAsync();
        Task<ProgramDetails> GetByIdAsync(string id);
        Task<bool> AddAsync(ProgramDto entity);
        Task<bool> UpdateAsync(string id, ProgramDto updateDto);
        Task<bool> DeleteAsync(string id);
    }
}
