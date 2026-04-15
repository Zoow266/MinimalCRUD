using MinimalCrud.DTO;
using MinimalCrud.Models;

namespace MinimalCrud.Services
{
    public interface IUserService
    {
        Task<List<UserResponseDTO>> GetAllAsync();
        Task<UserResponseDTO?> GetByIdAsync(Guid id);

        Task<UserResponseDTO> CreateAsync(UserDTO dto);
        Task<UserResponseDTO?> UpdateAsync(Guid id, UpdateUserDTO dto);

        Task<bool> DeleteAsync(Guid id);
    }
}