using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalCrud.DATA;
using MinimalCrud.DTO;
using MinimalCrud.Models;

namespace MinimalCrud.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context) => _context = context;

        // Получить всех пользователей
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var usersList = await _context.Users.ToListAsync();
            return Ok(usersList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var searchUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if(searchUser is null) return NotFound();
            return Ok(searchUser);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDTO userDTO)
        {
            if(userDTO is null) return BadRequest();

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = userDTO.Name,
                Email = userDTO.Email
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new {id = newUser.Id}, newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDTO updateUser)
        {
            if (updateUser is null) return BadRequest();

            var searchUpdateUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if(searchUpdateUser is null) return NotFound();

            if(!string.IsNullOrWhiteSpace(updateUser.Name)) searchUpdateUser.Name = updateUser.Name;
            if(!string.IsNullOrWhiteSpace(updateUser.Email)) searchUpdateUser.Email = updateUser.Email;

            await _context.SaveChangesAsync();

            return Ok(searchUpdateUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var deleteUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if(deleteUser is null) return NotFound();

            _context.Users.Remove(deleteUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}