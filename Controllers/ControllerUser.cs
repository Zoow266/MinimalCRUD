using Microsoft.AspNetCore.Mvc;
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
        public IActionResult CreateUser(User user)
        {
            if(user is null) return BadRequest();

            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email
            };

            users.Add(newUser);

            return Ok(newUser);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, User updateUser)
        {
            var searchUpdateUser = users.FirstOrDefault(x => x.Id == id);

            if(searchUpdateUser is null) return BadRequest();

            searchUpdateUser.Name = updateUser.Name;
            searchUpdateUser.Email = updateUser.Email;

            return Ok(searchUpdateUser);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            var deleteUser = users.FirstOrDefault(x => x.Id == id);

            if(deleteUser is null) return NoContent();

            users.Remove(deleteUser);

            return Ok();
        }
    }
}