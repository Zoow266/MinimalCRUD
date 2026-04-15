using Microsoft.AspNetCore.Mvc;
using MinimalCrud.DTO;
using MinimalCrud.Models;

namespace MinimalCrud.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // Список пользователей
        private static List<User> users = new List<User>();

        // Получить всех пользователей
        [HttpGet]
        public IActionResult GetUsers() => Ok(users);

        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var searchUser = users.FirstOrDefault(x => x.Id == id);
            if(searchUser is null) return NotFound();
            return Ok(searchUser);
        }

        [HttpPost]
        public IActionResult CreateUser(UserDTO userDTO)
        {
            if(userDTO is null) return BadRequest();

            var newUser = new User
            {
                Name = userDTO.Name,
                Email = userDTO.Email
            };

            users.Add(newUser);

            return CreatedAtAction(nameof(GetUserById), new {id = newUser.Id}, newUser);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, UpdateUserDTO updateUser)
        {
            if (updateUser is null) return BadRequest();

            var searchUpdateUser = users.FirstOrDefault(x => x.Id == id);
            if(searchUpdateUser is null) return NotFound();

            if(!string.IsNullOrWhiteSpace(updateUser.Name)) searchUpdateUser.Name = updateUser.Name;
            if(!string.IsNullOrWhiteSpace(updateUser.Email)) searchUpdateUser.Email = updateUser.Email;

            return Ok(searchUpdateUser);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            var deleteUser = users.FirstOrDefault(x => x.Id == id);

            if(deleteUser is null) return NotFound();

            users.Remove(deleteUser);

            return NoContent();
        }
    }
}