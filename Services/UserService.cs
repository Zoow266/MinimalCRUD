using Microsoft.EntityFrameworkCore;
using MinimalCrud.DATA;
using MinimalCrud.DTO;
using MinimalCrud.Models;

namespace MinimalCrud.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context) => _context = context;
        public async Task<UserResponseDTO> CreateAsync(UserDTO dto)
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return Map(newUser);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var checkUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if(checkUser is null) return false;

            _context.Users.Remove(checkUser);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<UserResponseDTO>> GetAllAsync()
        {
            var userList = await _context.Users.ToListAsync();
            return userList.Select(Map).ToList();
        }

        public async Task<UserResponseDTO?> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user is null ? null : Map(user);
        }

        public async Task<UserResponseDTO?> UpdateAsync(Guid id, UpdateUserDTO dto)
        {
            var checkUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if(checkUser is null) return null;

            if(!string.IsNullOrWhiteSpace(dto.Name)) checkUser.Name = dto.Name;

            if(!string.IsNullOrWhiteSpace(dto.Email)) checkUser.Email = dto.Email;

            await _context.SaveChangesAsync();

            return Map(checkUser);
        }

        private static UserResponseDTO Map(User user)
        {
            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}