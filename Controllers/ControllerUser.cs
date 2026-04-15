using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetUsers() => Ok(_context.Users.ToList());

        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var searchUser = _context.Users.FirstOrDefault(x => x.Id == id);
            if(searchUser is null) return NotFound();
            return Ok(searchUser);
        }

        [HttpPost]
        public IActionResult CreateUser(UserDTO userDTO)
        {
            if(userDTO is null) return BadRequest();

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = userDTO.Name,
                Email = userDTO.Email
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUserById), new {id = newUser.Id}, newUser);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, UpdateUserDTO updateUser)
        {
            if (updateUser is null) return BadRequest();

            var searchUpdateUser = _context.Users.FirstOrDefault(x => x.Id == id);
            if(searchUpdateUser is null) return NotFound();

            if(!string.IsNullOrWhiteSpace(updateUser.Name)) searchUpdateUser.Name = updateUser.Name;
            if(!string.IsNullOrWhiteSpace(updateUser.Email)) searchUpdateUser.Email = updateUser.Email;

            _context.SaveChanges();

            return Ok(searchUpdateUser);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            var deleteUser = _context.Users.FirstOrDefault(x => x.Id == id);

            if(deleteUser is null) return NotFound();

            _context.Users.Remove(deleteUser);
            _context.SaveChanges();

            return NoContent();
        }
    }
}