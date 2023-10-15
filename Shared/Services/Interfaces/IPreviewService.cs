using Domain.Models;
using Shared.DTOs;

namespace Shared.Services.Interfaces
{
    public interface IPreviewService
    {
        Task<IEnumerable<Preview>> GetAllAsync();
        Task<Preview> GetByIdAsync(string id);
        Task<bool> AddAsync(PreviewDto entity);
        Task UpdateAsync(string id, Preview entity);
        Task<bool> DeleteAsync(string id);
    }
}
