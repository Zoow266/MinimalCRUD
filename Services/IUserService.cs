using MinimalCrud.DTO;
using MinimalCrud.Models;

namespace MinimalCrud.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);

        Task<User> CreateAsync(UserDTO dto);
        Task<User?> UpdateAsync(Guid id, UpdateUserDTO dto);

        Task<bool> DeleteAsync(Guid id);
    }
}